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

    void StartLoaded() // Ȯ�ο�
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
    void StartLoaded2() // ������ �Լ�
    {
        Managers.Data.Init();
        Managers.UI.ShowSceneUI<UI_GameScene>();

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        // ���� ���� �׽�Ʈ
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

        // Data Test : json������ �����Դ��� Ȯ�ο�
        foreach(var playerData in Managers.Data.PlayerDic.Values)
        {
            Debug.Log($"LV: {playerData.level}, HP: {playerData.maxHp}");
        }
        
        foreach(var skillData in Managers.Data.SkillDic.Values)
        {
            Debug.Log($"templateID: {skillData.templateID}, damage: {skillData.damage}");
        }

        Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;
    }

    int _collectedGemCount = 0;
    int _remainingTotalGemCount = 10;
    public void HandleOnGemCountChanged(int gemCount)
    {
        // ��ų ���׷��̵� �˾��� ���� ����ġ ���� �ø���. (��ȹ �ǵ��� ���� ����)
        _collectedGemCount++;

        if (_collectedGemCount == _remainingTotalGemCount)
        {
            Managers.UI.ShowPop<UI_SkillSelectPopup>();
            _collectedGemCount = 0;
            _remainingTotalGemCount *= 2;
        }

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)_collectedGemCount / _remainingTotalGemCount);
    }

    // ������ �Ǿ��ٸ��� ���� -> �޸� �� ����
    private void OnDestroy()
    {
        if(Managers.Game != null)
        {
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        }
    }
}
