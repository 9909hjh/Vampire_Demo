using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기는?
    // 몬스터 최대 개수는?
    // 스톱?

    float _spawnInterval = 0.2f;
    int _maxMonserCount = 100;
    Coroutine _coUpdateSpawningPool;

    public bool Stoped { get; set; } = false;
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
        if (Stoped) // 보스 스테이지면 몬스터 스폰 금지
            return;

        int monserCount = Managers.Object.Monsters.Count;
        if (monserCount >= _maxMonserCount)
            return;

        Vector3 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 5, 15);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, 1 + Random.Range(0, 2));
    }
}
