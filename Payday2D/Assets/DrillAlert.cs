using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillAlert : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Civilian"))
        {
            collision.gameObject.GetComponent<CivilianRayCaster>().Hearing = true;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position);
            collision.gameObject.GetComponent<CivilianRayCaster>().distanceToHearing = hit.distance;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Civilian"))
        {
            collision.gameObject.GetComponent<CivilianRayCaster>().Hearing = false;
            collision.gameObject.GetComponent<CivilianRayCaster>().distanceToHearing = 0;
        }
    }
}
