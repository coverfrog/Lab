using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Pool/Pool Info/Info")]
    public class PoolInfo : ScriptableObject
    {
        [Title("Text")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;
        
        [Title("T")] 
        [SerializeField] private Behaviour prefab;
        [SerializeField] private PoolOptions options;

        #region < Get >

        public string CodeName => codeName;

        public Behaviour Prefab => prefab;

        public PoolOptions Options => options;

        #endregion
        
        
    }
}

