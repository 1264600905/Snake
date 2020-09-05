using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMake : MonoBehaviour {
    private static FoodMake _instance;
    public static FoodMake Instance
    {
        get
        {
            return _instance;
        }
    }
    public int xLimit=45;
    public int ylimit=30;
    public const int Lenth = 24;
    public GameObject foodPrefab;
    public Sprite[] foodSprite;
    public GameObject Reward;
    private Transform foodHolder;
    public Transform Pos1;
    public bool IsReward;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        foodHolder = GameObject.Find("Foods").transform;
        Make(IsReward);
    }
    private void Update()
    {
       
    }
    public void Make(bool isReward)
    {
        int index = Random.Range(0, foodSprite.Length);
        GameObject food = Instantiate(foodPrefab);
        food.transform.SetParent(foodHolder,false);
        int x = Random.Range(0, xLimit+2);
        int y= Random.Range(0,ylimit);
        Vector3 Local =Pos1.localPosition;
        Local+= new Vector3(x * Lenth, y * Lenth, 0);
        food.transform.localPosition = Local;
        food.GetComponent<Image>().sprite = foodSprite[index];
        if(isReward)
        {
            GameObject Reward_1= Instantiate(Reward);
            Reward_1.transform.SetParent(foodHolder, false);
            x = Random.Range(0, xLimit + 2);
            y = Random.Range(0, ylimit);
            Local = Pos1.localPosition;
            Local += new Vector3(x * Lenth, y * Lenth, 0);
        }
    }
}
