using UnityEngine;
using UnityEngine.UI;

public class RollingResultDiceUI : MonoBehaviour
{
    [SerializeField,Tooltip("投掷结果序列号")]private int index;
    [SerializeField,Tooltip("所在战斗骰面位置")]private Vector2 pageAndIndex;

    //TODO: 生成函数还没做
    public void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            BattleManager.Instance.parameter.playerChaStates.UseDice(index, BattleManager.Instance.currentSelectEnemy);
        });
    }
}
