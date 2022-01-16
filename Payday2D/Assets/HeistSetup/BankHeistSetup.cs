using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum thermalDrillPosition
{
    AboveBank,
    LeftOfBank,
    BelowBank
};

public class BankHeistSetup : MonoBehaviour
{
    public static BankHeistSetup instance;

    public thermalDrillPosition drillPosition;

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
}