using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class heistplaytime : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    string seconds;
    string minutes;

    private void Start()
    {
        timeText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(PlaytimeHandler.instance.seconds >= 10)
        {
            seconds = $"{PlaytimeHandler.instance.seconds}";
        } 
        if(PlaytimeHandler.instance.seconds < 10)
        {
            seconds = $"0{PlaytimeHandler.instance.seconds}";
        }

        if(PlaytimeHandler.instance.minutes < 10)
        {
            minutes = $"0{PlaytimeHandler.instance.minutes}";
        } 

        if(PlaytimeHandler.instance.minutes >= 10)
        {
            minutes = $"{PlaytimeHandler.instance.minutes}";
        }

        timeText.text = $"{minutes} : {seconds}";
    }
}
