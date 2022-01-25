using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum thermalDrillPosition
{
    AboveBank,
    LeftOfBank1,
    LeftOfBank2
};

public enum playerReadyState
{
    Ready,
    NotReady
};

public enum bodyBagsPosition
{
    AboveBank,
    RightSideOfBank,
    InsideBank,
};

public class BankHeistSetup : MonoBehaviour
{
    public static BankHeistSetup instance;

    [Header("Player State")]
    [SerializeField] private playerReadyState playerState;
    [Space(5)]

    [Header("Assets And Drill Setup")]
    public thermalDrillPosition drillPosition;
    public bodyBagsPosition bodyPosition;
    [Space(5)]

    [Header("Firebase")]
    public FirebaseUser user;
    public FirebaseAuth auth;

    [Header("Text")]
    [SerializeField] private string textBeforeUserName;
    [SerializeField] private TMP_Text user1DisplayText;
    [SerializeField] private TMP_Text user2DisplayText;
    [SerializeField] private TMP_Text user3DisplayText;
    [SerializeField] private TMP_Text user4DisplayText;
    [SerializeField] private string playerStateString;
    [Space(5)]

    [Header("Menus")]
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private GameObject prePlanningSmallMenu;
    [SerializeField] private GameObject reportBugMenu;
    [SerializeField] private GameObject prePlanningMenu;
    [SerializeField] private GameObject MainSetupMenu;
    [SerializeField] private GameObject LoadingMenu;
    [SerializeField] private GameObject MainMenu;
    [Space(5)]

    [Header("Drill Icons")]
    [SerializeField] private GameObject DrillIconAbove;
    [SerializeField] private Button DrillAboveButton;
    [SerializeField] private GameObject DrillIconLeftSide1;
    [SerializeField] private Button DrillLeftSide1Button;
    [SerializeField] private GameObject DrillIconLeftSide2;
    [SerializeField] private Button DrillLeftSide2Button;
    [Space(5)]

    [Header("Body Bags")]
    [SerializeField] private GameObject BodyBagIconAbove;
    [SerializeField] private Button BodyBagAboveButton;
    [SerializeField] private GameObject BodyBagIconRightSide;
    [SerializeField] private Button BodyBagRightSideButton;
    [SerializeField] private GameObject BodyBagIconInsideBank;
    [SerializeField] private Button BodyBagInsideBankButton;
    [Space(5)]

