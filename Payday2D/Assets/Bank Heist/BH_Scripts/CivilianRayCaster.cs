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

    private Vector3 normalizedDir;
    private Vector2 civillianForward;
    private float dot;

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
        if (localDetection > 0)
            Debug.Log(localDetection);

        rayCast();
        checkRayCast();
    }

    private void rayCast()
    {
        collidersNearBy = Physics2D.OverlapCircleAll(this.transform.position, RayCastDistance);

        for (int j = 0; j < collidersNearBy.Length; j++)
        {
            if (collidersNearBy[j].gameObject.CompareTag("Player"))
            {
                civillianForward = transform.TransformDirection(Vector2.up);
                normalizedDir = Vector3.Normalize(collidersNearBy[j].transform.position - this.transform.position);

                dot = Vector2.Dot(civillianForward, normalizedDir);

                if (dot > 0)
                {
                    hit = Physics2D.Raycast(this.transform.position, normalizedDir, RayCastDistance);

                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            seeingPlayer = true;
                        }
                        else
                            seeingPlayer = false;
                    }
                    else
                        seeingPlayer = false;
                }
            }
            else
                seeingPlayer = false;
        }
    }

    private void checkRayCast()
    {
        if(seeingPlayer == true && localDetection != 100)
        {
            if (hit.distance < 5f)
                localDetection = Mathf.MoveTowards(localDetection, 100, 50 * Time.deltaTime);

            if (hit.distance > 5f)
                localDetection = Mathf.MoveTowards(localDetection, 100, 10 * Time.deltaTime);
        }

        if(seeingPlayer == false && localDetection < 100)
        {
            localDetection = Mathf.MoveTowards(localDetection, 0, Time.deltaTime);
        }
    }
}
