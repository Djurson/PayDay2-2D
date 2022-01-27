using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeistDebugger : MonoBehaviour
{
    private GameObject[] AllObjectsInScene;
    public GameObject[] DeadCivilians;
    public GameObject[] DeadGuards;

    [SerializeField] private List<GameObject> OnBag;
    [SerializeField] private List<GameObject> Onplayer;
    [SerializeField] private List<GameObject> OnBody;

    public LayerMask PlayerLayer;
    public LayerMask BagLayer;
    public LayerMask BodyLayer;

    private void Update()
    {
        AllObjectsInScene = FindObjectsOfType<GameObject>();
        DeadCivilians = GameObject.FindGameObjectsWithTag("DeadCivilian");
        DeadGuards = GameObject.FindGameObjectsWithTag("DeadGuard");

        FindObjectsOnPlayerLayer();
        FindObjectsOnBagLayer();
        FindObjectsOnBodyLayer();
    }

    private void FindObjectsOnPlayerLayer()
    {
        for(int i = 0; i > AllObjectsInScene.Length; i++)
        {
            if(AllObjectsInScene[i].layer == PlayerLayer)
            {
                Onplayer.Add(AllObjectsInScene[i]);
            }
        }
    }

    private void FindObjectsOnBagLayer()
    {
        for (int i = 0; i > AllObjectsInScene.Length; i++)
        {
            if (AllObjectsInScene[i].layer == BagLayer)
            {
                OnBag.Add(AllObjectsInScene[i]);
            }
        }
    }

    private void FindObjectsOnBodyLayer()
    {
        for (int i = 0; i > AllObjectsInScene.Length; i++)
        {
            if (AllObjectsInScene[i].layer == BodyLayer)
            {
                OnBody.Add(AllObjectsInScene[i]);
            }
        }
    }
}
