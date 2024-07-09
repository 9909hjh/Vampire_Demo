using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.PlayerData> PlayerDic { get; private set; } = new Dictionary<int, Data.PlayerData>();
    public Dictionary<int, Data.SkillData> SkillDic { get; private set; } = new Dictionary<int, Data.SkillData>();


    public void Init()
    {
        PlayerDic = LoadJson<Data.PlayerDataLoader, int, Data.PlayerData>("PlayerData.json").MakeDict();
        SkillDic = LoadJson<Data.SkillDataLoader, int, Data.SkillData>("SkillData.json").MakeDict();
    }

    Loader LoadJson<Loader, key, Value>(string path) where Loader : ILoader<key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
