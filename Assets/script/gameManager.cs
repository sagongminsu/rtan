using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip match;
    public Text timeTxt;
    public GameObject card;
    float time;
    public GameObject firstcard;
    public GameObject secondcard;
    public GameObject endTxt;
    public static gameManager I;

    public AudioClip bgmusic;

    void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            GameObject newcard = Instantiate(card);
            newcard.transform.parent = GameObject.Find("cards").transform;

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newcard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }


    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time > 30.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void isMatched()
    {
        string firstcardImage = firstcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondcardImage = secondcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstcardImage == secondcardImage)
        {
            audioSource.PlayOneShot(match);

            firstcard.GetComponent<card>().destroycard();
            secondcard.GetComponent<card>().destroycard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if(cardsLeft == 2)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
                Invoke("GameEnd", 1f);
            }
        }
        else
        {
            firstcard.GetComponent<card>().closecard();
            secondcard.GetComponent<card>().closecard();
        }

        firstcard = null;
        secondcard = null;

    }
    void GameEnd()
    {
        endTxt.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
