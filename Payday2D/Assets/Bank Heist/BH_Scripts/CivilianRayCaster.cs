using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum civilianState
{
    Normal,
    Panicing,
    Intimidated,
    Calling
};

public class CivilianRayCaster : MonoBehaviour
{
    public int CivilianIndex;

    public civilianState localState;

    public float RayCastDistance;

    public bool seeingPlayer;

    private float localDetection;

    public SpriteRenderer baseSr;

    public Collider2D[] collidersNearBy;

    private RaycastHit2D hit;

    public LayerMask[] ignoreLayerMask;

    private Collider2D playerCollider;

    private void Start()
    {
        for(int i = 0; i < ignoreLayerMask.Length; i++)
        {
            ignoreLayerMask[i] = Physics2D.IgnoreRaycastLayer;
        }
        this.gameObject.name = "Civilian: " + CivilianIndex.ToString();
    }

    private void Update()
    {
        collidersNearBy = Physics2D.OverlapCircleAll(this.transform.position, RayCastDistance);

        for(int j = 0; j < collidersNearBy.Length; j++)
        {
            if (collidersNearBy[j].gameObject.CompareTag("Player"))
            {
                playerCollider = (Collider2D)collidersNearBy[j];

                Debug.Log(Dot);
                //if (Dot > 0.7f)
                //{
                //    hit = Physics2D.Raycast(this.transform.position, dir);

                //    if (hit.collider.CompareTag("Player"))
                //    {
                //        seeingPlayer = true;
                //        Debug.Log(gameObject.name + " seeing player = true");
                //    }
                //    else
                //        seeingPlayer = false;
                //}
            }
        }
    }
}
