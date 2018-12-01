using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovableBody : MonoBehaviour {

    public event Action onTaken;
    public event Action onThrown;

    bool _onGround;

    void Awake()
    {
        _onGround = true;
    }

    public void Take()
    {
        _onGround = false;

        if (onTaken != null)
            onTaken();
    }

    public void Throw()
    {
        _onGround = true;

        if (onThrown != null)
            onThrown();
    }
}
