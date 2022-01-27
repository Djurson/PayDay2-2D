using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThermallDrillToScreenPoint : MonoBehaviour
{
    [SerializeField] private GameObject ThermallDrill;
    private Camera cam;
    public Vector2 offset;
    public float lerpTime;
    private DrillInteraction drill;
    [SerializeField] private TMP_Text CountDown;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(ThermallDrill == null)
        {
            CountDown.gameObject.SetActive(false);
            ThermallDrill = GameObject.Find("BH_ThermalDrill");
        } if(ThermallDrill != null && drill != null)
        {
            if(drill == null)
            {
                if (ThermallDrill.activeInHierarchy == true)
                {
                    drill = ThermallDrill.GetComponent<DrillInteraction>();
                }
            }
            CountDown.gameObject.SetActive(true);
            if (drill.drillState == DrillState.Failure)
            {
                animator.SetBool("Failure", true);
            }
            else if (drill.drillState == DrillState.Drilling)
            {
                animator.SetBool("Failure", false);
            }

            CountDown.text = $"{drill.DrillTime}";
        }
    }

    private void FixedUpdate()
    {
        if(ThermallDrill != null && drill != null)
        {
            Vector2 textDesiredPos = cam.WorldToScreenPoint(ThermallDrill.transform.position);
            transform.position = Vector2.Lerp(transform.position, new Vector2(offset.x + textDesiredPos.x, offset.y + textDesiredPos.y), lerpTime);
        }
    }
}
