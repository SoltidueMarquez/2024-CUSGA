using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum EditState
    {
        BagDice,
        FightDice
    }
    
    [RequireComponent(typeof(Image))]
    public class EditableDiceUIObject : MonoBehaviour
    {
        [Header("物品信息")]
        [SerializeField,Tooltip("出售价格")]protected float salePrice;

        [Header("说明UI")]
        [SerializeField,Tooltip("说明UI")]protected GameObject descriptionCanvas;
        [SerializeField,Tooltip("名称Text")]protected Text nameText;
        [SerializeField,Tooltip("类型Text")]protected Text typeText;
        [SerializeField, Tooltip("稀有度Text")] protected Text levelText;
        [SerializeField,Tooltip("售价Text")]protected Text valueText;
        [SerializeField,Tooltip("基础数值Text")]protected Text baseValueText;
        [SerializeField,Tooltip("说明Text")]protected Text descriptionText;
        [SerializeField,Tooltip("点数Text")]protected Text idInDiceText;
        
        [Header("出售UI")]
        [SerializeField,Tooltip("出售UI")] protected GameObject saleUI;
        [SerializeField, Tooltip("出售按钮")] protected Button saleButton;
        [SerializeField,Tooltip("出售按钮Text")]protected Text saleButtonText;

        private EditState _editState;
        private State _state;
        private Transform _oldParent;  //原本的父物体
        public Column currentColumn;   //当前所在栏位
        
        

    }
}