    [Header("Buttons")]
    [SerializeField] private GameObject playerMenuButton;
    [SerializeField] private GameObject FirstDrillPosition;

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
        auth = LobbyManager.instance.auth;
    }

    private void Start()
    {
        user = auth.CurrentUser;
        if (user != null)
        {
            user1DisplayText.text = $"<color=#FF855F>{textBeforeUserName}</color=#FF855F>           <color=#ffffff>{user.DisplayName}           {playerStateString}</color=#ffffff>";
        }
        else
        {
            user1DisplayText.text = $"<color=#FF855F>{textBeforeUserName}</color=#FF855F>           <color=#ffffff>Player           {playerStateString}</color=#ffffff>";
        }
        user2DisplayText.text = $"<color=#00FF68>Player 2</color=#00FF68>           <color=#ffffff>Empty            Ready</color=#ffffff>";
        user3DisplayText.text = $"<color=#81FBFF>Player 3</color=#81FBFF>           <color=#ffffff>Empty            Ready</color=#ffffff>";
        user4DisplayText.text = $"<color=#F597FF>Player 4</color=#F597FF>           <color=#ffffff>Empty            Ready</color=#ffffff>";
        ClearUI();
        ClearDrillIcons();
        ClearBodyBagIcons();
        MainSetupMenu.SetActive(true);
        PlayerMenu();
    }

    private void Update()
    {
        if(LobbyManager.instance != null)
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
    }

    private void ClearUI()
    {
        playerMenu.SetActive(false);
        prePlanningSmallMenu.SetActive(false);
        reportBugMenu.SetActive(false);
        prePlanningMenu.SetActive(false);
        MainSetupMenu.SetActive(false);
    }

    private void ClearDrillIcons()
    {
        DrillIconAbove.SetActive(false);
        DrillIconLeftSide1.SetActive(false);
        DrillIconLeftSide2.SetActive(false);
    }

    private void ClearBodyBagIcons()
    {
        BodyBagIconAbove.SetActive(false);
        BodyBagIconRightSide.SetActive(false);
        BodyBagIconInsideBank.SetActive(false);
    }

    private void PrePlanningThermalDrillClearButtonColors()
    {
        var AboveColors = BodyBagAboveButton.colors;
        AboveColors.normalColor = Color.white;
        BodyBagAboveButton.colors = AboveColors;

        var LeftSide1Colors = DrillLeftSide1Button.colors;
        LeftSide1Colors.normalColor = Color.white;
        DrillLeftSide1Button.colors = LeftSide1Colors;

        var LeftSide2Colors = DrillLeftSide2Button.colors;
        LeftSide2Colors.normalColor = Color.white;
        DrillLeftSide2Button.colors = LeftSide2Colors;
    }

    private void PrePlanningBodyBagClearColors()
    {
        var AboveColors = DrillAboveButton.colors;
        AboveColors.normalColor = Color.white;
        DrillAboveButton.colors = AboveColors;

        var RightSideColors = BodyBagRightSideButton.colors;
        RightSideColors.normalColor = Color.white;
        BodyBagRightSideButton.colors = RightSideColors;

        var InsideBankColors = BodyBagInsideBankButton.colors;
        InsideBankColors.normalColor = Color.white;
        BodyBagInsideBankButton.colors = InsideBankColors;
    }

    public void PlayerMenu()
    {
        ClearUI();
        MainSetupMenu.SetActive(true);
        playerMenu.SetActive(true);
        playerMenuButton.GetComponent<Button>().Select();
    }

    public void PrePlanningSmallMenu()
    {
        ClearUI();
        MainSetupMenu.SetActive(true);
        prePlanningSmallMenu.SetActive(true);
    }

    public void ReportBug()
    {
        ClearUI();
        MainSetupMenu.SetActive(true);
        reportBugMenu.SetActive(true);
    }

    public void PrePlanningMenu()
    {
        ClearUI();
        FirstDrillPosition.GetComponent<Button>().Select();
        DrillIconAbove.SetActive(true);
        prePlanningMenu.SetActive(true);
    }

    public void DrillOverBankPosition()
    {
        ClearDrillIcons();
        PrePlanningThermalDrillClearButtonColors();
        var AboveColors = DrillAboveButton.colors;
        AboveColors.normalColor = AboveColors.selectedColor;
        DrillAboveButton.colors = AboveColors;

        DrillIconAbove.SetActive(true);
        drillPosition = thermalDrillPosition.AboveBank;
    }

    public void DrillPositionLeftOfBank1()
    {
        ClearDrillIcons();
        PrePlanningThermalDrillClearButtonColors();
        var LeftSide1Colors = DrillLeftSide1Button.colors;
        LeftSide1Colors.normalColor = LeftSide1Colors.selectedColor;
        DrillLeftSide1Button.colors = LeftSide1Colors;

        DrillIconLeftSide1.SetActive(true);
        drillPosition = thermalDrillPosition.LeftOfBank1;
    }

    public void DrillPositionLeftOfBank2()
    {
        ClearDrillIcons();
        PrePlanningThermalDrillClearButtonColors();
        var LeftSide2Colors = DrillLeftSide2Button.colors;
        LeftSide2Colors.normalColor = LeftSide2Colors.selectedColor;
        DrillLeftSide2Button.colors = LeftSide2Colors;

        DrillIconLeftSide2.SetActive(true);
        drillPosition = thermalDrillPosition.LeftOfBank2;
    }

    public void BodyBagPositionOverBank()
    {
        ClearBodyBagIcons();
        PrePlanningBodyBagClearColors();
        var AboveColors = BodyBagAboveButton.colors;
        AboveColors.normalColor = AboveColors.selectedColor;
        BodyBagAboveButton.colors = AboveColors;

        BodyBagIconAbove.SetActive(true);
        bodyPosition = bodyBagsPosition.AboveBank;
    }

    public void BodyBagPositionRightSideBank()
    {
        ClearBodyBagIcons();
        PrePlanningBodyBagClearColors();
        var RightSideColors = BodyBagRightSideButton.colors;
        RightSideColors.normalColor = RightSideColors.selectedColor;
        BodyBagRightSideButton.colors = RightSideColors;

        BodyBagIconRightSide.SetActive(true);
        bodyPosition = bodyBagsPosition.RightSideOfBank;
    }

    public void BodyBagPositionInsideBank()
    {
        ClearBodyBagIcons();
        PrePlanningBodyBagClearColors();
        var InsideColors = BodyBagInsideBankButton.colors;
        InsideColors.normalColor = InsideColors.selectedColor;
        BodyBagInsideBankButton.colors = InsideColors;

        BodyBagIconInsideBank.SetActive(true);
        bodyPosition = bodyBagsPosition.InsideBank;
    }

    public void loadHeist(int _sceneIndex)
    {
        StartCoroutine(loadHeistIE(_sceneIndex));
    }

    private IEnumerator loadHeistIE(int _sceneIndex)
    {
        ClearUI();
        MainMenu.SetActive(false);
        LoadingMenu.SetActive(true);

        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(_sceneIndex);
    }
}