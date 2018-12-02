using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Networking;

public class StatController : MonoBehaviour
{
    public static StatController instance;
    public int selGridInt = 0;
    public string[] selStrings = new string[] {"Grid 1", "Grid 2", "Grid 3", "Grid 4"};
    private string name = "pupa";
    private bool requested;
    public int score = 1;

    void Awake()
    {
        instance = this;
    }

    void OnGUI()
    {
        selGridInt = GUI.SelectionGrid(new Rect(25, 25, 100, 30), selGridInt, selStrings, 2);
    }

    public void Update()
    {/*
        if (!string.IsNullOrEmpty(name) && !requested)
        {
            requested = true;
            StatData statData = new StatData(name, score);
            string json = JsonUtility.ToJson(statData);
            Debug.Log(" -------------------------" + json);


            WWWForm dataParameters = new WWWForm();
            dataParameters.AddField("username", name);
            dataParameters.AddField("score", score);
            
            dataParameters.headers["Content-Type"] = "application/json";
            UnityWebRequest r = UnityWebRequest.Post("http://151.80.143.95:8099/result", json);
            // StartCoroutine("PostdataEnumerator", "http://151.80.143.95:8099/result");

            StartCoroutine(SendResults(r));
        }*/
    }

/*

    IEnumerator SendResults(UnityWebRequest www)
    {
        /*yield return www;


        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }#1#
    }*/
}