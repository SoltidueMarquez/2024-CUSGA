using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FightDicePageUIEffect : MonoBehaviour
    {
        [SerializeField, Tooltip("名字")] private Text nameText;

        public void Init(string name)
        {
            nameText.text = name;
        }

    }
}
