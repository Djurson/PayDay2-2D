using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public enum HeistState
{
    Stealth,
    Loud
};

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    [Header("Purchased Assets")]
    public bool BodyBags;
    public bool ExpertDriver;

    [Header("Firebase")]
    public FirebaseUser user;
    public FirebaseAuth auth;

    [Header("Current Heist")]
    public HeistState state;
    public int CollectedLootValue;
    public int XpEarned;
    public int BagsCollected;
    public int CiviliansKilled;
    public int GuardsKilled;
    public int MoneyTakenForKillingCivilians;

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

        if(BankHeistSetup.instance != null)
        {
            auth = BankHeistSetup.instance.auth;
        }
    }

    private void Start()
    {
        if(auth != null)
        {
            user = auth.CurrentUser;
        }
        //TODO: Get if the player has purchased any assets
    }
}
