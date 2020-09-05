using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {
    private static MainUI instance2;
    public static MainUI Instance
    {
        get
        {
            return instance2;
        }
    }
    public bool hasBorder=true;
    public int score = 0;
    public int Length=0;
    public Text msgText;
    public Text scoreText;
    public Text lengthText;
    public Image BgImage;
    public bool isPause=false;
    public Image buttonImage;
    public Sprite[] buttonSprite;
    private void Start()
    {
        if(PlayerPrefs.GetInt("border",1)==0)
        {
            hasBorder = false;
            foreach(Transform t in BgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }
    private void Update()
    {
        switch(score/100)
        {
            case 2:
                BgImage.color = new Color(191/255f,  100 / 255f, 100 / 255f);
                msgText.text = "第二阶段";
                break;
            case 4:
                BgImage.color = new Color(140 / 255f,  244 / 255f, 247 / 255f);
                msgText.text = "第三阶段";
                break;
            case 6:
                BgImage.color = new Color( 140 / 255f, 244 / 255f, 200 / 255f);
                msgText.text = "第四阶段";
                break;
            case 8:
                BgImage.color = new Color(1f,  114 / 255f, 158 / 255f);
                msgText.text = "无尽模式";
                break;
            default:
                break;

        }
    }
    private void Awake()
    {
        instance2 = this;
    }
    public void UpdateUI(int s=5,int l=1)
    {
        score += s;
        Length += l;
        scoreText.text = "得分\n" + score;
        lengthText.text = "长度\n" + Length;
    }
    public void Pause()
    {
        isPause = !isPause;
        if(isPause)
        {
            Time.timeScale = 0;
            buttonImage.sprite = buttonSprite[1];
        }
        else
        {
            Time.timeScale = 1;
            buttonImage.sprite = buttonSprite[0];
        }
    }
    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
