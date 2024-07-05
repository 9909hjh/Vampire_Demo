using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;
    float _speed = 2.0f;

    public Vector2 MoveDir
    {
        get { return _moveDir;}
        set { _moveDir = value; }
    }
    void Start()
    {
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
    }

    private void OnDestroy()
    {
        if(Managers.Game !=  null)
        {
            Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        }
    }

    void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //_moveDir = Managers.Game.MoveDir; //임시이동

        Vector3 dir = _moveDir * _speed * Time.deltaTime;
        transform.position += dir;
    }
}
