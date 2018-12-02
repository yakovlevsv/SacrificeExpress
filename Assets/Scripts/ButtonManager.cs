using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    private void Awake()
    {
        SoundManager.PlayMusic("BlackmoorColossus");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
