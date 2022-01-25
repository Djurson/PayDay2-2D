using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler instance;

    public LevelSystem levelSystem;
    public LevelSystemAnimated levelSystemAnimated;

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image ExperienceImage;

    public int levelSystemExperience;
    public int levelSystemLevel;
    [SerializeField] private int levelSystemExperienceToNextLevel;
    [Space(15)]
    public int levelSystemAnimatedExperience;
    public int levelSystemAnimatedLevel;
    [SerializeField] private int levlSystemAnimatedExperienceToNextLevel;

    public bool check = true;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        levelSystem = new LevelSystem();
        levelSystemAnimated = new LevelSystemAnimated();
        CheckScene();
        SetLevelSystemAnimated();
    }

    private void Update()
    {
        CheckScene();
        if(levelText != null && ExperienceImage != null)
        {
            SetLevelSystemAnimated();
        }

        levelSystemExperience = levelSystem.GetExperience();
        levelSystemLevel = levelSystem.GetLevelNumber();
        levelSystemExperienceToNextLevel = levelSystem.GetExperienceToNextLevel();

        levelSystemAnimatedExperience = levelSystemAnimated.GetExperience();
        levelSystemAnimatedLevel = levelSystemAnimated.GetLevelNumber();
        levlSystemAnimatedExperienceToNextLevel = levelSystemAnimated.GetExperienceToNextLevel();
    }

    private void CheckScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            levelText = MainMenuHandler.instance.LevelText;
            ExperienceImage = MainMenuHandler.instance.ExperienceCircle;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            ExperienceImage = FinishedHeistHandler.instance.ExperienceCircle;
            levelText = FinishedHeistHandler.instance.LevelText;
        }
    }

    public void SetLevelSystemAnimated()
    {
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        setExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        levelSystemAnimated.OnExperienceChanged += levelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += levelSystemAnimated_OnLevelChanged;
    }

    private void levelSystemAnimated_OnExperienceChanged(object sender, EventArgs e)
    {
        setExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }

    private void levelSystemAnimated_OnLevelChanged(object sender, EventArgs e)
    {
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }

    private void setExperienceBarSize(float experienceNormalized)
    {
        ExperienceImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = levelNumber.ToString();
    }

    public void AddExperience(int ammount)
    {
        levelSystem.AddExperience(ammount);
    }

    private IEnumerator WaitFor2Seconds()
    {
        yield return new WaitForSeconds(2);
        check = false;
        StopCoroutine("WaitFor2Seconds");
    }
}
