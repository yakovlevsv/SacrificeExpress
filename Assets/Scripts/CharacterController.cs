﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {


    public Transform joinPoint;
    [SerializeField]
    Transform[] _altars;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _pickUpDistance;
    [SerializeField]
    Transform _bodyPos;
    [SerializeField]
    Animator _animator;
    
    Rigidbody _rigidbody;
    Victum _victumInHands;
    bool _canPickUp;
    bool _canThrow;
    DateTime _timePrevStep;

    void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameContext.instance.finished) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var direction = new Vector3(h, 0, v);
        if (direction != Vector3.zero)
        {
            if (direction.magnitude > 1f)
                direction.Normalize();
            float speedCoef = SlowManager.instance.GetSpeed(transform.position);
            if (_victumInHands != null)
                speedCoef *= VictumsController.instance.GetVictumInfo(_victumInHands.type).speedCoef;

			Vector3 newSpeed = direction * _speed * speedCoef;
			newSpeed.y = _rigidbody.velocity.y;
            _rigidbody.velocity = newSpeed;
            _animator.SetBool("Move", true);

            if (_timePrevStep + TimeSpan.FromSeconds(0.3f) < DateTime.UtcNow)
            {
                _timePrevStep = DateTime.UtcNow;
                SoundManager.PlaySoundUI("walk");
            }

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
			Vector3 newSpeed = Vector3.zero;
			newSpeed.y = _rigidbody.velocity.y;
            _rigidbody.velocity = newSpeed;
            _rigidbody.angularVelocity = Vector3.zero;
            _animator.SetBool("Move", false);
        }
    }

    void Update()
    {
        _canPickUp = false;
        _canThrow = false;

        if (_victumInHands == null)
        {
            Victum closestVictum = VictumsController.instance.GetClosestVictum(transform.position, _pickUpDistance);
            if (closestVictum != null)
            {
                _canPickUp = true;

                if (Input.GetButtonDown("Submit"))
                {
                    _victumInHands = closestVictum;
                    _animator.SetBool("Carry", true);
                    var colliders = _victumInHands.body.GetComponentsInChildren<Collider>();
                    foreach(Collider collider in colliders)
                        Destroy(collider);
                }
            }
        } else
        {           
            _victumInHands.body.position = _bodyPos.position;
            _victumInHands.body.parent = joinPoint;

           Altar altar = AltarsController.instance.GetClosestAltar(transform.position, _pickUpDistance);
            if (altar != null)
            {
                if (altar.IsWaiting && altar.VictumType == _victumInHands.type)
                {
                    _canThrow = true;
                    if (Input.GetButtonDown("Submit"))
                    {
                        VictumsController.instance.KillVictum(_victumInHands);
                        Destroy(_victumInHands.body.gameObject);
                        _victumInHands = null;
                        _animator.SetBool("Carry", false);
                        altar.RunProcess();
                        GameContext.instance.AddPoint();
                    }
                }
                if (altar.IsSpecial)
                {
                    _canThrow = true;
                    if (Input.GetButtonDown("Submit"))
                    {
                        VictumsController.instance.KillVictum(_victumInHands);
                        Destroy(_victumInHands.body.gameObject);
                        _victumInHands = null;
                        _animator.SetBool("Carry", false);
                        altar.RunProcess();
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        float scale = Screen.height / 400f;
        GUI.matrix = Matrix4x4.Scale(Vector3.one * scale);

        GUILayout.BeginArea(new Rect((Screen.width / scale / 2) - 50, (Screen.height / scale / 2), 100, 100));

        if (_canPickUp)
        {
            GUILayout.Label("Press SPACE to pick up");
        }
        if (_canThrow)
        {
            GUILayout.Label("Press SPACE to throw");
        }
        GUILayout.EndArea();
    }
}
