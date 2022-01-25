using System;
using System.Globalization;
using System.Collections;
using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FinishedHeistHandler : MonoBehaviour
{
    public static FinishedHeistHandler instance;

    [Header("Fade In Effect")]
    [SerializeField] private Image fadeInEffect;
    [SerializeField] private bool nextMenu = false;

    [Header("Player Stats")]
    public int BagsCollected;
    public int TotalBagsValue;
    public int XpEarned;
    public int TotalXp;
    public int CiviliansKilled;
    public int GuardsKilled;
    public int HeistBasePayout;
    public int HeistBaseXp;
    public int OffshoreAccountMoneyInt;
    public int SpendableCashMoneyInt;
    public int TotalMoneyEarned;

    [Header("Stats Menu")]
    [SerializeField] private float BagsCollectedFloat;
    [SerializeField] private float TotalBagsValueFloat;
    [SerializeField] private string TotalBagsValueString;
    [SerializeField] private float CiviliansKilledFloat;
    [SerializeField] private float GuardsKilledFloat;
    [SerializeField] private TMP_Text TotalBagsText;
    [SerializeField] private TMP_Text TotalBagsValueText;
    [SerializeField] private TMP_Text TotalCiviliansKilledText;
    [SerializeField] private TMP_Text TotalGuardsKilledText;

    [Header("Money Menu")]
    [SerializeField] private float HeistBasePayoutFloat;
    [SerializeField] private string HeistBasePayoutString;
    [SerializeField] private float SpendableCash;
    [SerializeField] private string SpendableCashString;
    [SerializeField] private float OffshoreAccountMoney;
    [SerializeField] private string OffshoreAccountMoneyString;
    [SerializeField] private TMP_Text HeistBasePayoutText;
    [SerializeField] private TMP_Text BagsValueText;
    [SerializeField] private TMP_Text SpendableCashText;
    [SerializeField] private TMP_Text OffshoreAccountText;

    [Header("Xp Menu")]
    [SerializeField] private float CurrentXpFloat;
    [SerializeField] private string CurrentXpString;
    [SerializeField] private float HeistBaseXPFloat;
    [SerializeField] private string HeistBaseXPString;
    [SerializeField] private float TotalBagsXpFloat;
    [SerializeField] private string TotalBagsXpString;
    [SerializeField] private TMP_Text HeistBaseXpText;
    [SerializeField] private TMP_Text TotalBagsXPText;
    [SerializeField] private TMP_Text EarnedXpText;
    [SerializeField] private bool updateBaseXp = true;
    [SerializeField] private bool updateXp = true;

    [Header("Menus")]
    [SerializeField] private GameObject StatsMenu;
    [SerializeField] private GameObject MoneyMenu;
    [SerializeField] private GameObject XpMenu;
    [Space(5)]
    [SerializeField] private GameObject PressAnyKeyToContinue;

    [Header("Firebase")]
    public FirebaseUser user;
    public FirebaseAuth auth;

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

        if(GameHandler.instance != null)
        {
            user = GameHandler.instance.user;
            auth = GameHandler.instance.auth;
            BagsCollected = GameHandler.instance.BagsCollected;
            XpEarned = GameHandler.instance.XpEarned;
            TotalBagsValue = GameHandler.instance.CollectedLootValue;
            CiviliansKilled = GameHandler.instance.CiviliansKilled;
            GuardsKilled = GameHandler.instance.GuardsKilled;
        }
    }

    private void ClearUI()
    {
        StatsMenu.SetActive(false);
        MoneyMenu.SetActive(false);
        XpMenu.SetActive(false);
        PressAnyKeyToContinue.SetActive(false);
        TotalBagsText.gameObject.SetActive(false);
        TotalBagsValueText.gameObject.SetActive(false);
        TotalCiviliansKilledText.gameObject.SetActive(false);
        TotalGuardsKilledText.gameObject.SetActive(false);
        HeistBasePayoutText.gameObject.SetActive(false);
        BagsValueText.gameObject.SetActive(false);
        SpendableCashText.gameObject.SetActive(false);
        OffshoreAccountText.gameObject.SetActive(false);
        TotalBagsValueFloat = 0;
        EarnedXpText.gameObject.SetActive(false);
        HeistBaseXpText.gameObject.SetActive(false);
        TotalBagsXPText.gameObject.SetActive(false);
    }

    private void Start()
    {
        ClearUI();
        fadeInEffect.gameObject.SetActive(true);
        StartCoroutine("FadeInEffect");
        TotalMoneyEarned = TotalBagsValue + HeistBasePayout;
        OffshoreAccountMoneyInt = (int) (TotalMoneyEarned * 0.6f);
        SpendableCashMoneyInt = (int)(TotalMoneyEarned * 0.25f);
        TotalXp = XpEarned + HeistBaseXp;
    }

    private void Update()
    {
        if(StatsMenu.activeInHierarchy)
            UpdateBagsText();
        if (MoneyMenu.activeInHierarchy)
            UpdateBankHeistBasePayoutText();
        if (XpMenu.activeInHierarchy)
            UpdateBaseHeistXpText();
        if (XpMenu.activeInHierarchy && updateBaseXp == false)
            UpdateTotalBagsXpText();

        if (nextMenu)
        {
            PressAnyKeyToContinue.SetActive(true);
        }
        else
        {
            PressAnyKeyToContinue.SetActive(false);
        }
    }

    private IEnumerator FadeInEffect()
    {
        yield return new WaitForSeconds(1);
        Destroy(fadeInEffect.gameObject);
        StatsMenu.SetActive(true);
        TotalBagsText.gameObject.SetActive(true);
        StopCoroutine("FadeInEffect");
    }

    #region stats menu
    private void UpdateBagsText()
    {
        BagsCollectedFloat = Mathf.MoveTowards(BagsCollectedFloat, BagsCollected, 2 * Time.deltaTime);
        TotalBagsText.text = $"Total Bags: {BagsCollectedFloat.ToString("F0")}";

        if(BagsCollectedFloat == BagsCollected)
        {
            UpdateCollectedValue();
        }
    }

    private void UpdateCollectedValue()
    {
        TotalBagsValueText.gameObject.SetActive(true);
        TotalBagsValueFloat = Mathf.MoveTowards(TotalBagsValueFloat, TotalBagsValue, TotalBagsValue / 5 * Time.deltaTime);
        NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
        NFI.CurrencyGroupSeparator = ".";
        NFI.CurrencySymbol = "";
        TotalBagsValueString = Convert.ToDecimal(TotalBagsValueFloat).ToString("C0", NFI);
        TotalBagsValueText.text = $"Total Bags Value: ${TotalBagsValueString}";

        if(TotalBagsValueFloat == TotalBagsValue)
        {
            UpdateCiviliansKilledText();
        }
    }

    private void UpdateCiviliansKilledText()
    {
        TotalCiviliansKilledText.gameObject.SetActive(true);
        CiviliansKilledFloat = Mathf.MoveTowards(CiviliansKilledFloat, CiviliansKilled, 2 * Time.deltaTime);
        TotalCiviliansKilledText.text = $"Total Civilians Killed: {CiviliansKilledFloat.ToString("F0")}";

        if(CiviliansKilledFloat == CiviliansKilled)
        {
            UpdateGuardsKilledText();
        }
    }

    private void UpdateGuardsKilledText()
    {
        TotalGuardsKilledText.gameObject.SetActive(true);
        GuardsKilledFloat = Mathf.MoveTowards(GuardsKilledFloat, GuardsKilled, 2 * Time.deltaTime);
        TotalGuardsKilledText.text = $"Total Guards Killed: {GuardsKilledFloat.ToString("F0")}";

        if(GuardsKilledFloat == GuardsKilled)
        {
            nextMenu = true;
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                ClearUI();
                MoneyMenu.SetActive(true);
                nextMenu = false;
            }
        }
    }
    #endregion  stats menu

    #region money menu
    private void UpdateBankHeistBasePayoutText()
    {
        HeistBasePayoutText.gameObject.SetActive(true);
        HeistBasePayoutFloat = Mathf.MoveTowards(HeistBasePayoutFloat, HeistBasePayout, HeistBasePayout / 5 * Time.deltaTime);
        NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
        NFI.CurrencyGroupSeparator = ".";
        NFI.CurrencySymbol = "";
        HeistBasePayoutString = Convert.ToDecimal(HeistBasePayoutFloat).ToString("C0", NFI);
        HeistBasePayoutText.text = $"Heist Base Payout: ${HeistBasePayoutString}";

        if(HeistBasePayoutFloat == HeistBasePayout)
        {
            UpdateTotalBagsValueText();
        }
    }

    private void UpdateTotalBagsValueText()
    {
        BagsValueText.gameObject.SetActive(true);
        TotalBagsValueFloat = Mathf.MoveTowards(TotalBagsValueFloat, TotalBagsValue, TotalBagsValue / 5 * Time.deltaTime);
        NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
        NFI.CurrencyGroupSeparator = ".";
        NFI.CurrencySymbol = "";
        TotalBagsValueString = Convert.ToDecimal(TotalBagsValueFloat).ToString("C0", NFI);
        BagsValueText.text = $"Total Bags Value: ${TotalBagsValueString}";

        if(TotalBagsValueFloat == TotalBagsValue)
        {
            UpdateSpendableCashText();
        }
    }

    private void UpdateSpendableCashText()
    {
        SpendableCashText.gameObject.SetActive(true);
        SpendableCash = Mathf.MoveTowards(SpendableCash, SpendableCashMoneyInt, SpendableCashMoneyInt / 5 * Time.deltaTime);
        NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
        NFI.CurrencyGroupSeparator = ".";
        NFI.CurrencySymbol = "";
        SpendableCashString = Convert.ToDecimal(SpendableCash).ToString("C0", NFI);
        SpendableCashText.text = $"Spendable Cash: ${SpendableCashString}";

        if(SpendableCash == SpendableCashMoneyInt)
        {
            UpdateOffshoreAccountText();
        }
    }

    private void UpdateOffshoreAccountText()
    {
        OffshoreAccountText.gameObject.SetActive(true);
        OffshoreAccountMoney = Mathf.MoveTowards(OffshoreAccountMoney, OffshoreAccountMoneyInt, OffshoreAccountMoneyInt / 5 * Time.deltaTime);
        NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
        NFI.CurrencyGroupSeparator = ".";
        NFI.CurrencySymbol = "";
        OffshoreAccountMoneyString = Convert.ToDecimal(OffshoreAccountMoney).ToString("C0", NFI);
        OffshoreAccountText.text = $"Money Sent To Offshore Account: ${OffshoreAccountMoneyString}";

        if (OffshoreAccountMoney == OffshoreAccountMoneyInt)
        {
            nextMenu = true;
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                ClearUI();
                XpMenu.SetActive(true);
                nextMenu = false;
            }
        }
    }
    #endregion money menu

    #region xp menu
    private void UpdateBaseHeistXpText()
    {
        if(updateBaseXp == true)
        {
            EarnedXpText.gameObject.SetActive(true);
            HeistBaseXpText.gameObject.SetActive(true);
            HeistBaseXPFloat = Mathf.MoveTowards(HeistBaseXPFloat, HeistBaseXp, HeistBaseXp / 5 * Time.deltaTime);
            CurrentXpFloat = Mathf.MoveTowards(CurrentXpFloat, HeistBaseXp, HeistBaseXp / 5 * Time.deltaTime);
            NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
            NFI.CurrencyGroupSeparator = ".";
            NFI.CurrencySymbol = "";

            CurrentXpString = Convert.ToDecimal(CurrentXpFloat).ToString("C0", NFI);
            HeistBaseXPString = Convert.ToDecimal(HeistBaseXPFloat).ToString("C0", NFI);

            EarnedXpText.text = $"Earned XP: {CurrentXpString}";
            HeistBaseXpText.text = $"Heist Base XP: {HeistBaseXPString}";
        }

        if(CurrentXpFloat == HeistBaseXp)
        {
            updateBaseXp = false;
        }
    }

    private void UpdateTotalBagsXpText()
    {
        if(updateXp == true)
        {
            TotalBagsXPText.gameObject.SetActive(true);
            TotalBagsXpFloat = Mathf.MoveTowards(TotalBagsXpFloat, XpEarned, XpEarned / 5 * Time.deltaTime);

            NumberFormatInfo NFI = new CultureInfo("en-US", false).NumberFormat;
            NFI.CurrencyGroupSeparator = ".";
            NFI.CurrencySymbol = "";
            CurrentXpString = Convert.ToDecimal(CurrentXpFloat).ToString("C0", NFI);
            TotalBagsXpString = Convert.ToDecimal(TotalBagsXpFloat).ToString("C0", NFI);

            TotalBagsXPText.text = $"Total Bags XP: {TotalBagsXpString}";

            if (TotalBagsXpFloat == XpEarned)
            {
                CurrentXpFloat = Mathf.MoveTowards(CurrentXpFloat, TotalXp, TotalXp / 5 * Time.deltaTime);
                EarnedXpText.text = $"Earned XP: {CurrentXpString}";
                CurrentXpString = Convert.ToDecimal(CurrentXpFloat).ToString("C0", NFI);
            }

            if(CurrentXpFloat == TotalXp)
            {
                updateXp = false;
            }
        }
    }

    private void UpdateLevel()
    {
        //TODO Update Levels
        //Load Back To Main Menu
    }
    #endregion xp menu
}