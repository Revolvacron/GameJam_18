using UnityEngine;
using System.Collections.Generic;

namespace EVE.SOF
{
    [System.Serializable]
    public class SOFCacheDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        [HideInInspector]
        private TKey[] _keyStore = null;
        [SerializeField]
        [HideInInspector]
        private TValue[] _valueStore = null;

        public void UpdateSerialization()
        {
            int n = this.Count;
            _keyStore = new TKey[n];
            _valueStore = new TValue[n];

            int i = 0;
            foreach (var kvp in this)
            {
                _keyStore[i] = kvp.Key;
                _valueStore[i] = kvp.Value;
                ++i;
            }
        }

        public void OnAfterDeserialize()
        {
            if (_keyStore != null && _valueStore != null && _keyStore.Length == _valueStore.Length)
            {
                this.Clear();
                int n = _keyStore.Length;
                for (int i = 0; i < n; ++i)
                {
                    this[_keyStore[i]] = _valueStore[i];
                }
            }
        }

        public void OnBeforeSerialize()
        {
            // DO NOTHING...
        }
    }

    [System.Serializable]
    public class HullStore : SOFCacheDictionary<string, EveSOFHull> { }
    [System.Serializable]
    public class FactionStore : SOFCacheDictionary<string, EveSOFFaction> { }
    [System.Serializable]
    public class RaceStore : SOFCacheDictionary<string, EveSOFRace> { }
    [System.Serializable]
    public class MaterialStore : SOFCacheDictionary<string, EveSOFMaterial> { }

    /// <summary>
    /// Cache used to store SOF data.
    /// </summary>
    [System.Serializable]
    public class EveSOFDataCache
    {
        /// <summary>
        /// Hulls stored by name.
        /// </summary>
        [SerializeField]
        public HullStore hulls = new HullStore();
        /// <summary>
        /// Factions stored by name.
        /// </summary>
        [SerializeField]
        public FactionStore factions = new FactionStore();
        /// <summary>
        /// Races stored by name.
        /// </summary>
        [SerializeField]
        public RaceStore races = new RaceStore();
        /// <summary>
        /// Materials stored by name.
        /// </summary>
        [SerializeField]
        public MaterialStore materials = new MaterialStore();

        /// <summary>
        /// Updates the serializable data held in each of the stores.
        /// 
        /// Should be triggered when any of the stores are changed.
        /// </summary>
        public void UpdateSerialization()
        {
            hulls.UpdateSerialization();
            factions.UpdateSerialization();
            races.UpdateSerialization();
            materials.UpdateSerialization();
        }
    }
}
