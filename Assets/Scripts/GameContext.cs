using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour {

    public static GameContext instance;

    private void Awake()
    {
        instance = this;
    }
}
