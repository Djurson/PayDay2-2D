using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupDrillVault : MonoBehaviour
{
    public GameObject ThermalDrill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DrillBag"))
        {
            ThermalDrill.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}
