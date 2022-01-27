using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DropOffHandler : MonoBehaviour
{
    public static DropOffHandler instance;

    [SerializeField] private int GoldBarsCollected;
    [SerializeField] private int GoldBarsLimit;

    [SerializeField] private BoxCollider2D boxTrigger;
    [SerializeField] private Transform VanTransform;
    [SerializeField] private GameObject VanDropOffOutline;

    private bool leave;
    private float leaveFloat;

    private GameObject player;
    public GameObject fadeOutEffect;
    private bool loadNextScenelevel = false;

    private void Update()
    {
        if(GameManager.instance.heistState != HeistState.Failed)
        {
            if (leave == true)
            {
                leaveFloat = Mathf.MoveTowards(leaveFloat, 2f, Time.deltaTime);
            }

            if (leaveFloat == 2)
            {
                if (VanDropOffOutline != null)
                    Destroy(VanDropOffOutline);
                if (boxTrigger != null)
                    Destroy(boxTrigger);
                if (loadNextScenelevel == false)
                {
                    fadeOutEffect.SetActive(true);
                    PlaytimeHandler.instance.StopCounting();
                    GameManager.instance.PlayTimeInHeistsSeconds += PlaytimeHandler.instance.playtime;
                    StartCoroutine("waitForOneSecond");
                    loadNextScenelevel = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoldBars"))
        {
            GoldBarsCollected += 1;
            GameManager.instance.CollectedLootValue += 61500;
            GameManager.instance.XpEarned += 1150;
            GameManager.instance.BagsCollected += 1;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Player") && GoldBarsCollected >= GoldBarsLimit)
        {
            player = collision.gameObject;
            if (fadeOutEffect == null)
            {
                fadeOutEffect = player.GetComponent<CameraFollowPlayer>().cameraFade;
            }
            leave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            leave = false;
        }
    }

    private void nextSceneLevel()
    {
        SceneManager.LoadSceneAsync(4);
        LevelHandler.instance.check = true;
    }

    private IEnumerator waitForOneSecond()
    {
        yield return new WaitForSeconds(1);
        nextSceneLevel();
        StopCoroutine("waitForOneSecond");
    }
}
