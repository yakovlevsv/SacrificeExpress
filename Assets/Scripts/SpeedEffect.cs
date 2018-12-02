using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedEffect : MonoBehaviour {

    public float SpeedCoef { get { return _speedCoef; } }

    [SerializeField]
    float _speedCoef;
    [SerializeField]
    Bounds _bounds;

    void Start()
    {
        SlowManager.instance.RegisterSpeedEffect(this);
    }

    void OnDisable()
    {
        SlowManager.instance.UnregisterSpeedEffect(this);
    }

    public Bounds GetBounds()
    {
        Vector3 center = _bounds.center + transform.position;
        return new Bounds(center, _bounds.size);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(GetBounds().center, GetBounds().size);
    }
}
