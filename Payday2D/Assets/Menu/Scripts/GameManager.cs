using UnityEngine;
using UnityEngine.SceneManagement;

public enum HeistState
{
    Stealth,
    Loud,
    Failed,
    Paused
};

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Purchased Assets")]
    public bool BodyBags;
    public bool ExpertDriver;
    private bool LoadedHeist = false;

    [Header("Current Heist")]
    public HeistState heistState;
    public int CollectedLootValue;
    public int XpEarned;
    public int BagsCollected;
    public int CiviliansKilled;
    public int GuardsKilled;
    public int MoneyTakenForKillingCivilians;
    public int HeistBasePayout;
    public int HeistBaseXp;

    [Header("Player Data")]
    public int PlayerCurrentSpendableCash;
    public int PlayerCurrentOffshoreAccount;
    public int HeistsCompleted;
    public int PlayTimeInHeistsSeconds;

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

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3 && LoadedHeist == false)
        {
            HeistBaseXp = 2250;
            HeistBasePayout = 32500;
            LoadedHeist = true;
        } else if(SceneManager.GetActiveScene().buildIndex != 3 || SceneManager.GetActiveScene().buildIndex != 4)
        {
            LoadedHeist = false;
        }
    }

    public void ChangeScene(int _sceneIndex)
    {
        SceneManager.LoadSceneAsync(_sceneIndex);
    }
}
