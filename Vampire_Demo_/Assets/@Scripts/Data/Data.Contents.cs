using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    // Json�� �޸𸮿� ��� ���� �� �ֵ��� �ϴ� ��ȯ �۾�.
    #region PlayerData
    [Serializable]
    public class PlayerData
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable] // <= �̰��� �̴̹� �޸𸮿� ��� �ִ� ���� ���Ϸ� ��ȯ�� �� �ְ� ���ش�.
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
        // - ���� Ȯ����
        // - � �������� (����, ��ų ����, ���, ���)
        // - �� �� �������?
    }

    [Serializable] // <= �̰��� �̴̹� �޸𸮿� ��� �ִ� ���� ���Ϸ� ��ȯ�� �� �ְ� ���ش�.
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
