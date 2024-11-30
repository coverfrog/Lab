using System;
using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Pool/Pool Info/Info")]
    public class PoolInfo : ScriptableObject
    {
        [Header("")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;
        
        [Header("")]
        [SerializeField] private Behaviour prefab;
        [SerializeField] private PoolOptions options;

        #region < Get >

        public string CodeName => codeName;

        public Behaviour Prefab => prefab;

        public PoolOptions Options => options;

        #endregion
        
        
    }
}

