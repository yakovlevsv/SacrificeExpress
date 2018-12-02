using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContext : MonoBehaviour {

    public static GameContext instance;
    public Text text;
    public Text score;

    public GameObject finishInfo;

    public int oneStarPoints;
    public int twoStarPoints;
    public int threeStarPoints;
    public int playerPoints;
    private bool finished = false;

    private static string timeText = "Time left: ";
    private static string scoreText = "Score: ";
    public float seconds;


    private void Awake()
    {
        instance = this;
        finishInfo.SetActive(false);

        SoundManager.PlayMusic("MusicGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds > 0)
        {
            score.text = scoreText+ playerPoints;
            seconds -= Time.deltaTime;
            text.text = timeText + Mathf.Round(seconds);
        }
        else if(!finished)
        {            
            seconds = 0;
            FinishRound();
        }

    }

    public void AddPoint()
    {
        playerPoints++;
        Debug.Log(playerPoints);
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
