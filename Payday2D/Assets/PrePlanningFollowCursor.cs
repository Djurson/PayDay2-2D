using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePlanningFollowCursor : MonoBehaviour
{
    [Header("Pre Planning")]
    [SerializeField] private Image PrePlanningImage;
    [SerializeField] private Vector2 mousePosWorld;
    [SerializeField] private float panBorderThickness;
    [SerializeField] private float panSpeed;
    [SerializeField] private Vector3 desiredPos;
    [SerializeField] private float ZoomPerformed;
    [SerializeField] private float ZoomValue = 0.1f;
    [SerializeField] private float movementFactor;
    [SerializeField] private bool canMove = true;
    [SerializeField] Vector3 imageTransform;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    {
        controls.Enable();
        limitMovement();
        PrePlanningImage.rectTransform.position = Vector2.zero;
    }

    private void Update()
    {
        imageTransform = PrePlanningImage.transform.position;
        limitMovement();
        getmousePosWorldition();
    }

    private void limitMovement()
    {
        movementFactor = (PrePlanningImage.rectTransform.localScale.x - 0.35f) / (0.75f - 0.35f);

        if(PrePlanningImage.rectTransform.localScale.x <= 0.35f)
        {
            PrePlanningImage.rectTransform.position = Vector2.Lerp(PrePlanningImage.rectTransform.position, new Vector2(960, 540), Time.deltaTime);
            desiredPos = new Vector2(960, 540);
        }

        if(imageTransform.x <= -1350f + 960)
        {
            canMove = false;
            PrePlanningImage.rectTransform.position = Vector2.Lerp(PrePlanningImage.rectTransform.position, new Vector2(960, 540), Time.deltaTime);
            desiredPos = new Vector2(960, 540);
        }
    }

    private void getmousePosWorldition()
    {
        mousePosWorld = controls.KeyboardInputs.MousePosition.ReadValue<Vector2>();
        controls.KeyboardInputs.MouseScroll.performed += ctx => ZoomPerformed = ctx.ReadValue<float>();
        controls.KeyboardInputs.MouseScroll.canceled += ctx => ZoomPerformed = 0;

        if(canMove == true)
        {
            if (PrePlanningImage.rectTransform.localScale.x > 0.35f)
            {
                if (mousePosWorld.y >= Screen.height - panBorderThickness)
                {
                    desiredPos.y -= (panSpeed * movementFactor) * Time.deltaTime;
                }

                if (mousePosWorld.y <= panBorderThickness)
                {
                    desiredPos.y += (panSpeed * movementFactor) * Time.deltaTime;
                }

                if (mousePosWorld.x >= Screen.height - panBorderThickness)
                {
                    desiredPos.x -= (panSpeed * movementFactor) * Time.deltaTime;
                }

                if (mousePosWorld.x <= panBorderThickness)
                {
                    desiredPos.x += (panSpeed * movementFactor) * Time.deltaTime;
                }

                PrePlanningImage.rectTransform.position = desiredPos;
            }
        }

        PrePlanningImage.rectTransform.localScale = new Vector2(Mathf.Clamp(PrePlanningImage.rectTransform.localScale.x, 0.35f, 0.75f), Mathf.Clamp(PrePlanningImage.rectTransform.localScale.y, 0.35f, 0.75f));

        if (PrePlanningImage.rectTransform.localScale.x >= 0.3f)
        {
            if (ZoomPerformed >= 1)
            {
                PrePlanningImage.rectTransform.localScale = Vector2.MoveTowards(PrePlanningImage.rectTransform.localScale, PrePlanningImage.rectTransform.localScale * (1 + ZoomValue), 2 * Time.deltaTime);
            }
        }

        if (PrePlanningImage.rectTransform.localScale.x <= 0.8f)
        {
            if (ZoomPerformed <= -1)
            {
                PrePlanningImage.rectTransform.localScale = Vector2.MoveTowards(PrePlanningImage.rectTransform.localScale, PrePlanningImage.rectTransform.localScale * (1 - ZoomValue), 2 * Time.deltaTime);
            }
        }
    }
}
