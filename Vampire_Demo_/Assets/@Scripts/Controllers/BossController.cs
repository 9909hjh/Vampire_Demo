using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();
        
        Hp = 10000; // 데이터 시트 참고해서 만들기.

        CreatureState = Define.CreatureState.Skill;

        Skills.AddSkill<Move>(transform.position); // 처음 시작시 움직이기
        Skills.AddSkill<Dash>(transform.position); // 3단 대쉬를 위해 3번 추가.
        Skills.AddSkill<Dash>(transform.position); 
        Skills.AddSkill<Dash>(transform.position); 
        Skills.StartNextSequenceSkill();

        return true;
    }

    public override void UpdateAnimation()
    {
        switch(CreatureState)
        {
            case Define.CreatureState.Idle:
                _animator.Play("Idle");
                break;
            case Define.CreatureState.Moving:
                _animator.Play("Moving");
                break;
            case Define.CreatureState.Skill:
                //_animator.Play("Attack");
                break;
            case Define.CreatureState.Dead:
                _animator.Play("Death");
                break;
        }
    }
    #region 구버전 보스움직임과 스킬 구현
    // Boss Collider + Player Collider
    //float _range = 2.5f;
    //protected override void UpdateMoving()
    //{
    //    PlayerController pc = Managers.Object.Player;
    //    if (pc.IsValid() == false)
    //        return;

    //    // 이 코드는 평타 코드인데 만약 렌덤을 돌려서 하고 싶다면 코루틴을 사용하자
    //    Vector3 dir = pc.transform.position - transform.position;

    //    if(dir.magnitude < _range)
    //    {
    //        CreatureState = Define.CreatureState.Skill;

    //        float animLength = 0.41f;
    //        Wait(animLength);
    //    }
    //}


    //protected override void UpdateSkill()
    //{
    //    if (_coWait == null)
    //        CreatureState = Define.CreatureState.Moving;
    //}
    #endregion

    protected override void UpdateDead()
    {
        if (_coWait == null)
            Managers.Object.Despawn(this);
    }

    #region WiatCoroutine
    Coroutine _coWait;

    void Wait(float waitSeconds)
    {
        if (_coWait != null)
            StopCoroutine(_coWait);

        _coWait = StartCoroutine(CoStartWait(waitSeconds));
    }

    IEnumerator CoStartWait(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        _coWait = null;
    }


    #endregion

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
    }

    protected override void OnDead()
    {
        CreatureState = Define.CreatureState.Dead;
        Wait(2.0f); // 죽는 애니메이션 시간 
        // 죽었다면 젬 말고 다른 보물 상자를 드랍 하도록 설정
        base.OnDead();
    }
}
