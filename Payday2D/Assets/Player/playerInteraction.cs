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
    public GameObject DrillBagPrefab;
    public GameObject DrillBagChildPrefab;
    public GameObject GoldBagPrefab;
    public GameObject GoldBagChildPrefab;

    [Header("Game Objects")]
    public GameObject throwableObject;
    public GameObject closeByInteractebleObject;
    private GameObject drillBagChild;

    [Header("UI")]
    public Image interactionProgress;
    public TextMeshProUGUI playerInfoText;
    string interactionTextBinding;

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
                if (hit.collider.CompareTag("Bag") || hit.collider.CompareTag("DrillBag") || hit.collider.CompareTag("GoldBars"))
                {
                    playerInfoText.enabled = true;
                    closeByInteractebleObject = hit.collider.gameObject;
                    interactionMax = hit.collider.gameObject.GetComponent<Bag>().interactionTime;
                    playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Grab";
                }

                if (hit.collider.CompareTag("ThermalDrill"))
                {
                    if (hit.collider.GetComponent<DrillInteraction>().drillState == DrillState.Setup || hit.collider.GetComponent<DrillInteraction>().drillState == DrillState.Failure)
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

                if (hit.collider.CompareTag("DeadCivilian"))
                {
                    playerInfoText.enabled = true;
                    closeByInteractebleObject = hit.collider.gameObject;
                    interactionMax = hit.collider.gameObject.GetComponent<localCivilianHandler>().interactionTime;
                    playerInfoText.text = $"Hold [{interactionTextBinding.ToUpper()}] To Body Bag The Dead Body";
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
            if (closeByInteractebleObject.CompareTag("Bag") || closeByInteractebleObject.CompareTag("DrillBag") || hit.collider.CompareTag("GoldBars"))
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
                    }
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
        string interactionBinding = playerControls.KeyboardInputs.Interact.GetBindingDisplayString();
        interactionTextBinding = interactionBinding.ToLower().Replace("press ", "");

        updateInteraction();
        playerInput();

        if (_playerMovement.playerMode == playerMode.Robbing)
        {
            checkDoorInteraction();
            checkBagInteraction();
            checkDrillInteraction();
            rayCast();
        }

        if(closeByInteractebleObject == null)
        {
            playerInfoText.enabled = false;
            interactionMax = 0;
        }

        if(interaction > 0)
            _playerMovement.isInteracting = true;

        if (interaction == 0)
            _playerMovement.isInteracting = false;
    }
}
