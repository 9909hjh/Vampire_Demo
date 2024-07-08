using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;
    //float m_speed = 2.0f;

    float EnvCollectDist { get; set; } = 1.0f;
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
        CollectEnv();
    }

    void MovePlayer()
    {
        //_moveDir = Managers.Game.MoveDir; //임시이동

        Vector3 dir = _moveDir * _speed * Time.deltaTime;
        transform.position += dir;
    }

    void CollectEnv()
    {
        List<GemController> gems = Managers.Object.Gems.ToList();
        foreach(GemController gem in gems)
        {
            Vector3 dir = gem.transform.position - transform.position;
            if(dir.magnitude <= EnvCollectDist)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }
        }

        // 임시
        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().
            GatherObjects(transform.position, EnvCollectDist + 0.5f);

        Debug.Log($"SearchGems({findGems.Count}) TotalGams({gems.Count})");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController target = collision.gameObject.GetComponent<MonsterController>();
        if(target == null)
        {
            return;
        }

    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        Debug.Log("OnDamaged : " + Hp);

        // 임시코드 확인용
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }
}
