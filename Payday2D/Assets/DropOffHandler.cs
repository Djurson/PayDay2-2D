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
    private GameObject fadeOutEffect;

    private Color ImageColor;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(instance);
            instance = this;
        }
    }

    private void Update()
    {
        if(fadeOutEffect == null) { fadeOutEffect = GameObject.FindWithTag("FadeOutEffect"); }

        if(leave == true)
        {
            leaveFloat = Mathf.MoveTowards(leaveFloat, 2f, Time.deltaTime);
        }

        if(leaveFloat == 2)
        {
            if(VanDropOffOutline != null)
                Destroy(VanDropOffOutline);
            if(boxTrigger != null)
                Destroy(boxTrigger);
            var color = fadeOutEffect.GetComponent<Image>().color;
            color.a = Mathf.MoveTowards(color.a, 255f, Time.deltaTime);
            fadeOutEffect.GetComponent<Image>().color = color;
            ImageColor = color;
            VanTransform.position = Vector2.MoveTowards(VanTransform.position, new Vector2(54, VanTransform.position.y), Time.deltaTime);
        }

        if(ImageColor.a == 255f)
        {
            SceneManager.LoadSceneAsync(4);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoldBars"))
        {
            GoldBarsCollected += 1;
            GameHandler.instance.CollectedLootValue += 61500;
            GameHandler.instance.XpEarned += 1150;
            GameHandler.instance.BagsCollected += 1;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Player") && GoldBarsCollected >= GoldBarsLimit)
        {
            player = collision.gameObject;
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
}
