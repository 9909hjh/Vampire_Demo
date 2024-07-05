using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    // Json이 메모리에 들고 있을 수 있도록 하는 변환 작업.
    #region PlayerData
    [Serializable]
    public class PlayerData
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable] // <= 이것의 이미는 메모리에 들고 있는 것을 파일로 변환할 수 있게 해준다.
    public class PlayerDataLoader : ILoader<int, PlayerData>
    {
        public List<PlayerData> stats = new List<PlayerData>();

        public Dictionary<int, PlayerData> MakeDict()
        {
            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
            foreach (PlayerData stat in stats)
            {
                dict.Add(stat.level, stat);
            }
            return dict;
        }
    }
    #endregion

    #region MonsterData
    [Serializable]
    public class MonsterData
    {
        public string name;
        public string prefab;
        public int level;
        public int maxHp;
        public int attack;
        public float speed;
        // DropData
        // - 일정 확률로
        // - 어떤 아이템을 (보석, 스킬 가차, 골드, 고기)
        // - 몇 개 드랍할지?
    }

    [Serializable] // <= 이것의 이미는 메모리에 들고 있는 것을 파일로 변환할 수 있게 해준다.
    public class MonsterDataLoader : ILoader<string, MonsterData>
    {
        public List<MonsterData> stats = new List<MonsterData>();

        public Dictionary<string, MonsterData> MakeDict()
        {
            Dictionary<string, MonsterData> dict = new Dictionary<string, MonsterData>();
            foreach (MonsterData stat in stats)
            {
                dict.Add(stat.name, stat);
            }
            return dict;
        }
    }

    #endregion
}
