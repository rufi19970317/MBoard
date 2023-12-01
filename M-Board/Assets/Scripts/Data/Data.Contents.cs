using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// 필수 인터페이스
public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

namespace Data
{
    #region PassiveStat

    [Serializable]
    public class PassiveStat
    {
        public string name;
        public int level;

        public static PassiveStat DeepCopy(PassiveStat passive)
        {
            return passive;
        }
    }

    // 데이터 저장 자동화
    [Serializable]
    public class PassiveStatData : ILoader<string, PassiveStat>
    {
        public List<PassiveStat> weaponStats = new List<PassiveStat>();

        public Dictionary<string, PassiveStat> MakeDict()
        {
            Dictionary<string, PassiveStat> dict = new Dictionary<string, PassiveStat>();
            foreach (PassiveStat passiveStat in weaponStats)
                dict.Add(passiveStat.name, passiveStat);
            return dict;
        }
    }
    #endregion
}