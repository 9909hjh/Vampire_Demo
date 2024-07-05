using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기는?
    // 몬스터 최대 개수는?
    // 스톱?

    float _spawnInterval = 2.0f;
    int _maxMonserCount = 100;
    Coroutine _coUpdateSpawningPool;

    void Start()
    {
        _coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
    }

    IEnumerator CoUpdateSpawningPool()
    {
        while (true)
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    void TrySpawn()
    {
        int monserCount = Managers.Object.Monsters.Count;
        if (monserCount >= _maxMonserCount)
            return;

        MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
        mc.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
    }
}
