using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoSwordChild : MonoBehaviour
{
    BaseController _onwer;
    int _damage;

    public void SetInfo(BaseController onwer, int damage)
    {
        _onwer = onwer;
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.transform.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
            return;

        mc.OnDamaged(_onwer, _damage);
    }
}
