using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    [Header("Purchased Assets")]
    public bool BodyBags;
    public bool ExpertDriver;

    [Header("Firebase")]
    public FirebaseUser user;
    public FirebaseAuth auth;

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
        auth = BankHeistSetup.instance.auth;
    }

    private void Start()
    {
        user = auth.CurrentUser;
        //TODO: Get if the player has purchased any assets
    }
}
