using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBagCaseHandler : MonoBehaviour
{
    public int BodyBags = 4;
    [SerializeField] private List<GameObject> BodyBagsList;
    public float interactionTime = 3;

    public void RemoveBodyBag()
    {
        BodyBags -= 1;
        GameObject bodyBag = BodyBagsList[BodyBags];
        BodyBagsList.RemoveAt(BodyBags);
        Destroy(bodyBag.gameObject);
    }
}
