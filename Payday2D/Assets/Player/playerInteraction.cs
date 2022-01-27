using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public enum playerCarryingState
{
    Nothing,
    Bag,
    Gold,
    Person
};

public class playerInteraction : MonoBehaviour
{
    [Header("Player State")]
    public playerCarryingState _playerCarry;

    [Header("Floats")]
    public float throwForce;
    [Space(0.5f)]
    public float interaction;
    [Space(0.5f)]
    public float interactionMax;
    private float interacting;

    [Header("Prefabs")]
    [SerializeField] private GameObject DrillBagPrefab;
    [SerializeField] private GameObject DrillBagChildPrefab;
    [SerializeField] private GameObject GoldBagPrefab;
    [SerializeField] private GameObject GoldBagChildPrefab;
    [SerializeField] private GameObject BodyBagPrefab;
    [SerializeField] private GameObject BodyBagChildPrefab;

    [Header("Game Objects")]
    public GameObject throwableObject;
    public GameObject closeByInteractebleObject;
    private GameObject drillBagChild;

    [Header("UI")]
    public Image interactionProgress;
    public TextMeshProUGUI playerInfoText;
    string interactionTextBinding;
    public int bodyBags;
    public int Pagers = 4;
    public int cableTies = 9;

    [Header("Scripts")]
    private PlayerMovement _playerMovement;

    #region player controls

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    #endregion player controls

