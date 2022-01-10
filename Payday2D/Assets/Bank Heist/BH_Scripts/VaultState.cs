using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LocalVaultState
{
    Open,
    Closed
};

public class VaultState : MonoBehaviour
{
    public Animator animator;

    public LocalVaultState _state;

    private void Update()
    {
        if(_state == LocalVaultState.Open)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SetupDrillVault>().enabled = false;
            animator.SetBool("OpenVault", true);
        }
    }
}
