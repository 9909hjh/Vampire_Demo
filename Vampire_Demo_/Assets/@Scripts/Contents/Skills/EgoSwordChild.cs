using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoSwordChild : MonoBehaviour
{
    BaseController _onwer;
    int _damage;

    public void SetInfo(BaseController onwer, int damage)
    {
        // 나중에 여기에 스킬 데이터를 들고와서 적용시키는 방식으로 하자 projectile을 참고
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
