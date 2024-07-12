using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // ������ �ֱ��?
    // ���� �ִ� ������?
    // ����?

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
        if (Stoped) // ���� ���������� ���� ���� ����
            return;

        int monserCount = Managers.Object.Monsters.Count;
        if (monserCount >= _maxMonserCount)
            return;

        Vector3 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 5, 15);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, 1 + Random.Range(0, 2));
    }
}
