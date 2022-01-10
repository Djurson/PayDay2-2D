using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AiPathFindingRandomTargetChooser : MonoBehaviour
{
    private AIDestinationSetter destionationSetter;

    public List<AIDestinationSetter> destinationSetters;

    private int CurrentTarget;

    private GameObject[] Targets;
    public GameObject[] Civilians;

    public bool findNewTarget;

    private void Start()
    {
        destionationSetter = GetComponent<AIDestinationSetter>();
        Targets = GameObject.FindGameObjectsWithTag("AiTargets");
        Civilians = GameObject.FindGameObjectsWithTag("Civilian");

        for (int i = 0; i < Civilians.Length; i++)
        {
            destinationSetters.Add(Civilians[i].GetComponent<AIDestinationSetter>());
        }

        UpdateDestination();
    }

    private void Update()
    {
        if(Vector2.Distance(Targets[CurrentTarget].transform.position, this.transform.position) < 1)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Targets[CurrentTarget].transform.rotation, 1.5f * Time.deltaTime);
            StartCoroutine("waitForSeconds");
        }
    }

    private void UpdateDestination()
    {
        CurrentTarget = Random.Range(0, Targets.Length);

        for(int i = 0; i < Civilians.Length; i++)
        {
            if(destinationSetters[i] != this.destionationSetter)
            {
                if (destinationSetters[i].target != Targets[CurrentTarget])
                {
                    findNewTarget = false;
                }
                else if (destinationSetters[i].target != Targets[CurrentTarget])
                {
                    findNewTarget = true;
                }
            }
        }

        if (findNewTarget == true)
            UpdateDestination();

        destionationSetter.target = Targets[CurrentTarget].transform;
    }

    private IEnumerator waitForSeconds()
    {
        WaitForSeconds delay = new WaitForSeconds(Random.Range(10,50));
        yield return delay;
        UpdateDestination();
        StopCoroutine("waitForSeconds");
    }
}
