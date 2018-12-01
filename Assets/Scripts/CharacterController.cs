using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField]
    float _speed;

    Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    void FixedUpdate() {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var direction = new Vector3(h, 0, v);
        if (direction != Vector3.zero)
        {
            direction.Normalize();
            _rigidbody.velocity = direction * _speed;
        }
    }
}
