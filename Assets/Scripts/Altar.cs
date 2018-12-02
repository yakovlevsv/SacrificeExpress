using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Altar : MonoBehaviour
{
    public bool IsSpecial { get { return _special; } }
    public bool IsSleep { get { return _altarState == AltarStates.Sleep; } }
    public bool IsWaiting { get { return _altarState == AltarStates.Waiting; } }
    public VictumTypes VictumType { get { return _victumType; } }

    [SerializeField]
    bool _special;
    [SerializeField]
    Animator _animator;
    [SerializeField]
    Transform _victumRoot;

    AltarStates _altarState;
    DateTime _startStateTime;
    GameObject _sacrifice;
    VictumTypes _victumType;
    TimeSpan _waitTime;
    Transform body;
    GameObject _waitObject;

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

        _waitObject = Instantiate(VictumsController.instance.GetVictumInfo(victumType).prefab, _victumRoot);
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

        if (newAltarState != AltarStates.Waiting && _waitObject != null)
        {
            Destroy(_waitObject);
            _waitObject = null;
        }

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