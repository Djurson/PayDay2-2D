using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public int level;
    public int experience;
    private int baseExperience;

    public LevelSystem()
    {
        level = 0;
        baseExperience = 125;
    }

    public void AddExperience(int ammount)
    {
        experience += ammount;
        while (experience >= ExperienceToNextLevel())
        {
            experience -= ExperienceToNextLevel();
            level++;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int ExperienceToNextLevel()
    {
        if(level > 0)
        {
            int experienceToNext = baseExperience * (int)Mathf.Pow(level, 2);
            return experienceToNext;
        }
        else
        {
            return 125;
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        return experience / (float) ExperienceToNextLevel();
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetBaseExperience()
    {
        return baseExperience;
    }

    public int GetExperienceToNextLevel()
    {
        return ExperienceToNextLevel();
    }
}
