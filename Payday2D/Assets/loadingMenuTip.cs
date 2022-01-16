using UnityEngine;
using System.IO;
using TMPro;
using System.Collections;

public class loadingMenuTip : MonoBehaviour
{
    [SerializeField] private string[] TipsTextFileTextLines;
    [SerializeField] private TMP_Text TmpTextTip;

    private void Start()
    {
        if (Directory.Exists($"{Application.persistentDataPath}/Tips/"))
        {
            if (File.Exists($"{Application.persistentDataPath}/Tips/Tips.txt")) TipsTextFileTextLines = File.ReadAllLines($"{Application.persistentDataPath}/Tips/Tips.txt");
        }
        TmpTextTip.text = $"Tip: {TipsTextFileTextLines[Random.Range(0, TipsTextFileTextLines.Length)]}";

        StartCoroutine(changeTip());
    }

    private IEnumerator changeTip()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);

        while (true)
        {
            yield return delay;

            TmpTextTip.text = $"Tip: {TipsTextFileTextLines[Random.Range(0, TipsTextFileTextLines.Length)]}";
        }
    }
}
