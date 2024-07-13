using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepeatSkill : SkillBase
{
    public float CoolTime { get; set; } = 1.0f;

    public RepeatSkill() : base(Define.SkillType.Repeat)
    {

    }

    #region CoSKill

    Coroutine _coSKill;
    public override void ActivateSKill()
    {
        if(_coSKill != null )
        {
            StopCoroutine( _coSKill );
        }

        _coSKill = StartCoroutine(CoStartSkill());
    }

    protected abstract void DoSkillJob();

    // 일만적인 스킬
    protected virtual IEnumerator CoStartSkill()
    {
        WaitForSeconds wait = new WaitForSeconds( CoolTime );

        while(true)
        {
            DoSkillJob();

            yield return wait;
        }
    }

    #endregion
}
