using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum civilianState
{
    Normal,
    Panicing,
    Intimidated,
    CableTied,
    Calling
};

public class CivilianRayCaster : MonoBehaviour
{
    public int CivilianIndex;

    public civilianState localState;

    public float RayCastDistance;

    public bool seeingPlayer;
    public bool Hearing;
    public float distanceToHearing;

    private float localDetection;

    public SpriteRenderer baseSr;

    public Collider2D[] collidersNearBy;

    private RaycastHit2D hit;

    public LayerMask[] ignoreLayerMask;

    private Vector3 normalizedDir;
    private Vector2 civillianForward;
    private float dot;
    PlayerDetection localPlayerDetection;

    private GameObject player;

    private void Start()
    {
        for(int i = 0; i < ignoreLayerMask.Length; i++)
        {
            ignoreLayerMask[i] = Physics2D.IgnoreRaycastLayer;
        }
        this.gameObject.name = "Civilian: " + CivilianIndex.ToString();
        localPlayerDetection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDetection>();
    }

    private void Update()
    {
        if (localDetection > 0)
        {
            localPlayerDetection.detectedCivilians.Add(this);
            if (localPlayerDetection.detection != 100 && localState != civilianState.CableTied)
                localPlayerDetection.detection = localDetection;
        } else if(localDetection == 0)
            localPlayerDetection.detectedCivilians.Remove(this);

        if (localState != civilianState.CableTied)
        {
            rayCast();
            checkRayCast();
        }
        else
            localDetection = 0;
    }

    private void rayCast()
    {
        if(player == null)
        {
            collidersNearBy = Physics2D.OverlapCircleAll(this.transform.position, RayCastDistance);

            for (int j = 0; j < collidersNearBy.Length; j++)
            {
                if (collidersNearBy[j].gameObject.CompareTag("Player"))
                {
                    player = collidersNearBy[j].gameObject;
                }
            }
        }

        if(player != null)
        {
            civillianForward = transform.TransformDirection(Vector2.up);
            normalizedDir = Vector3.Normalize(player.transform.position - this.transform.position);

            dot = Vector2.Dot(civillianForward, normalizedDir);

            if (dot > -0.77f)
            {
                hit = Physics2D.Raycast(this.transform.position, normalizedDir, RayCastDistance);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        seeingPlayer = true;
                    }
                    if(!hit.collider.CompareTag("Player"))
                        seeingPlayer = false;
                }
                else
                    seeingPlayer = false;
            }
        }
        else
            seeingPlayer = false;
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

        if (Hearing == true)
        {
            if (distanceToHearing > 15)
                localDetection = Mathf.MoveTowards(localDetection, 100, 15 * Time.deltaTime);
            if (distanceToHearing < 15)
                localDetection = Mathf.MoveTowards(localDetection, 100, 50 * Time.deltaTime);
        }

        if (seeingPlayer == false && localDetection < 100 && Hearing == false)
        {
            localDetection = Mathf.MoveTowards(localDetection, 0, 5 * Time.deltaTime);
        }
    }
}
