using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Cf
{
    public abstract class MoveAct : ScriptableObject
    {
        public abstract void MoveBegin();

        public abstract void Moving();

        public abstract void MoveEnd();
    }
}
