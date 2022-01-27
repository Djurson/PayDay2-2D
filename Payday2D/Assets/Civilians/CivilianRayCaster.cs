using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum civilianState
{
    Normal,
    Panicing,
    Intimidated,
    FollowingPlayer,
    CableTied
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

    public float interaction = 2;

    private Collider2D PlayerRayCastCollider;
    private Collider2D BagRayCastCollider;
    private Collider2D BodyRayCollider;
    private Collider2D CivilianCollider;

    private RaycastHit2D hit;
    private RaycastHit2D hitBag;
    private RaycastHit2D hitBody;
    private RaycastHit2D hitCivilian;

    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private LayerMask BagLayer;
    [SerializeField] private LayerMask BodyLayer;
    [SerializeField] private LayerMask CivilianLayer;

    private Vector3 normalizedDir;
    public PlayerDetection localPlayerDetection;

    public GameObject detectionIcon;

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
        if (localPlayerDetection != null)
        {
            if (localDetection > 0)
            {
                if (hasAdded == false)
                {
                    localPlayerDetection.detectedCivilians.Add(this);
                    hasAdded = true;
                }
            }
            else if (localDetection == 0)
                localPlayerDetection.detectedCivilians.Remove(this);

            if (localState != civilianState.CableTied && localState != civilianState.FollowingPlayer)
            {
                rayCast();
                checkRayCast();
            }
            else
            {
                localDetection = 0;
                localPlayerDetection.detectedCivilians.Remove(this);
            }

            if (detectionIcon != null)
            {
                if (localDetection == 100)
                {
                    detectionIcon.SetActive(true);
                }
                if (localDetection != 100)
                    detectionIcon.SetActive(false);
            }
        }
        else
            Debug.LogError("Player Detection Not Set");
    }

    private void rayCast()
    {
        PlayerRayCastCollider = Physics2D.OverlapCircle(transform.position, RayCastDistance, PlayerLayer.value);
        BagRayCastCollider = Physics2D.OverlapCircle(transform.position, RayCastDistance, BagLayer.value);
        BodyRayCollider = Physics2D.OverlapCircle(transform.position, RayCastDistance, BodyLayer.value);
        CivilianCollider = Physics2D.OverlapCircle(transform.position, RayCastDistance, CivilianLayer.value);

        if (PlayerRayCastCollider != null)
        {
            normalizedDir = Vector3.Normalize(PlayerRayCastCollider.gameObject.transform.position - this.transform.position);

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
        else if (BagRayCastCollider != null && BagRayCastCollider.gameObject.CompareTag("Bag"))
        {
            normalizedDir = Vector3.Normalize(BagRayCastCollider.gameObject.transform.position - this.transform.position);

            hitBag = Physics2D.Raycast(this.transform.position, normalizedDir, RayCastDistance);

            if (hitBag.collider != null)
            {
                if (hitBag.collider.CompareTag("Bag") || hit.collider.CompareTag("BodyBag"))
                {
                    seeingPlayer = true;
                }
                if (!hitBag.collider.CompareTag("Bag") && !hit.collider.CompareTag("BodyBag"))
                    seeingPlayer = false;
            }
            else
                seeingPlayer = false;
        } else if(BodyRayCollider != null && BodyRayCollider.gameObject.CompareTag("DeadCivilian") || BodyRayCollider != null && BodyRayCollider.gameObject.CompareTag("DeadGuard"))
        {
            normalizedDir = Vector3.Normalize(BodyRayCollider.gameObject.transform.position - this.transform.position);

            hitBody = Physics2D.Raycast(this.transform.position, normalizedDir, RayCastDistance);

            if (hitBody.collider != null && hitBody.collider.gameObject != null)
            {
                if (hitBody.collider.gameObject.CompareTag("DeadCivilian") || hitBody.collider.gameObject.CompareTag("DeadGuard"))
                {
                    seeingPlayer = true;
                }
                if (!hitBody.collider.gameObject.CompareTag("DeadCivilian") && !hitBody.collider.gameObject.CompareTag("DeadGuard"))
                    seeingPlayer = false;
            }
            else
                seeingPlayer = false;
        }
        else if (CivilianCollider != null)
        {
            normalizedDir = Vector3.Normalize(PlayerRayCastCollider.gameObject.transform.position - this.transform.position);

            hitCivilian = Physics2D.Raycast(this.transform.position, normalizedDir, RayCastDistance);

            if (hitCivilian.collider != null)
            {
                if (hitCivilian.collider.CompareTag("Civilian") && hitCivilian.collider.GetComponent<CivilianRayCaster>().localState == civilianState.Panicing || hitCivilian.collider.CompareTag("Civilian") && hitCivilian.collider.GetComponent<CivilianRayCaster>().localState == civilianState.CableTied || hitCivilian.collider.CompareTag("Civilian") && hitCivilian.collider.GetComponent<CivilianRayCaster>().localState == civilianState.FollowingPlayer)
                {
                    seeingPlayer = true;
                }
                if (!hitCivilian.collider.CompareTag("Civilian") && hitCivilian.collider.GetComponent<CivilianRayCaster>().localState == civilianState.Panicing || hitCivilian.collider.CompareTag("Civilian") && hitCivilian.collider.GetComponent<CivilianRayCaster>().localState == civilianState.CableTied || hitCivilian.collider.CompareTag("Civilian") && hitCivilian.collider.GetComponent<CivilianRayCaster>().localState == civilianState.FollowingPlayer)
                    seeingPlayer = false;
            }
        } else 
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
            if(hit.distance < 2f)
                localDetection = Mathf.MoveTowards(localDetection, 100, 150 * Time.deltaTime);

            if(hitBag.collider != null)
            {
                localDetection = Mathf.MoveTowards(localDetection, 100, 10 * Time.deltaTime);
            } else if(hitBody.collider != null)
            {
                localDetection = Mathf.MoveTowards(localDetection, 100, 50 * Time.deltaTime);
            }
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
