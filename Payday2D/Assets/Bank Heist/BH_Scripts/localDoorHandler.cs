using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum doorState
{
    Open,
    Locked,
    FullyLocked,
    Closed
};

public class localDoorHandler : MonoBehaviour
{
    public Animator animator;

    public doorState localDoorState;

    public float interactionTime = 5f;

    public void OpenDoor()
    {
        animator.SetBool("IsOpened", true);
        localDoorState = doorState.Open;
    }
}
