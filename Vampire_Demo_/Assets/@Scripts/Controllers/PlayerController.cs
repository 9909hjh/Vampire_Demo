using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 _moveDir = Vector2.zero;
    float _speed = 5.0f;

    public Vector2 MoveDir
    {
        get { return _moveDir;}
        set { _moveDir = value; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        _moveDir = Managers.MoveDir; //임시이동

        Vector3 dir = _moveDir * _speed * Time.deltaTime;
        transform.position += dir;
    }
}
