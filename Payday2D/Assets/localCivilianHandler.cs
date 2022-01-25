using UnityEngine;
using Pathfinding;

public enum CivilianState
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
    public CivilianState civilianState;
    public float interactionTime = 3f;

    [Header("Dead Civilian")]
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private LayerMask DeadBodyLayer;
    [SerializeField] private bool hasSentInfo = false;

    [Header("Script Refrences")]
    [SerializeField] private CivilianRayCaster rayCastScript;
    [SerializeField] private AiPathFindingRandomTargetChooser targetChooserScript;
    [SerializeField] private AIDestinationSetter targetSetterScript;
    [SerializeField] private AIPath pathfindingScript;
    [SerializeField] private civilianRandomizer randomizerScript;
    [SerializeField] private CapsuleCollider2D CapsuleTrigger;
    private Rigidbody2D rb;

    private void Start()
    {
        civilianIndex = GetComponent<CivilianRayCaster>().CivilianIndex;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, 100);

        if (health == 0)
        {
            if(hasSentInfo == false)
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
            randomizerScript.enabled = false;
            BaseSpriteRenderer.sprite = deadSprite;
            civilianState = CivilianState.Dead;
            rayCastScript.enabled = false;
            Destroy(CapsuleTrigger);
            rb.mass = 1000000;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            gameObject.tag = "DeadCivilian";
            gameObject.layer = LayerMask.NameToLayer("DeadBody");
        }
        else
            civilianState = CivilianState.Alive;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }
}
