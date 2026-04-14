using UnityEngine;
using System.Collections.Generic;

namespace Core.Attacks.AttackSettings
{
    [CreateAssetMenu(fileName = "AttackList", menuName = "Configs/AttackList")]
    public class AttackList : ScriptableObject
    {
        [SerializeField] public List<AttackParameters> attackParameters;
    }
}