    RaycastHit2D hit;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        playerControls.Enable();
        interactionProgress.fillAmount = 0;
    }

    private void rayCast()
    {
        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 2.5f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 2.5f, Color.white);

        if(hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Door"))
            {
                var localDoorHandler = hit.collider.GetComponent<localDoorHandler>();
                closeByInteractebleObject = localDoorHandler.gameObject;

                if (localDoorHandler.localDoorState == doorState.Locked || localDoorHandler.localDoorState == doorState.Closed)
                {
                    interactionMax = localDoorHandler.interactionTime;
                    playerInfoText.enabled = true;
                    playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Lock Pick";
                }
            }

            if(throwableObject == null)
            {
                if (hit.collider.CompareTag("Bag") || hit.collider.CompareTag("DrillBag") || hit.collider.CompareTag("GoldBars") || hit.collider.CompareTag("BodyBag"))
                {
                    playerInfoText.enabled = true;
                    closeByInteractebleObject = hit.collider.gameObject;
                    interactionMax = hit.collider.gameObject.GetComponent<Bag>().interactionTime;
                    playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Grab";
                }

                if (hit.collider.CompareTag("ThermalDrill"))
                {
                    DrillInteraction drillInteraction;
                    hit.collider.TryGetComponent<DrillInteraction>(out drillInteraction);
                    if (drillInteraction != null && drillInteraction.drillState == DrillState.Setup || drillInteraction != null && drillInteraction.drillState == DrillState.Failure)
                    {
                        if (hit.collider.GetComponent<DrillInteraction>().drillState == DrillState.Setup)
                        {
                            playerInfoText.enabled = true;
                            closeByInteractebleObject = hit.collider.gameObject;
                            interactionMax = hit.collider.gameObject.GetComponent<DrillInteraction>().interactionTime;
                            playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Setup Drill";
                        }

                        if (hit.collider.GetComponent<DrillInteraction>().drillState == DrillState.Failure)
                        {
                            playerInfoText.enabled = true;
                            closeByInteractebleObject = hit.collider.gameObject;
                            interactionMax = hit.collider.gameObject.GetComponent<DrillInteraction>().interactionTime;
                            playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Fix Drill";
                        }
                    }
                }

                if (hit.collider.CompareTag("DeadCivilian") && bodyBags > 0)
                {
                    playerInfoText.enabled = true;
                    closeByInteractebleObject = hit.collider.gameObject;
                    interactionMax = hit.collider.gameObject.GetComponent<localCivilianHandler>().interactionTime;
                    playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Body Bag The Dead Body";
                }

                if (hit.collider.CompareTag("DeadGuard"))
                {
                    if(hit.collider.GetComponent<LocalGuardHandler>().hasSetPager == true && bodyBags > 0)
                    {
                        playerInfoText.enabled = true;
                        closeByInteractebleObject = hit.collider.gameObject;
                        interactionMax = hit.collider.gameObject.GetComponent<LocalGuardHandler>().interactionTime;
                        playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Body Bag The Dead Body";
                    }

                    if(hit.collider.GetComponent<LocalGuardHandler>().hasSetPager == false)
                    {
                        closeByInteractebleObject = hit.collider.gameObject;
                        playerInfoText.enabled = true;
                        interactionMax = hit.collider.gameObject.GetComponent<LocalGuardHandler>().PagerInteraction;
                        playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Answer Pager";
                    }
                }

                if (hit.collider.CompareTag("Civilian"))
                {
                    if(hit.collider.gameObject.GetComponent<CivilianRayCaster>().localState == civilianState.Panicing && cableTies > 0)
                    {
                        closeByInteractebleObject = hit.collider.gameObject;
                        playerInfoText.enabled = true;
                        interactionMax = hit.collider.GetComponent<CivilianRayCaster>().interaction;
                        playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()} To Cable Tie Civilian]";
                    } else if(hit.collider.gameObject.GetComponent<CivilianRayCaster>().localState == civilianState.CableTied)
                    {
                        closeByInteractebleObject = hit.collider.gameObject;
                        playerInfoText.enabled = true;
                        interactionMax = hit.collider.GetComponent<CivilianRayCaster>().interaction;
                        playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()} To Move Civilian]";
                    } else if(hit.collider.gameObject.GetComponent<CivilianRayCaster>().localState == civilianState.FollowingPlayer)
                    {
                        closeByInteractebleObject = hit.collider.gameObject;
                        playerInfoText.enabled = true;
                        interactionMax = hit.collider.GetComponent<CivilianRayCaster>().interaction;
                        playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()} To Sit Civilian Down]";
                    }
                }

                if (hit.collider.CompareTag("BodyBagCase") && hit.collider.GetComponent<BodyBagCaseHandler>().BodyBags > 0 && bodyBags < 2)
                {
                    closeByInteractebleObject = hit.collider.gameObject;
                    playerInfoText.enabled = true;
                    interactionMax = hit.collider.GetComponent<BodyBagCaseHandler>().interactionTime;
                    playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()} To Grab A Body Bag]";
                }
            }
        }

        if(hit.collider == null)
        {
            closeByInteractebleObject = null;
            interactionProgress.fillAmount = 0;
            interaction = 0;
        }
    }

    private void updateInteraction()
    {
        if(closeByInteractebleObject != null)
        {
            if (interacting == 1)
            {
                interaction = Mathf.MoveTowards(interaction, interactionMax, Time.deltaTime);
                interactionProgress.fillAmount = interaction / interactionMax;
            }
        }

        if(closeByInteractebleObject != null && interacting == 1 && closeByInteractebleObject.CompareTag("DeadGuard") && closeByInteractebleObject.GetComponent<LocalGuardHandler>().hasSetPager == false)
        {
            closeByInteractebleObject.GetComponent<LocalGuardHandler>().answeringPager = true;
        }

        if (interacting == 0)
        {
            interaction = 0;
            interactionProgress.fillAmount = 0;
        }
    }

    private void checkDoorInteraction()
    {
        if (interactionMax > 0 && interacting == 1 && closeByInteractebleObject != null)
        {
            if (interaction == interactionMax)
            {
                if (closeByInteractebleObject.CompareTag("Door"))
                {
                    closeByInteractebleObject.GetComponent<localDoorHandler>().OpenDoor();
                    closeByInteractebleObject = null;
                    interactionMax = 0;
                }
            }
        }
    }

    private void checkBagInteraction()
    {
        if(closeByInteractebleObject != null)
        {
            if (closeByInteractebleObject.CompareTag("Bag") || closeByInteractebleObject.CompareTag("DrillBag") || hit.collider.CompareTag("GoldBars") || hit.collider.CompareTag("DeadCivilian") || closeByInteractebleObject.CompareTag("BodyBag") || closeByInteractebleObject.CompareTag("DeadGuard"))
            {
                if(interaction == interactionMax)
                {
                    if (closeByInteractebleObject.CompareTag("DrillBag"))
                    {
                        throwableObject = DrillBagPrefab;
                        var instatiated = Instantiate(DrillBagChildPrefab, transform);
                        drillBagChild = instatiated.gameObject;
                        Destroy(closeByInteractebleObject);
                        closeByInteractebleObject = null;
                        _playerCarry = playerCarryingState.Bag;
                    }
                    else if (closeByInteractebleObject.CompareTag("GoldBars"))
                    {
                        throwableObject = GoldBagPrefab;
                        var instatiated = Instantiate(GoldBagChildPrefab, transform);
                        drillBagChild = instatiated.gameObject;
                        Destroy(closeByInteractebleObject);
                        closeByInteractebleObject = null;
                        _playerCarry = playerCarryingState.Gold;
                    } else if (closeByInteractebleObject.CompareTag("DeadCivilian") || closeByInteractebleObject.CompareTag("BodyBag"))
                    {
                        if (closeByInteractebleObject.CompareTag("DeadCivilian"))
                        {
                            bodyBags -= 1;
                        }
                        throwableObject = BodyBagPrefab;
                        var instatiated = Instantiate(BodyBagChildPrefab, transform);
                        drillBagChild = instatiated.gameObject;
                        Destroy(closeByInteractebleObject);
                        closeByInteractebleObject = null;
                        _playerCarry = playerCarryingState.Person;
                    } else if (closeByInteractebleObject.CompareTag("DeadGuard"))
                    {
                        if (closeByInteractebleObject.GetComponent<LocalGuardHandler>().hasSetPager == true)
                        {
                            bodyBags -= 1;
                            throwableObject = BodyBagPrefab;
                            var instatiated = Instantiate(BodyBagChildPrefab, transform);
                            drillBagChild = instatiated.gameObject;
                            Destroy(closeByInteractebleObject);
                            closeByInteractebleObject = null;
                            _playerCarry = playerCarryingState.Person;
                        }
                        else if (closeByInteractebleObject.GetComponent<LocalGuardHandler>().hasSetPager == false && Pagers > 0)
                        {
                            closeByInteractebleObject.GetComponent<LocalGuardHandler>().hasSetPager = true;
                            closeByInteractebleObject.GetComponent<LocalGuardHandler>().answeringPager = false;
                            closeByInteractebleObject = null;
                            interaction = 0;
                        } else if(closeByInteractebleObject.GetComponent<LocalGuardHandler>().hasSetPager == false && Pagers <= 0)
                        {
                            closeByInteractebleObject.GetComponent<LocalGuardHandler>().hasSetPager = false;
                            closeByInteractebleObject.GetComponent<LocalGuardHandler>().answeringPager = false;
                            closeByInteractebleObject = null;
                            interaction = 0;
                        }
                    }
                }
            }
        }
    }

    private void CheckBodyBagCaseInteraction()
    {
        if(closeByInteractebleObject != null)
        {

            if (closeByInteractebleObject.CompareTag("BodyBagCase"))
            {
                if(interaction == interactionMax)
                {
                    closeByInteractebleObject.GetComponent<BodyBagCaseHandler>().RemoveBodyBag();
                    bodyBags += 1;
                    closeByInteractebleObject = null;
                }
            }
        }
    }

    private void checkDrillInteraction()
    {
        if(closeByInteractebleObject != null)
        {
            if(closeByInteractebleObject.CompareTag("ThermalDrill"))
            {
                if (interactionMax > 0 && interacting == 1 && closeByInteractebleObject != null)
                {
                    if (interaction == interactionMax)
                    {
                        closeByInteractebleObject.GetComponent<DrillInteraction>().startDrill();
                        interactionMax = 0;
                        closeByInteractebleObject = null;
                    }
                }
            }
        }
    }

    private void CheckCivilianInteraction()
    {
        if(closeByInteractebleObject != null)
        {
            if (closeByInteractebleObject.CompareTag("Civilian"))
            {
                if(interaction == interactionMax)
                {
                    if(closeByInteractebleObject.GetComponent<CivilianRayCaster>().localState == civilianState.Panicing || closeByInteractebleObject.GetComponent<CivilianRayCaster>().localState == civilianState.FollowingPlayer)
                    {
                        if(closeByInteractebleObject.GetComponent<CivilianRayCaster>().localState == civilianState.Panicing)
                        {
                            cableTies -= 1;
                        }
                        closeByInteractebleObject.GetComponent<CivilianRayCaster>().localState = civilianState.CableTied;
                        closeByInteractebleObject = null;
                        interaction = 0;
                    } else if(closeByInteractebleObject.GetComponent<CivilianRayCaster>().localState == civilianState.CableTied)
                    {
                        closeByInteractebleObject.GetComponent<CivilianRayCaster>().localState = civilianState.FollowingPlayer;
                        closeByInteractebleObject = null;
                        interaction = 0;
                    }
                }
            }
        }
    }

    private void playerInput()
    {
        playerControls.KeyboardInputs.Interact.performed += ctx => interacting = ctx.ReadValue<float>();
        playerControls.KeyboardInputs.Interact.canceled += ctx => interacting = 0;

        playerControls.KeyboardInputs.ThrowBags.performed += ctx => throwInvetoryBag();
    }

    private void throwInvetoryBag()
    {
        if(throwableObject != null)
        {
            var instatiatedObject = Instantiate(throwableObject, transform.position, Quaternion.identity);
            instatiatedObject.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * throwForce, ForceMode2D.Impulse);
            throwableObject = null;
            closeByInteractebleObject = null;
            Destroy(drillBagChild);
            drillBagChild = null;
            _playerCarry = playerCarryingState.Nothing;
        }
    }

    private void Update()
    {
        if(GameManager.instance.heistState != HeistState.Failed)
        {
            string interactionBinding = playerControls.KeyboardInputs.Interact.GetBindingDisplayString();
            interactionTextBinding = interactionBinding.ToLower().Replace("press ", "");

            updateInteraction();
            playerInput();

            if (_playerMovement.playerMode == playerMode.Robbing)
            {
                checkDoorInteraction();
                checkBagInteraction();
                checkDrillInteraction();
                CheckCivilianInteraction();
                CheckBodyBagCaseInteraction();
                rayCast();
            }

            if (closeByInteractebleObject == null)
            {
                playerInfoText.enabled = false;
                interactionMax = 0;
            }

            if (interaction > 0 && interactionMax > 0)
                _playerMovement.isInteracting = true;

            if (interaction == 0)
                _playerMovement.isInteracting = false;
        }
    }
}
