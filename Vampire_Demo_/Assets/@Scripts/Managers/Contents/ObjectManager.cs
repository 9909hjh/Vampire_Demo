using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();

    public T Spawn<T>(int templateID = 0) where T : BaseController // 만약 키 값을 숫자가 아니고 이름으로 하고 싶으면 string으로 변경
    {
        System.Type type = typeof(T);

        if(type == typeof(PlayerController))
        {
            // Todo : Data
            GameObject go = Managers.Resource.Instantiate("Player.prefab", pooling: true);
            go.name = "Player";

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;

            return pc as T;
        }
        else if(type == typeof(MonsterController))
        {
            // 임시 코드
            string name = (templateID == 0 ? "Goblin_01" : "Snake_01");
            GameObject go = Managers.Resource.Instantiate(name + ".prefab", pooling: true);

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            Monsters.Add(mc);

            return mc as T;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
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
        else  if(type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
    }
}
