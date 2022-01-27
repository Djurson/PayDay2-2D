using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GuardPathRandomizer : MonoBehaviour
{
    public AIDestinationSetter destionationSetter;

    private int CurrentTarget;

    public Transform startTransform;

    private void Start()
    {
        destionationSetter = GetComponent<AIDestinationSetter>();

        destionationSetter.target = startTransform;

        checkDistance();
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
        CurrentTarget = Random.Range(0, BankHeistRandomizer.instance.GuardSpawnPoints.Count);

        destionationSetter.target = BankHeistRandomizer.instance.GuardSpawnPoints[CurrentTarget].transform;

        BankHeistRandomizer.instance.AiSpawnPoints.Remove(BankHeistRandomizer.instance.GuardSpawnPoints[CurrentTarget]);
    }

    private IEnumerator waitForSeconds()
    {
        WaitForSeconds delay = new WaitForSeconds(Random.Range(10, 50));
        yield return delay;
        if(GameManager.instance.heistState != HeistState.Failed)
        {
            BankHeistRandomizer.instance.GuardSpawnPoints.Add(destionationSetter.target.gameObject);
            UpdateDestination();
            StopCoroutine("waitForSeconds");
        }
        else StopCoroutine("waitForSeconds");
    }
}
