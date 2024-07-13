using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();
        
        Hp = 10000; // ������ ��Ʈ �����ؼ� �����.

        CreatureState = Define.CreatureState.Skill;

        Skills.AddSkill<Move>(transform.position); // ó�� ���۽� �����̱�
        Skills.AddSkill<Dash>(transform.position); // 3�� �뽬�� ���� 3�� �߰�.
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
    #region ������ ���������Ӱ� ��ų ����
    // Boss Collider + Player Collider
    //float _range = 2.5f;
    //protected override void UpdateMoving()
    //{
    //    PlayerController pc = Managers.Object.Player;
    //    if (pc.IsValid() == false)
    //        return;

    //    // �� �ڵ�� ��Ÿ �ڵ��ε� ���� ������ ������ �ϰ� �ʹٸ� �ڷ�ƾ�� �������
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
        Wait(2.0f); // �״� �ִϸ��̼� �ð� 
        // �׾��ٸ� �� ���� �ٸ� ���� ���ڸ� ��� �ϵ��� ����
        base.OnDead();
    }
}
