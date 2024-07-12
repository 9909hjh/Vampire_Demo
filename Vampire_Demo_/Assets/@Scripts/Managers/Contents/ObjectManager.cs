using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();
    public HashSet<SkillController> Skills { get; } = new HashSet<SkillController>();
    public HashSet<GemController> Gems { get; } = new HashSet<GemController>();

    public T Spawn<T>(Vector3 position, int templateID = 0) where T : BaseController // 만약 키 값을 숫자가 아니고 이름으로 하고 싶으면 string으로 변경
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            // Todo : Data
            GameObject go = Managers.Resource.Instantiate("Player.prefab", pooling: true);
            go.name = "Player";
            go.transform.position = position;

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;
            pc.Init();

            return pc as T;
        }
        else if (type == typeof(MonsterController))
        {
            // 임시 코드
            //string name = (templateID == 0 ? "Goblin_01" : "Snake_01");

            string name = ""; 

            switch(templateID)
            {
                case Define.GOBLIN_ID:
                    name = "Goblin_01";
                    break;
                case Define.SNAKE_ID:
                    name = "Snake_01";
                    break;
                case Define.BOSS_ID:
                    name = "Boss_01";
                    break;
            }

            GameObject go = Managers.Resource.Instantiate(name + ".prefab", pooling: true);
            go.transform.position = position;

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            Monsters.Add(mc);
            mc.Init();

            return mc as T;
        }
        else if (type == typeof(GemController))
        {
            GameObject go = Managers.Resource.Instantiate(Define.EXP_GEM_PREFAB, pooling: true);
            go.transform.position = position;

            GemController gc = go.GetOrAddComponent<GemController>();
            Gems.Add(gc);
            gc.Init();

            string key = Random.Range(0, 2) == 0 ? "Expgem_0.sprite" : "Expgem_1.sprite";
            Sprite sprite = Managers.Resource.Load<Sprite>(key);
            go.GetComponent<SpriteRenderer>().sprite = sprite;

            // 임시 test
            GameObject.Find("@Grid").GetComponent<GridController>().Add(go);

            return gc as T;
        }
        else if(type == typeof(ProjectileController))
        {
            GameObject go = Managers.Resource.Instantiate("FireProjectile.prefab", pooling : true);
            go.transform.position = position;

            ProjectileController pc = go.GetOrAddComponent<ProjectileController>();
            Projectiles.Add(pc);
            pc.Init();

            return pc as T;
        }
        else if (typeof(T).IsSubclassOf(typeof(SkillController)))
        {
            if(Managers.Data.SkillDic.TryGetValue(templateID, out Data.SkillData skillData) == false)
            {
                Debug.LogError($"ObjectManager Spawn Skill Failed {templateID}");
                return null;
            }

            GameObject go = Managers.Resource.Instantiate(skillData.prefab, pooling: true);
            go.transform.position = position;

            SkillController sc = go.GetOrAddComponent<SkillController>();
            Skills.Add(sc);
            sc.Init();

            return sc as T;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        if (obj.IsValid() == false)
            return;

        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            // Todo
        }
        else if (type == typeof(MonsterController))
        {
            Monsters.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if(type == typeof(GemController))
        {
            Gems.Remove(obj as GemController);
            Managers.Resource.Destroy(obj.gameObject);

            // 임시 test
            GameObject.Find("@Grid").GetComponent<GridController>().Remove(obj.gameObject);
        }
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(SkillController))
        {
            Skills.Remove(obj as SkillController);
            Managers.Resource.Destroy(obj.gameObject);
        }
    }

    public void DespawnAllMonsters()
    {
        var monsters = Monsters.ToList();

        foreach (var monster in monsters)
        {
            Despawn<MonsterController>(monster);
        }
    }
}
