using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;
    //float m_speed = 2.0f;

    float EnvCollectDist { get; set; } = 1.0f;

    [SerializeField] Transform _indicator;
    [SerializeField] Transform _fireSocket;

    public Transform Indicator { get { return _indicator; } }
    public Vector3 FireSocket { get { return _fireSocket.position; } }
    public Vector3 ShootDir { get { return (_fireSocket.position - _indicator.position).normalized; } }

    public Vector2 MoveDir
    {
        get { return _moveDir;}
        set { _moveDir = value; }
    }

    public override bool Init()
    {
        Debug.Log("Init method called"); // 로그 추가
        if (base.Init() == false)
            return false;

        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        //스킬 적용
        Skills.AddSkill<FireballSkill>(transform.position);
        Skills.AddSkill<EgoSword>(_indicator.position);

        return true;
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

        if(_moveDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
        }

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDist * EnvCollectDist;

        //List<GemController> gems = Managers.Object.Gems.ToList();

        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().
            GatherObjects(transform.position, EnvCollectDist + 0.5f);

        foreach (var go in findGems)
        {
            GemController gem = go.GetComponent<GemController>();

            Vector3 dir = gem.transform.position - transform.position;
            if(dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Game.Gem += 1;

                Managers.Object.Despawn(gem);
            }
        }

        //Debug.Log($"SearchGems({findGems.Count}) TotalGams({gems.Count})");
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

        // 임시코드 확인용 : 몸에 닿으면 큰 데미지
        //CreatureController cc = attacker as CreatureController;
        //cc?.OnDamaged(this, 10000);
    }
}
