using UnityEngine;
using System.IO;
using TMPro;
using System.Collections;

public class loadingMenuTip : MonoBehaviour
{
    [SerializeField] private int tip;
    [SerializeField] private int tipSaved;
    [SerializeField] private string[] TipsTextFileTextLines;
    [SerializeField] private TMP_Text TmpTextTip;

    private void Start()
    {
        if (Directory.Exists($"{Application.persistentDataPath}/Tips/"))
        {
            if (File.Exists($"{Application.persistentDataPath}/Tips/Tips.txt")) TipsTextFileTextLines = File.ReadAllLines($"{Application.persistentDataPath}/Tips/Tips.txt");
        }
        tip = Random.Range(0, TipsTextFileTextLines.Length);
        tipSaved = tip;

        TmpTextTip.text = $"Tip: {TipsTextFileTextLines[tip]}";

        StartCoroutine(changeTip());
    }

    private void generateTip()
    {
        tip = Random.Range(0, TipsTextFileTextLines.Length);

        if(tip == tipSaved)
        {
            generateTip();
        } else if(tip != tipSaved)
        {
            tipSaved = tip;

            TmpTextTip.text = $"Tip: {TipsTextFileTextLines[tip]}";
        }
    }

    private IEnumerator changeTip()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);

        while (true)
        {
            yield return delay;

            generateTip();
        }
    }
}
