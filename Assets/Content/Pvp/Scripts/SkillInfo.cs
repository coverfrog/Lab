using UnityEngine;

namespace Pvp
{
    [CreateAssetMenu(menuName = "Pvp/Skill/Info", fileName = "Skill Info")]
    public class SkillInfo : ScriptableObject
    {
        [Header("< Text >")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;
        
        [Header("< Option >")]
        [SerializeField] private SkillActionType actionType;
        [SerializeField] private SkillTargetName targetName;
        [SerializeField] private SkillDamageType damageType;
    }
}
