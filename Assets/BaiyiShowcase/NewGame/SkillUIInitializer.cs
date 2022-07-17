using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace BaiyiShowcase.NewGame
{
    public class SkillUIInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private TextMeshProUGUI _skillName;
        [Required]
        [SerializeField] private TextMeshProUGUI _skillValue;

        public void Initialize(string skillName, int skillValue)
        {
            _skillName.text = skillName;
            _skillValue.text = skillValue.ToString();
        }

        public void ChangeSkillValue(int skillValue)
        {
            _skillValue.text = skillValue.ToString();
        }
    }
}