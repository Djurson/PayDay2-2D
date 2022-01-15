using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Trello;

public class TrelloBugReportManager : MonoBehaviour
{
    [Header("Bug Report Essentials")]
    [SerializeField] private TrelloPoster trelloPoster;
    [SerializeField] private TMP_InputField cardName;
    [SerializeField] private TMP_InputField cardDesc;
    [SerializeField] private TMP_Dropdown cardList;
    [SerializeField] private TMP_Dropdown cardLabel;

    private void Start()
    {
        cardList.AddOptions();
    }
}
