using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Altar : MonoBehaviour
{
    private static readonly int[] sacrificeUsers = {1, 2, 3, 4};

    private AltarStates altarState;
    public Animator anim;
    public long timeWait;
    public long timeSleep;
    public long timeProcess;
    private long startTime;
    private Dictionary<AltarStates, long> map = new Dictionary<AltarStates, long>();
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
        startTime = DateTime.Now.Ticks;
        altarState = AltarStates.Sleep;
        map.Add(AltarStates.Sleep, timeSleep);
        map.Add(AltarStates.Waiting, timeWait);
        map.Add(AltarStates.Process, timeProcess);
    }

    private void FixedUpdate()
    {
        changeState();
        Animation(altarState);
    }

    public void RunProcess(int sacrificeId)
    {
        altarState = AltarStates.Process;
        sacrificeUser = sacrificeId;
    }

    public void Sleep()
    {
        altarState = AltarStates.Sleep;
    }

    public void Wait(int sacrificeId)
    {
        altarState = AltarStates.Waiting;
        sacrificeUser = sacrificeId;
    }

    void changeState()
    {
        long timeNow = DateTime.Now.Ticks;

        switch (altarState)
        {
            case AltarStates.Sleep:
            {
                long time = map[AltarStates.Sleep];
                if (timeNow - startTime - time >= time)
                {
                    startTime = timeNow;
                    altarState = AltarStates.Waiting;
                }

                break;
            }
            case AltarStates.Waiting:
            {
                long time = map[AltarStates.Waiting];
                if (timeNow - startTime - time >= time)
                {
                    startTime = timeNow;
                    altarState = AltarStates.Process;
                }

                break;
            }
            case AltarStates.Process:
            {
                long time = map[AltarStates.Process];
                if (timeNow - startTime - time >= time)
                {
                    startTime = timeNow;
                    altarState = AltarStates.Sleep;
                }

                break;
            }
        }
    }

    void Animation(AltarStates state)
    {
        switch (state)
        {
            case AltarStates.Process:
            {
                anim.SetTrigger("Process");
                anim.ResetTrigger("Wait");
                anim.SetInteger("ProcessType", 0);
                break;
            }
            case AltarStates.Waiting:
            {
                anim.SetTrigger("Wait");
                anim.ResetTrigger("Sleep");
                anim.SetInteger("ProcessType", sacrificeUser);
                break;
            }
            case AltarStates.Sleep:
            {
                anim.ResetTrigger("Process");
                anim.SetTrigger("Sleep");
                anim.SetInteger("ProcessType", 0);
                break;
            }
        }
    }
}