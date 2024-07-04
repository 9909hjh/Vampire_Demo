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
                StartLoaded();
            }
        });
    }

    void StartLoaded()
    {
        GameObject prefab = Managers.Resource.Load<GameObject>("Player.prefab");

        //Managers.Resource.LoadAsync<GameObject>("Snake_01", (go) =>
        //{
        //    // Todo
        //});

        GameObject go = new GameObject() { name = "@Monsters" };
        _snake.transform.parent = go.transform;
        _goblin.transform.parent = go.transform;

        //prefab.AddComponent<PlayerController>();
        //Camera.main.GetComponent<CameraController>()
    }

    void Update()
    {
        
    }
}
