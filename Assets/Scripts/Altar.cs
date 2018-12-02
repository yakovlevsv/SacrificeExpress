using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Altar : MonoBehaviour
{
    public bool IsSleep { get { return _altarState == AltarStates.Sleep; } }
    public bool IsWaiting { get { return _altarState == AltarStates.Waiting; } }
    public VictumTypes VictumType { get { return _victumType; } }

    [SerializeField]
    Animator _animator;

    AltarStates _altarState;
    DateTime _startStateTime;
    GameObject _sacrifice;
    VictumTypes _victumType;
    TimeSpan _waitTime;
    GameContext gameContext;
    Transform body;

    enum AltarStates
    {
        Sleep,
        Waiting,
        Process
    }

    void Awake()
    {
        _startStateTime = DateTime.Now;
        _altarState = AltarStates.Sleep;
        gameContext = GetComponent<GameContext>();
    }

    void Update()
    {
        CheckStateTimeout();
    }

    public void RunProcess()
    {
        ChangeState(AltarStates.Process);
    }

    public void Sleep()
    {
        ChangeState(AltarStates.Sleep);
    }

    public void Wait(VictumTypes victumType, TimeSpan waitTime)
    {
        _waitTime = waitTime;
        _victumType = victumType;
        ChangeState(AltarStates.Waiting);
    }

    void CheckStateTimeout()
    {
        switch (_altarState)
        {
            case AltarStates.Waiting:
            {
                TimeSpan time = _waitTime;
                if (DateTime.UtcNow - _startStateTime >= time)
                    ChangeState(AltarStates.Sleep);

                break;
            }
            case AltarStates.Process:
            {
                TimeSpan time = AltarsController.instance.ProcessTime;
                if (DateTime.UtcNow - _startStateTime >= time)
                    ChangeState(AltarStates.Sleep);

                break;
            }
        }
    }

    void ChangeState(AltarStates newAltarState)
    {
        _altarState = newAltarState;
        _startStateTime = DateTime.UtcNow;

        switch (_altarState)
        {
            case AltarStates.Process:
            {
                    Debug.Log("process");
                    _animator.SetTrigger("Process");
                _animator.SetInteger("ProcessType", (int)_victumType);                               
                break;
            }
            case AltarStates.Waiting:
            {
                _animator.SetTrigger("Wait");
                break;
            }
            case AltarStates.Sleep:
            {
                _animator.SetTrigger("Sleep");
                break;
            }
        }
    }
}