using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame.Core;
using UnityEngine.Events;
using Map;
using Unity.VisualScripting;

public class StoreManager : SingletonBase<StoreManager>
{
    public bool enableDebug = false;
    public GameObject StoreUIcanvas;

    [Header("货铺生成规律节点")]
    [Tooltip("节点本身包含在更高的一级")]
    public int[] diceScoreFlag = { 15, 24 };

    public List<ProductHalidom> productHalidoms = new List<ProductHalidom>();
    public List<ProductDice> productDices = new List<ProductDice>();
    protected override void Awake()
    {
        base.Awake();
        if (StoreUIcanvas != null)
        {
            StoreUIcanvas = transform.Find("Canvas").gameObject;
        }
        StoreUIcanvas.SetActive(false);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnEnterStore.AddListener(OpenStore);
        OnExitStore.AddListener(CloseStore);
        OnClickReroll.AddListener(ClickReroll);
        OnRefreshStore.AddListener(RerollShop);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region ------UnityEvent------

    /// <summary>
    /// 进入商店时调用
    /// </summary>
    public UnityEvent OnEnterStore;
    /// <summary>
    /// 离开商店时调用
    /// </summary>
    public UnityEvent OnExitStore;
    /// <summary>
    /// 点击强化点数按钮时调用
    /// </summary>
    public UnityEvent OnClickUpgrade;
    /// <summary>
    /// 点击重投按钮时调用
    /// </summary>
    public UnityEvent OnClickReroll;
    /// <summary>
    /// 刷新商品店时调用
    /// </summary>
    public UnityEvent OnRefreshStore;
    /// <summary>
    /// 用于刷新骰子ui: 骰子序号,骰面
    /// </summary>
    public UnityEvent<int, SingleDiceObj> OnDiceRoll;

    #endregion

    public void m_Debug(string log)
    {
        if (enableDebug)
        {
            Debug.Log("Store: " + log);
        }
    }

    private void OpenStore()
    {
        StoreUIcanvas.SetActive(true);
        OnRefreshStore?.Invoke();
    }

    private void CloseStore()
    {
        StartCoroutine(LateCloseStore());
    }
    IEnumerator LateCloseStore()
    {
        yield return new WaitForSeconds(2);
        StoreUIcanvas.SetActive(false);
    }

    #region ------刷新商店------

    private void ClickReroll()
    {
        OnRefreshStore?.Invoke();
    }

    /// <summary>
    /// 刷新商店逻辑
    /// </summary>
    private void RerollShop()
    {
        List<BattleDice> battleDices = MapManager.Instance.playerChaState.GetBattleDiceHandler().battleDices;

        //获取骰子点数
        int diceScore = 0;
        foreach (var battleDice in battleDices)
        {
            battleDice.GetRandomDice(out SingleDiceObj singleDiceObj);
            OnDiceRoll?.Invoke(battleDice.diceIndexInList, singleDiceObj);
            diceScore += singleDiceObj.idInDice;
        }

        m_Debug("diceScore: " + diceScore);

        //清空所有商品
        foreach (var product in productHalidoms)
        {
            product.ClearProduct();
        }
        foreach (var product in productDices)
        {
            product.ClearProduct();
        }

        //根据点数生成商品
        if (diceScore < diceScoreFlag[0])
        {
            AddProductHalidom(RareType.Common, RareType.Common, RareType.Rare);
            AddProductDice(RareType.Common, RareType.Common, RareType.Rare);
        }
        else if (diceScore < diceScoreFlag[1])
        {
            AddProductHalidom(RareType.Common, RareType.Rare, RareType.Rare);
            AddProductDice(RareType.Common, RareType.Rare, RareType.Rare);
        }
        else
        {
            AddProductHalidom(RareType.Common, RareType.Rare, RareType.Legendary);
            AddProductDice(RareType.Common, RareType.Rare, RareType.Legendary);
        }
    }

    /// <summary>
    /// 往圣物商品格里面加一个圣物，不会和玩家已有圣物，本轮商店已有圣物重复
    /// </summary>
    /// <param name="rareType1"></param>
    /// <param name="rareType2"></param>
    /// <param name="rareType3"></param>
    private void AddProductHalidom(RareType rareType1, RareType rareType2, RareType rareType3)
    {
        int i = 0;
        while (i < 100)
        {
            HalidomObject halidomObject = RandomManager.GetRandomHalidomObj(rareType1);
            if (!AlreadyHasHalidom(halidomObject))
            {
                productHalidoms[0].InitialProduct(halidomObject);
                break;
            }
            i++;
        }
        i = 0;
        while (i < 100)
        {
            HalidomObject halidomObject = RandomManager.GetRandomHalidomObj(rareType2);
            if (!AlreadyHasHalidom(halidomObject))
            {
                productHalidoms[1].InitialProduct(halidomObject);
                break;
            }
            i++;
        }
        i = 0;
        while (i < 100)
        {
            HalidomObject halidomObject = RandomManager.GetRandomHalidomObj(rareType3);
            if (!AlreadyHasHalidom(halidomObject))
            {
                productHalidoms[2].InitialProduct(halidomObject);
                break;
            }
            i++;
        }
    }

    /// <summary>
    /// 往骰面商品格里面加一个骰面
    /// </summary>
    private void AddProductDice(RareType rareType1, RareType rareType2, RareType rareType3)
    {

        productDices[0].InitialProduct(new SingleDiceObj
            (RandomManager.GetSingleDiceModel((int)rareType1 + 1, 0), Random.Range(0, 6)));

        productDices[1].InitialProduct(new SingleDiceObj
            (RandomManager.GetSingleDiceModel((int)rareType2 + 1, 0), Random.Range(0, 6)));

        productDices[2].InitialProduct(new SingleDiceObj
            (RandomManager.GetSingleDiceModel((int)rareType3 + 1, 0), Random.Range(0, 6)));

    }
    /// <summary>
    /// 玩家和商店是否拥有圣物
    /// </summary>
    /// <param name="halidomObject"></param>
    /// <returns></returns>
    private bool AlreadyHasHalidom(HalidomObject halidomObject)
    {
        foreach (var playerHalidom in HalidomManager.Instance.halidomList)
        {
            if (playerHalidom != null)
            {
                if (playerHalidom.id == halidomObject.id)
                {
                    return true;
                }
            }
        }

        foreach (var product in productHalidoms)
        {
            if (product.product != null)
            {
                if (product.product.id == halidomObject.id)
                {
                    return true;
                }
            }
        }
        return false;
    }

    #endregion
}
