using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler instance;

    [Header("Menus")]
    [SerializeField] private GameObject startupMenu;
    [SerializeField] private GameObject mainMenu;

    [Header("Saving Logic")]
    [SerializeField] private GameObject savingAnimationGameObject;

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
        ClearUi();
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
    }

    private void ClearUi()
    {
        mainMenu.SetActive(false);
        startupMenu.SetActive(false);
        savingAnimationGameObject.SetActive(false);
    }

    public void ActivateMainMenu()
    {
        ClearUi();
        mainMenu.SetActive(true);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
