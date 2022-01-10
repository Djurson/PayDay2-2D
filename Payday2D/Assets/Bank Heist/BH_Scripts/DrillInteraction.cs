using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrillState
{
    Setup,
    Drilling,
    Failure,
    None
};

public class DrillInteraction : MonoBehaviour
{
    public VaultState vault;

    public GameObject ThermalDrill;
    public GameObject ThermalDrillHighLight;
    public GameObject DrillingScreen;
    public GameObject FailureScreen;

    public DrillState drillState;

    public float interactionTime;

    public float DrillTime;

    public int[] FailureTimesSeconds;

    private int failTimes;

    public int minFailTimes;

    private void Start()
    {
        failTimes = Random.Range(minFailTimes, (int) (DrillTime / 50));

        FailureTimesSeconds = new int[failTimes];

        for (int i = 0; i < FailureTimesSeconds.Length; i++)
        {
            FailureTimesSeconds[i] = (int) Random.Range(0, DrillTime);
        }
    }

    private void Update()
    {
        if(drillState != DrillState.Setup)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if(DrillTime <= 0)
        {
            StopCoroutine("DrillTimeCountDown");
            drillState = DrillState.None;
            vault._state = LocalVaultState.Open;
            Destroy(this.gameObject);
        }
    }

    public void startDrill()
    {
        drillState = DrillState.Drilling;

        FailureScreen.SetActive(false);
        DrillingScreen.SetActive(true);

        if(ThermalDrillHighLight != null)
        {
            Destroy(ThermalDrillHighLight);
        }

        ThermalDrill.SetActive(true);

        StartCoroutine("DrillTimeCountDown");
    }

    private IEnumerator DrillTimeCountDown()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (true)
        {
            yield return delay;

            DrillTime -= 1;

            for (int i = 0; i < FailureTimesSeconds.Length; i++)
            {
                if (DrillTime == FailureTimesSeconds[i])
                {
                    drillState = DrillState.Failure;
                    FailureScreen.SetActive(true);
                    DrillingScreen.SetActive(false);

                    StopCoroutine("DrillTimeCountDown");
                }
            }
        }
    }
}
