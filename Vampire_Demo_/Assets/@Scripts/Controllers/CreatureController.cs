using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    protected float _speed = 5.0f;

    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;


    public virtual void OnDamaged(BaseController attacker, int damage)
    {
        if (Hp <= 0)
            return;

        Hp -= damage;
        if(Hp <= 0)
        {
            Hp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead() 
    { 

    }
}
