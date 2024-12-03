using UnityEngine;

namespace Pvp
{
    public enum SkillTargetName
    {
        Self,
        Enemy,
        Ally,
        Mix,
    }

    public enum SkillDamageType
    {
        Damage,
        Health,
        Mix,
    }

    public enum SkillActionType
    {
        Self,
        ToTarget,
        ToPoint,
    }
}
