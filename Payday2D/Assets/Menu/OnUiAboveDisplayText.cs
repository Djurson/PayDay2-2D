using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class OnUiAboveDisplayText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI M_DisplayText;

    public EventSystem eventSystem;

    public string DisplayText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        M_DisplayText.gameObject.SetActive(true);
        M_DisplayText.text = DisplayText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        M_DisplayText.gameObject.SetActive(false);
    }
}
