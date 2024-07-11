using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if(count == totalCount)
            {
                StartLoaded2();
            }
        });
    }

    void StartLoaded() // 확인용
    {
        var player = Managers.Resource.Instantiate("Player.prefab");
        player.AddComponent<PlayerController>();

        var snake = Managers.Resource.Instantiate("Snake_01.prefab");
        var hoblin = Managers.Resource.Instantiate("Goblin_01.prefab");
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player;
    }

    SpawningPool _spawningPool;
    void StartLoaded2() // 개선한 함수
    {
        Managers.Data.Init();

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        // 몬스터 스폰 테스트
        //for(int i = 0; i < 10; i++)
        //{
        //    Vector3 randPos = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        //    MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));
        //}

        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map_01.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;

        // Data Test : json파일을 가져왔는지 확인용
        foreach(var playerData in Managers.Data.PlayerDic.Values)
        {
            Debug.Log($"LV: {playerData.level}, HP: {playerData.maxHp}");
        }
        
        foreach(var skillData in Managers.Data.SkillDic.Values)
        {
            Debug.Log($"templateID: {skillData.templateID}, damage: {skillData.damage}");
        }
    }

}
