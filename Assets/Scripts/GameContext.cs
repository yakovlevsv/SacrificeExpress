using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContext : MonoBehaviour {

    public static GameContext instance;
    public Text text;

    public GameObject finishInfo;


    public int oneStarPoints;
    public int twoStarPoints;
    public int threeStarPoints;
    public int playerPoints;
    private bool finished = false;

    private static string timeTest = "Time left: ";
    public float seconds;


    private void Awake()
    {
        instance = this;
        finishInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds > 0)
        {
            seconds -= Time.deltaTime;
            text.text = timeTest + Mathf.Round(seconds);
        }
        else if(!finished)
        {            
            seconds = 0;
            FinishRound();
        }

    }

    private void FinishRound()
    {
        finished = true;
        seconds = 0;
        finishInfo.SetActive(true);
        Animation animation = finishInfo.GetComponent<Animation>();
        if (playerPoints>=threeStarPoints)
        {
            animation.Play("Victory03");       
        }else if(playerPoints>=twoStarPoints)
        {
            animation.Play("Victory02");            
        }
        else if (playerPoints >= oneStarPoints)
        {
            animation.Play("Victory01");            
        }
        else
        {
            animation.Play("Lose");            
        }

    }
}
