using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Pool/Pool Option")]
    public class PoolOptions : ScriptableObject
    {
        [SerializeField] protected PoolType poolType = PoolType.Stack;
        [SerializeField] protected bool collectionChecks = true;
        [SerializeField] protected int maxPoolSize = 10_000;
        [SerializeField] [ShowIf("IsTypeStack")] protected int defaultCapacity = 10;

        #region < Get >

        public PoolType GetPoolType => poolType;
        public bool GetCollectionChecks => collectionChecks;
        public int GetDefaultCapacity => defaultCapacity;
        public int GetMaxPoolSize => maxPoolSize;

        #endregion
        
        #region < Condition >
        
        private bool IsTypeStack => poolType == PoolType.Stack;
        
        private bool IsTypeLinkedList => poolType == PoolType.LinkedList;

        #endregion
        
        #region < Tip >

        #region < Pool Type >

        /*
            [ Good Scenario ]
            > Stack : Bullet, Arrow
            > Simple

            > Linked List : Network Packet
            > Require Add Condition

            [ Stack ]
            > System
                - Last In, First Out
            > Read, Write Speed
                - O(1)
            > Good
                - Low Use Memory, Cash
            > Bad
                - Task At Target ( X )

            [ Linked List ]
            > System
                - First In, First Out
                - Last In, First Out
            > Read, Write Speed
                - O(1)
            > Good
                - Task At Target ( O )
                - Dual System
                - Large Size Can Use
            > Bad
                - Expensive Use Memory, Cash
         */
        #endregion

        #region < CollectionChecks >

        /*
            [ Suggest ]
            > True Condition

            [ Act ]
            > None Overlap
            > Check Object Is Not Return In My Pool
         */

        #endregion
        
        #endregion
    }
}
