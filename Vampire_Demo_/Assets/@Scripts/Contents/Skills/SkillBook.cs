using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    // 스킬 북이지만 일종의 스킬 매니저의 느낌으로 제작
    public List<SkillBase> Skills { get; } = new List<SkillBase>();
    public List<SkillBase> RepeatedSkills { get; } = new List<SkillBase>();
    // 프리팹으로 제작할까
    public List<SequenceSkill> sequenceSkills { get; } = new List<SequenceSkill>();

    public T AddSkill<T>(Vector3 position, Transform parent = null) where T : SkillBase
    {
        System.Type type = typeof(T);

        if(type == typeof(EgoSword))
        {
            var egoSword = Managers.Object.Spawn<EgoSword>(position, Define.EGO_SWORD_ID);
            egoSword.transform.SetParent(parent);
            egoSword.ActivateSKill();

            Skills.Add(egoSword);
            RepeatedSkills.Add(egoSword);

            return egoSword as T;

        }
        else if(type == typeof(FireballSkill))
        {
            var fireball = Managers.Object.Spawn<FireballSkill>(position, Define.FIRE_BALL_ID);
            fireball.transform.SetParent(parent);
            fireball.ActivateSKill();

            Skills.Add(fireball);
            RepeatedSkills.Add(fireball);

            return fireball as T;
        }
        else
        {

        }

        return null;
    }
}
