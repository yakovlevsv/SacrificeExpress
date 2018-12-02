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
            _animator.SetBool("Move", true);

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
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
               

            foreach (Transform alter in _altars)
            {
                if (Helpers.GetDistance(alter.position, transform.position) < _pickUpDistance)
                {
                    _canThrow = true;
                    if (Input.GetButtonDown("Submit"))
                    {
                        VictumsController.instance.KillVictum(_victumInHands);
                        Destroy(_victumInHands.body.gameObject);
                        _victumInHands = null;
                        _animator.SetBool("Carry", false);
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
