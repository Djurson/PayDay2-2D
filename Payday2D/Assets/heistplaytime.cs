using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class heistplaytime : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private void Start()
    {
        timeText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        timeText.text = $"{PlaytimeHandler.instance.minutes} : {PlaytimeHandler.instance.seconds}";
    }
}
