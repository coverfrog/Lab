using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cf
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public Dictionary<TKey, TValue> ToDictionary() => dictionary;

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (var kvp in dictionary)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            dictionary = new Dictionary<TKey, TValue>();

            if (keys.Count != values.Count)
                throw new Exception("Keys and values count mismatch!");

            for (int i = 0; i < keys.Count; i++)
                dictionary[keys[i]] = values[i];
        }
    }
}
