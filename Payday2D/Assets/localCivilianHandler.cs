using UnityEngine;
using Pathfinding;
using System.Collections;

public enum CivilianHealth
{
    Alive,
    Dead
};

public class localCivilianHandler : MonoBehaviour
{
    [Header("Civilian Information")]
    [SerializeField] private float health;
    [SerializeField] private int civilianIndex;
    [SerializeField] private SpriteRenderer BaseSpriteRenderer;
    public CivilianHealth state;
    public float interactionTime = 3f;

    [Header("Dead Civilian")]
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private bool hasSentInfo = false;

    [Header("Script Refrences")]
    [SerializeField] private CivilianRayCaster rayCastScript;
    [SerializeField] private AiPathFindingRandomTargetChooser targetChooserScript;
    [SerializeField] private AIDestinationSetter targetSetterScript;
    [SerializeField] private AIPath pathfindingScript;
    private Rigidbody2D rb;

    private bool callingCops;
    private float callingCopsFloat;

    private Transform player;

    private void Start()
    {
        civilianIndex = GetComponent<CivilianRayCaster>().CivilianIndex;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        health = Mathf.Clamp(health, 0, 100);

        if (health == 0)
        {
            if(hasSentInfo == false && GameManager.instance != null)
            {
                GameManager.instance.CiviliansKilled += 1;
                GameManager.instance.MoneyTakenForKillingCivilians += 2250;
                hasSentInfo = true;
            }
            rayCastScript.localDetection = 0;
            rayCastScript.localPlayerDetection.detectedCivilians.Remove(rayCastScript);
            Destroy(rayCastScript.detectionIcon.gameObject);
            targetChooserScript.enabled = false;
            targetSetterScript.enabled = false;
            pathfindingScript.enabled = false;
            BaseSpriteRenderer.sprite = deadSprite;
            state = CivilianHealth.Dead;
            rayCastScript.enabled = false;
            rb.mass = 1000000;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            gameObject.tag = "DeadCivilian";
            gameObject.layer = LayerMask.NameToLayer("DeadBody");
            StopCoroutine("WaitForRandomSeconds");
        }
        else
            state = CivilianHealth.Alive;

        if(rayCastScript.localDetection == 100 && rayCastScript.localState != civilianState.CableTied && rayCastScript.localState != civilianState.FollowingPlayer)
        {
            rayCastScript.localState = civilianState.Panicing;
            targetChooserScript.enabled = false;
            targetSetterScript.enabled = false;
            pathfindingScript.enabled = false;
            StartCoroutine("WaitForRandomSeconds");
        }

        if (rayCastScript.localState == civilianState.CableTied)
        {
            targetChooserScript.enabled = false;
            targetSetterScript.enabled = false;
            pathfindingScript.enabled = false;
            StopCoroutine("WaitForRandomSeconds");
            callingCopsFloat = 0;
            callingCops = false;
        }
        else if (rayCastScript.localState == civilianState.FollowingPlayer)
        {
            targetChooserScript.enabled = false;
            targetSetterScript.enabled = true;
            targetSetterScript.target = player;
            pathfindingScript.enabled = true;
            StopCoroutine("WaitForRandomSeconds");
            callingCopsFloat = 0;
            callingCops = false;
        }

        if (callingCops == true && state == CivilianHealth.Alive && rayCastScript.localState != civilianState.Panicing && rayCastScript.localState != civilianState.FollowingPlayer)
        {
            callingCopsFloat = Mathf.MoveTowards(callingCopsFloat, 2, Time.deltaTime);
        }

        if(callingCopsFloat == 2)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.heistState = HeistState.Failed;
            }
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }

    private IEnumerator WaitForRandomSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            callingCops = true;
        }
    }
}
