using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private string playerName;

    private bool isChanged = false;


    public void NewGame()
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
        GameContext.instance.finished = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenStatistic()
    {
        if (!isChanged)
        {
            Debug.Log("Input Name");
            return;
        }

        SceneManager.LoadScene("Statistic", LoadSceneMode.Single);
        StatController.instance.name = playerName;
    }


    public void UpdatePlayerName(string playerName)
    {
        Debug.Log("Name changed");
        this.isChanged = true;
        this.playerName = playerName;
    }
}