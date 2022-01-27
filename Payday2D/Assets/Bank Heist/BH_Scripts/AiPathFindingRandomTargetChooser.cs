using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AiPathFindingRandomTargetChooser : MonoBehaviour
{
    public AIDestinationSetter destionationSetter;

    private int CurrentTarget;

    public Transform startTransform;

    public bool Standing = true;

    private void Start()
    {
        destionationSetter = GetComponent<AIDestinationSetter>();

        destionationSetter.target = startTransform;

        if (Standing == false)
        {
            checkDistance();
        }
    }

    private void checkDistance()
    {
        if (Vector2.Distance(destionationSetter.target.transform.position, this.transform.position) < 1)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, destionationSetter.target.rotation, 1.5f * Time.deltaTime);
            StartCoroutine("waitForSeconds");
        }
    }

    private void Update()
    {
        checkDistance();
    }

    private void UpdateDestination()
    {
        CurrentTarget = Random.Range(0, BankHeistRandomizer.instance.AiSpawnPoints.Count);

        destionationSetter.target = BankHeistRandomizer.instance.AiSpawnPoints[CurrentTarget].transform;

        BankHeistRandomizer.instance.AiSpawnPoints.Remove(BankHeistRandomizer.instance.AiSpawnPoints[CurrentTarget]);
    }

    private IEnumerator waitForSeconds()
    {
        WaitForSeconds delay = new WaitForSeconds(Random.Range(10,50));
        yield return delay;
        BankHeistRandomizer.instance.AiSpawnPoints.Add(destionationSetter.target.gameObject);
        UpdateDestination();
        StopCoroutine("waitForSeconds");
    }
}
