using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartUI : MonoBehaviour {
    public Text last;
    public Text best;
    public Toggle[] button;
    private void Awake()
    {
        last.text = "上次:长度" + PlayerPrefs.GetInt("lastLength", 0) + ",分数" + PlayerPrefs.GetInt("lastScore",0);
        best.text = "最佳:长度" + PlayerPrefs.GetInt("bestLength", 0) + ",分数" + PlayerPrefs.GetInt("bestScore", 0);
    }
    private void Start()
    {
        if(PlayerPrefs.GetString("sh","sh01")=="sh01")
        {
            button[0].isOn = true;
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sh01", "sb0101");
            PlayerPrefs.SetString("sh02", "sb0102");
        }
        else
        {
            button[1].isOn = true;
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sh01", "sb0201");
            PlayerPrefs.SetString("sh02", "sb0202");
        }
        if(PlayerPrefs.GetInt("border",1)==1)
        {
            button[2].isOn = true;
            PlayerPrefs.SetInt("border", 1);
        }
        else
        {
            button[3].isOn = true;
            PlayerPrefs.SetInt("border", 0);
        }
    }
    public  void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void BlueSelected(bool isOn)
    {
        if(isOn)
        {
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sh01", "sb0101");
            PlayerPrefs.SetString("sh02", "sb0102");
        }
    }
    public void YellowSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sh01", "sb0201");
            PlayerPrefs.SetString("sh02", "sb0202");
        }
    }
    public void BorderSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 1);
        }
    }
    public void NoBorderSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 0);
        }
    }
}
