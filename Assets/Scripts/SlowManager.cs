using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlowManager : MonoBehaviour {

    public static SlowManager instance;

    List<SpeedEffect> _effects = new List<SpeedEffect>();

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
    }

    public float GetSpeed(Vector3 position)
    {
        float speed = 1f;
        foreach(SpeedEffect effect in _effects)
        {
            if (effect.GetBounds().Contains(position))
                speed *= effect.SpeedCoef;
        }
        return speed;
    }

    public void RegisterSpeedEffect(SpeedEffect effect)
    {
        _effects.Add(effect);
    }

    public void UnregisterSpeedEffect(SpeedEffect effect)
    {
        _effects.Remove(effect);
    }
}
