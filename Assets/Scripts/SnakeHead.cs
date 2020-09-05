using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {
    public float step_x;
    public float step_y;
    private float x;
    private float y;
    public int leftLimit;
    public int rightLimit;
    private Vector3 HeadPos;
    public float Speed=0.5f;
    public AudioClip[] Audios;
    public List<Transform> BodyList = new List<Transform>();
    public GameObject BodyPrefabs;
    public Sprite[] BodySprite = new Sprite[2];
    private Transform Canvas;
    private bool isdie = false;
    public GameObject dieEft;
    private void Awake()
    {
        Canvas = GameObject.Find("Canvas").transform;
        //通过这个方法加载资源path的书写不需要加/，及扩展名.
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh01"));
        BodySprite[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sh01", "sh0101"));
        BodySprite[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sh02", "sh0102"));
        print(PlayerPrefs.GetString("sh01"));
    }
    private void Start()
    {
        InvokeRepeating("Move", 0, Speed);
        x = step_x;y = 0;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
    }
    void Move()
    {
        HeadPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(HeadPos.x + x, HeadPos.y + y, HeadPos.z);
        if (BodyList.Count > 0)
        {
            for (int i = BodyList.Count - 2; i >= 0; i--)
            {
                BodyList[i + 1].localPosition = BodyList[i].localPosition;
            }
            BodyList[0].localPosition = HeadPos;
        }
       
    }
    void Grow()
    {
        int num = (BodyList.Count % 2 == 0) ? 0 : 1;
        GameObject body = Instantiate(BodyPrefabs, new Vector3(2000, 2000, 0), Quaternion.identity);
        body.GetComponent<Image>().sprite = BodySprite[num];
        body.transform.SetParent(Canvas, false);
        BodyList.Add(body.transform);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&MainUI.Instance.isPause==false &&isdie==false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, Speed - 0.3f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && MainUI.Instance.isPause == false && isdie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, Speed);
        }
        if (Input.GetKey(KeyCode.W)&&y!= -step_y && MainUI.Instance.isPause == false && isdie == false)
        {
            x = 0;y = step_y;
            gameObject.transform.localRotation= Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) && y!= step_y && MainUI.Instance.isPause == false && isdie == false)
        {
            x = 0;y = -step_y;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey(KeyCode.A) && x!= step_x && MainUI.Instance.isPause == false && isdie == false)
        {
            x = -step_x;y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.D) && x!= -step_x && MainUI.Instance.isPause == false && isdie == false)
        {
            x = step_x;y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(Audios[0], Vector3.zero);
            MainUI.Instance.UpdateUI();
            Grow();
            if(Random.Range(0,100)<20)
            {
                FoodMake.Instance.Make(true);
            }
            else
            {
                FoodMake.Instance.Make(false);
            }
        }
        else if (collision.tag == "body")
        {
            die();
        }else if(collision.tag== "Reward")
        {
              
             Destroy(collision.gameObject);
            MainUI.Instance.UpdateUI(Random.Range(5, 20), 1);
            Grow();
        }
        else
        {
            if(MainUI.Instance.hasBorder)
            {
                die();
            }
            else
            {
                switch (collision.tag)
                {//这里出现了一个神奇的BUG，由于collision太窄，所以碰撞无法检测。测试了好久才发现。
                    case "Up":
                        gameObject.transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 28, transform.localPosition.z);
                        break;
                    case "Down":
                        gameObject.transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 24, transform.localPosition.z);
                        break;
                    case "Left":
                        gameObject.transform.localPosition = new Vector3(-transform.localPosition.x + leftLimit, transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "Right":
                        gameObject.transform.localPosition = new Vector3(-transform.localPosition.x + rightLimit, transform.localPosition.y, transform.localPosition.z);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void die()
    {
        AudioSource.PlayClipAtPoint(Audios[1],Vector3.zero);
        CancelInvoke();
        isdie = true;
        Instantiate(dieEft);
        PlayerPrefs.SetInt("lastLength", MainUI.Instance.Length);
        PlayerPrefs.SetInt("lastScore", MainUI.Instance.score);
        if(PlayerPrefs.GetInt("bestScore",0)< MainUI.Instance.score)
        {
            PlayerPrefs.SetInt("bestLength", MainUI.Instance.Length);
            PlayerPrefs.SetInt("bestScore", MainUI.Instance.score);
        }
        StartCoroutine(GameOver(3));
    }
    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
