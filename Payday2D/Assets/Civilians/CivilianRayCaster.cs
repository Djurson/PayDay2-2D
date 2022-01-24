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

    public float localDetection;

    private Collider2D PlayerRayCastCollider;
    private Collider2D BagRayCastCollider;
    private RaycastHit2D BodyRayCast;

    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private LayerMask BagLayer;

    private RaycastHit2D hit;

    private Vector3 normalizedDir;
    private Vector2 civillianForward;
    private float dot;
    public PlayerDetection localPlayerDetection;

    public GameObject detectionIcon;

    public GameObject player;

    private bool hasAdded = false;

    private void Start()
    {
        this.gameObject.name = "Civilian: " + CivilianIndex.ToString();
        localPlayerDetection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDetection>();
    }

    private void Update()
    {
        checkState();
    }

    private void checkState()
    {
        if (localDetection > 0)
        {
            if(hasAdded == false)
            {
                localPlayerDetection.detectedCivilians.Add(this);
                hasAdded = true;
            }
        }
        else if (localDetection == 0)
            localPlayerDetection.detectedCivilians.Remove(this);

        if (localState != civilianState.CableTied)
        {
            rayCast();
            checkRayCast();
        }
        else
            localDetection = 0;

        if(detectionIcon != null)
        {
            if (localDetection == 100)
                detectionIcon.SetActive(true);
            if (localDetection != 100)
                detectionIcon.SetActive(false);
        }
    }

    private void rayCast()
    {
        PlayerRayCastCollider = Physics2D.OverlapCircle(transform.position, RayCastDistance, PlayerLayer.value);
        BagRayCastCollider = Physics2D.OverlapCircle(transform.position, RayCastDistance, BagLayer.value);

        if (PlayerRayCastCollider != null)
        {
            civillianForward = transform.TransformDirection(Vector2.up);
            normalizedDir = Vector3.Normalize(PlayerRayCastCollider.gameObject.transform.position - this.transform.position);

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
                    if (!hit.collider.CompareTag("Player"))
                        seeingPlayer = false;
                }
                else
                    seeingPlayer = false;
            }
        }
        else if (BagRayCastCollider != null && BagRayCastCollider.gameObject.CompareTag("Bag"))
        {
            civillianForward = transform.TransformDirection(Vector2.up);
            normalizedDir = Vector3.Normalize(BagRayCastCollider.gameObject.transform.position - this.transform.position);

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
                    if (!hit.collider.CompareTag("Player"))
                        seeingPlayer = false;
                }
                else
                    seeingPlayer = false;
            }
        }
        else
        {
            seeingPlayer = false;
            player = null;
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
            if(hit.distance < 2f)
                localDetection = Mathf.MoveTowards(localDetection, 100, 150 * Time.deltaTime);
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
