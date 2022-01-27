using UnityEngine;
using System.Collections;
using Pathfinding;

public class LocalGuardHandler : MonoBehaviour
{
    [Header("Guard Information")]
    public float health;
    [SerializeField] private int GuardIndex;
    [SerializeField] private SpriteRenderer BaseSpriteRenderer;
    public CivilianHealth state;
    public float interactionTime = 3f;
    public bool hasSetPager = false;
    public bool answeringPager;
    private float PagerFloat;
    public float PagerInteraction = 5f;

    [Header("Dead Guard")]
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private bool hasSentInfo = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject Outline;

    [Header("Script Refrences")]
    [SerializeField] private GuardRayCaster rayCastScript;
    [SerializeField] private GuardPathRandomizer targetChooserScript;
    [SerializeField] private AIDestinationSetter targetSetterScript;
    [SerializeField] private AIPath pathfindingScript;
    [SerializeField] private playerInteraction interaction;
    private Rigidbody2D rb;

    private Transform player;

    private void Start()
    {
        Outline.SetActive(false);
        GuardIndex = GetComponent<GuardRayCaster>().GuardIndex;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        interaction = player.GetComponent<playerInteraction>();
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, 100);

        if (health == 0)
        {
            if (hasSentInfo == false && GameManager.instance != null)
            {
                GameManager.instance.GuardsKilled += 1;
                GameManager.instance.XpEarned += 100;
                hasSentInfo = true;
            }
            rayCastScript.localDetection = 0;
            rayCastScript.localPlayerDetection.detectedGuards.Remove(rayCastScript);
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
            gameObject.tag = "DeadGuard";
            gameObject.layer = LayerMask.NameToLayer("DeadBody");
            if(hasSetPager == false && PagerFloat < 10f && answeringPager == false)
            {
                Outline.SetActive(true);
                animator.SetBool("PlayerAnsweringPager", false);
            } else if(answeringPager == true)
            {
                Outline.SetActive(true);
                animator.SetBool("PlayerAnsweringPager", true);
            } else if(hasSetPager == false && PagerFloat >= 10f)
            {
                Outline.SetActive(false);
            }
        }
        else
            state = CivilianHealth.Alive;

        if(rayCastScript.localDetection == 100 && state == CivilianHealth.Alive)
        {
            targetChooserScript.enabled = false;
            if (Vector2.Distance(player.position, transform.position) < 1.5f)
            {
                targetSetterScript.enabled = false;
                pathfindingScript.enabled = false;
            }
            else
            {
                targetSetterScript.enabled = true;
                targetSetterScript.target = player;
                pathfindingScript.enabled = true;
            }
        }

        if (health == 0 && hasSetPager == false)
        {
            PagerFloat = Mathf.MoveTowards(PagerFloat, 10, Time.deltaTime);
        }

        if(PagerFloat == 10 && hasSetPager == false)
        {
            interaction.interaction = 0;
            if (GameManager.instance != null)
            {
                //Failar heistet just nu
                GameManager.instance.heistState = HeistState.Failed;
            }
        }

        if(GameManager.instance.heistState == HeistState.Failed && health > 0 && Vector2.Distance(transform.position, player.position) > 1.5f)
        {
            targetChooserScript.enabled = false;
            targetSetterScript.target = player;
        } else if(GameManager.instance.heistState == HeistState.Failed && health > 0 && Vector2.Distance(transform.position, player.position) < 1.5f)
        {
            targetChooserScript.enabled = false;
            targetSetterScript.target = null;
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }
}
