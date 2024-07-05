using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    GameObject _snake;
    GameObject _goblin;
    


    void Start()
    {
        Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
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
        _spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>();

        for(int i = 0; i < 10; i++)
        {
            MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
            mc.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        }

        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;
    }

    void Update()
    {
        
    }
}
