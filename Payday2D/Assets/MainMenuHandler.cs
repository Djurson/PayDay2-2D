using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Globalization;

public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler instance;

    [Header("Menus")]
    [SerializeField] private GameObject startupMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject offlineHeistSetupMenu;
    [SerializeField] private GameObject confirmQuitMenu;
    [SerializeField] private GameObject bugreportMenu;
    [SerializeField] private GameObject loadingMenu;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private TextMeshProUGUI playtimeText;


    [Header("Saving Logic")]
    [SerializeField] private GameObject savingAnimationGameObject;

    [Header("Text Assets")]
    [SerializeField] private TextAsset TipsTextFile;
    private string TipsTextFileText;

    [Header("Stats")]
    public TMP_Text LevelText;
    public Image ExperienceCircle;
    [SerializeField] private TMP_Text SpendableCashText;
    [SerializeField] private TMP_Text OffshoreAccountText;
    [SerializeField] private TMP_Text CompletedHeistsText;
    [SerializeField] private TMP_Text PlaytimeText;

    private int seconds;
    private int minutes;
    private int hours;

    public bool firstTimeLoading = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        if (File.Exists($"{Application.persistentDataPath}/Tips/Tips.txt"))
        {
            if(TipsTextFileText != File.ReadAllText($"{Application.persistentDataPath}/Tips/Tips.txt"))
            {
                File.WriteAllText($"{Application.persistentDataPath}/Tips/Tips.txt", TipsTextFile.text);
            }
        }
        versionText.text = $"Version: {Application.version}";
        if(firstTimeLoading == true)
        {
            ClearUI();
            startupMenu.SetActive(true);
        }
        else
        {
            ActivateMainMenu();
        }

        FirebaseManager.instance.LoadUserDat();
    }

    private void Update()
    {
        if(startupMenu != null)
        {
            if (startupMenu.activeInHierarchy)
            {
                if (Keyboard.current.anyKey.wasPressedThisFrame)
                {
                    ActivateMainMenu();
                }
            }
        }

        NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
        NFI.CurrencyGroupSeparator = ".";
        NFI.CurrencySymbol = "";
        SpendableCashText.text = $"Cash: ${Convert.ToDecimal(GameManager.instance.PlayerCurrentSpendableCash).ToString("C0", NFI)}";
        OffshoreAccountText.text = $"Offshore Balance: ${Convert.ToDecimal(GameManager.instance.PlayerCurrentOffshoreAccount).ToString("C0", NFI)}";
        CompletedHeistsText.text = $"Heists completed: {GameManager.instance.HeistsCompleted}";

        seconds = (GameManager.instance.PlayTimeInHeistsSeconds % 60);
        minutes = (GameManager.instance.PlayTimeInHeistsSeconds / 60) % 60;
        hours = (GameManager.instance.PlayTimeInHeistsSeconds / 3600);

        PlaytimeText.text = $"Time Spent In Heists: {hours} h, {minutes} min, {seconds} sec";
    }

    private void ClearUI()
    {
        mainMenu.SetActive(false);
        startupMenu.SetActive(false);
        savingAnimationGameObject.SetActive(false);
        offlineHeistSetupMenu.SetActive(false);
        confirmQuitMenu.SetActive(false);
        bugreportMenu.SetActive(false);
        loadingMenu.SetActive(false);
    }

    public void ClearPopUpUI()
    {
        confirmQuitMenu.SetActive(false);
        bugreportMenu.SetActive(false);
    }

    public void OfflineHeist()
    {
        ClearUI();
        offlineHeistSetupMenu.SetActive(true);
    }

    public void ActivateMainMenu()
    {
        ClearUI();
        mainMenu.SetActive(true);
    }

    public void ConfirmQuitMenu()
    {
        ClearPopUpUI();
        confirmQuitMenu.SetActive(true);
    }

    public void BugReportMenu()
    {
        ClearPopUpUI();
        bugreportMenu.SetActive(true);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void changeSceneCall(int _sceneIndex)
    {
        StartCoroutine(changeScene(_sceneIndex));
    }

    private IEnumerator changeScene(int _sceneIndex)
    {
        ClearUI();
        loadingMenu.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(_sceneIndex);
    }
}
