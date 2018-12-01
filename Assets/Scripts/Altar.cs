using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Altar : MonoBehaviour
{
    private static readonly int[] sacrificeUsers = {1, 2, 3, 4};

    public Animator anim;

    private AltarStates altarState;
    private DateTime startTime;
    private GameObject sacrifice;
    private int sacrificeUser;
    private long timeNextState;

    private enum AltarStates
    {
        Sleep,
        Waiting,
        Process
    }

    void Awake()
    {
        sacrificeUser = sacrificeUsers[Random.Range(1, 4)];
        startTime = DateTime.Now;
        altarState = AltarStates.Sleep;
    }

    void Update()
    {
        CheckStateTimeout();
    }

    [ContextMenu("Process")]
    public void RunProcess()
    {
        ChangeState(AltarStates.Process);
    }

    [ContextMenu("Sleep")]
    public void Sleep()
    {
        ChangeState(AltarStates.Sleep);
    }

    [ContextMenu("Wait")]
    public void WaitDebug()
    {
        Wait(0);
    }

    public void Wait(int sacrificeId)
    {
        sacrificeUser = sacrificeId;
        ChangeState(AltarStates.Waiting);
    }

    void CheckStateTimeout()
    {
        DateTime timeNow = DateTime.Now;

        switch (altarState)
        {
            case AltarStates.Waiting:
            {
                TimeSpan time = TimeSpan.FromSeconds(10);
                if (timeNow - startTime - time >= time)
                {
                    startTime = timeNow;
                    ChangeState(AltarStates.Sleep);
                }

                break;
            }
            case AltarStates.Process:
            {
                TimeSpan time = TimeSpan.FromSeconds(10);
                if (timeNow - startTime - time >= time)
                {
                    startTime = timeNow;
                    ChangeState(AltarStates.Sleep);
                }

                break;
            }
        }
    }

    void ChangeState(AltarStates newAltarState)
    {
        altarState = newAltarState;

        switch (altarState)
        {
            case AltarStates.Process:
            {
                anim.SetTrigger("Process");
                anim.SetInteger("ProcessType", sacrificeUser);
                break;
            }
            case AltarStates.Waiting:
            {
                anim.SetTrigger("Wait");
                break;
            }
            case AltarStates.Sleep:
            {
                anim.SetTrigger("Sleep");
                break;
            }
        }
    }
}