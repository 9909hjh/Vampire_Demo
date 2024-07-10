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

    public Vector2 MoveDir
    {
        get { return _moveDir;}
        set { _moveDir = value; }
    }

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        StartProjectile();

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

    // 임시 코드
    #region FireProjectile

    Coroutine _coFireProjectile;

    void StartProjectile()
    {
        if(_coFireProjectile !=  null)
            StopCoroutine(_coFireProjectile);

        _coFireProjectile = StartCoroutine(CoStartProjectile());
    }

    IEnumerator CoStartProjectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while(true)
        {
            ProjectileController pc = Managers.Object.Spawn<ProjectileController>(_fireSocket.position, 1);
            pc.SetInfo(1, this, (_fireSocket.position - _indicator.position).normalized);
            yield return wait;
        }
    }

    #endregion
}
