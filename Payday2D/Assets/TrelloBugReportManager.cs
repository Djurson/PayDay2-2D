using System.Collections.Generic;
using UnityEngine;
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

    private bool noLabels = false;

    private void Start()
    {
        cardList.AddOptions(GetDropdownOptions(trelloPoster.TrelloCardListOptions));
        TrelloCardOption[] cardLabels = trelloPoster.TrelloCardLabelOptions;
        if (cardLabels == null || cardLabels.Length == 0)
        {
            noLabels = true;
            cardLabel.gameObject.SetActive(false);
        }
        else
        {
            cardLabel.AddOptions(GetDropdownOptions(cardLabels));
        }
    }

    private List<TMP_Dropdown.OptionData> GetDropdownOptions(TrelloCardOption[] cardOptions)
    {
        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < cardOptions.Length; i++)
        {
            dropdownOptions.Add(new TMP_Dropdown.OptionData(cardOptions[i].Name));
        }
        return dropdownOptions;
    }

    public void StartPostCard()
    {
        if(cardList.value != 1)
        {
            StartCoroutine(trelloPoster.PostCard(new TrelloCard(cardName.text, cardDesc.text, "bottom", trelloPoster.TrelloCardListOptions[cardList.value].Id, noLabels ? null : trelloPoster.TrelloCardLabelOptions[cardLabel.value].Id, null)));
        }
        else
        {
            StartCoroutine(trelloPoster.PostCard(new TrelloCard(cardName.text, cardDesc.text, "bottom", trelloPoster.TrelloCardListOptions[cardList.value].Id, noLabels ? null : "61dda1b48166f387532e7862", null)));
        }
    }
}
