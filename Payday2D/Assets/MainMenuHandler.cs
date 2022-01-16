using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        ClearUI();
        startupMenu.SetActive(true);
    }

    private void Update()
    {
        if (startupMenu.activeInHierarchy)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                ActivateMainMenu();
            }
        }

        playtimeText.text = $"Playtime: {PlaytimeHandler.instance.hours} Hours {PlaytimeHandler.instance.minutes} Minutes";
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
        yield return new WaitForSeconds(12);
        SceneManager.LoadSceneAsync(_sceneIndex);
    }
}
