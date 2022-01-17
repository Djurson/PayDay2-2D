using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;

public enum thermalDrillPosition
{
    AboveBank,
    LeftOfBank,
    BelowBank
};

public enum playerReadyState
{
    Ready,
    NotReady
};

public class BankHeistSetup : MonoBehaviour
{
    public static BankHeistSetup instance;

    [Header("Player State")]
    [SerializeField] private playerReadyState playerState;

    [Header("Assets And Drill Setup")]
    public thermalDrillPosition drillPosition;

    [Header("Firebase")]
    public FirebaseUser user;

    [Header("Buttons")]
    [SerializeField] private Button playerButton;
    [SerializeField] private Button prePlanningButton;
    [SerializeField] private Button reportBugButton;

    [Header("Text")]
    [SerializeField] private string textBeforeUserName;
    [SerializeField] private TMP_Text user1DisplayText;
    [SerializeField] private TMP_Text user2DisplayText;
    [SerializeField] private TMP_Text user3DisplayText;
    [SerializeField] private TMP_Text user4DisplayText;
    [SerializeField] private string playerStateString = "Not Ready";

    [Header("Menus")]
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private GameObject prePlanningSmallMenu;
    [SerializeField] private GameObject reportBugMenu;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        user = LobbyManager.instance.user;
        user1DisplayText.text = $"<color=#FF855F>{textBeforeUserName}</color=#FF855F>           <color=#ffffff>{user.DisplayName}           {playerStateString}</color=#ffffff>";
        user2DisplayText.text = $"<color=#00FF68>Player 2</color=#00FF68>           <color=#ffffff>Empty            Ready</color=#ffffff>";
        user3DisplayText.text = $"<color=#81FBFF>Player 2</color=#81FBFF>           <color=#ffffff>Empty            Ready</color=#ffffff>";
        user4DisplayText.text = $"<color=#F597FF>Player 2</color=#F597FF>           <color=#ffffff>Empty            Ready</color=#ffffff>";
    }

    private void Update()
    {
        if (playerState != playerReadyState.Ready)
        {
            user1DisplayText.text = $"<color=#FF855F>{textBeforeUserName}</color=#FF855F>           <color=#ffffff>{user.DisplayName}           {playerStateString}</color=#ffffff>";
        }
        else
        {
            user1DisplayText.text = $"<color=#FF855F>{textBeforeUserName}</color=#FF855F>           <color=#ffffff>{user.DisplayName}           {playerState}</color=#ffffff>";
        }
    }

    private void ClearUI()
    {
        playerMenu.SetActive(false);
        prePlanningSmallMenu.SetActive(false);
        reportBugMenu.SetActive(false);
        var playerButtonNormalColor = playerButton.colors.normalColor;
    }

    public void PlayerMenu()
    {
        ClearUI();
        playerMenu.SetActive(true);
        currentSelectedMenu = currentMenu.Players;
    }

    public void PrePlanningSmallMenu()
    {
        ClearUI();
        prePlanningSmallMenu.SetActive(true);
        playerMenu.SetActive(true);
        currentSelectedMenu = currentMenu.Preplanning;
    }

    public void ReportBug()
    {
        ClearUI();
        reportBugMenu.SetActive(true);
        currentSelectedMenu = currentMenu.ReportBug;
    }
}