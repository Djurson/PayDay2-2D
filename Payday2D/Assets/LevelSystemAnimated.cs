using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LevelSystemAnimated
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private LevelSystem levelSystem;
    private bool isAnimating;
    private float updateTimer;
    private float updateTimerMax;
    private float experienceFloat;

    public int level;
    public int experience;
    private int baseExperience;

    private int ExperienceToNextLevel()
    {
        if (level > 0)
        {
            int experienceToNext = baseExperience * (int)Mathf.Pow(level, 2);
            return experienceToNext;
        }
        else
        {
            return 125;
        }
    }

    public LevelSystemAnimated()
    {
        SetLevelSystem(LevelHandler.instance.levelSystem);
        updateTimerMax = .016f;

        FunctionUpdater.Create(() => Update());
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperience();
        experienceFloat = levelSystem.GetExperience();
        baseExperience = levelSystem.GetBaseExperience();

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    private void Update()
    {
        if (isAnimating)
        {
            //Checks if its time to update
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax)
            {
                updateTimer -= updateTimerMax;
                UpdateTypeAddExperience();
            }
        }
    }

    private void UpdateTypeAddExperience()
    {
        if (level < levelSystem.GetLevelNumber())
        {
            AddExperience();
        }
        else
        {
            if (experience < levelSystem.GetExperience())
            {
                AddExperience();
            }
            else
            {
                isAnimating = false;
            }
        }
    }

    private void AddExperience()
    {
        experienceFloat = Mathf.MoveTowards(experienceFloat, ExperienceToNextLevel(), (float) ExperienceToNextLevel() * Time.deltaTime);
        experience = (int) experienceFloat;
        if (experience >= ExperienceToNextLevel())
        {
            level++;
            experience = 0;
            experienceFloat = 0;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        return (float) experience / ExperienceToNextLevel();
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetExperienceToNextLevel()
    {
        return ExperienceToNextLevel();
    }
}
