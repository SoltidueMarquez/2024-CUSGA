using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame.Core;
using UnityEngine.Events;
using Map;
using UI.Store;
using System;
using Sirenix.OdinInspector;
using UI.Tutorial;

public class StoreManager : PersistentSingleton<StoreManager>
{
    public bool enableDebug = false;
    public GameObject StoreUIcanvas;

    public float discount = 1;

    [Header("货铺生成规律节点")]
    [Tooltip("节点本身包含在更高的一级")]
    public int[] diceScoreFlag = { 15, 24 };

    [Header("强化点数")]
    [Tooltip("初始价格")] public int upgradeCostOrigin = 2;
    [Tooltip("价格增幅")] public int upgradeCostAdd = 2;
    private int upgradeCost = 2;

    [Header("方便测试")]
    [Tooltip("小于则不指定")] public int pointRollResult = -1;

    [Header("商品格子")]
    public List<ProductHalidom> productHalidoms = new List<ProductHalidom>();
    public List<ProductDice> productDices = new List<ProductDice>();

    private int upgradePoint = 1;

    private ChaState player;

    private int clickNum;
    protected override void Awake()
    {
        base.Awake();
        if (StoreUIcanvas != null)
        {
            StoreUIcanvas = transform.Find("Canvas").gameObject;
        }
        StoreUIcanvas.SetActive(false);


    }

    // Start is called before the first frame update
    void Start()
    {
        OnEnterStore.AddListener(OpenStore);
        OnExitStore.AddListener(CloseStore);
        OnRefreshStore.AddListener(RerollShop);
        OnRefreshStore.AddListener(MapManager.Instance.UpdatePlayerUI);
        OnClickUpgrade.AddListener(ClickUpgrade);

        player = MapManager.Instance.playerChaState;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region ------UnityEvent------

    [Header("事件")]
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
    /// 刷新商品店时调用
    /// </summary>
    public UnityEvent OnRefreshStore;
    /// <summary>
    /// 用于刷新骰子ui: 骰子序号,骰面
    /// </summary>
    public UnityEvent<int, SingleDiceObj> OnDiceRoll;
    /// <summary>
    /// 强化失败时调用
    /// </summary>
    public UnityEvent<BuyFailType> OnUpgradeFail;
    /// <summary>
    /// 强化成功时调用
    /// </summary>
    public UnityEvent<UpgradeInfo> OnUpgradeSuccess;

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

        //重置刷新次数
        player.resource.currentRollTimes = player.baseProp.maxRollTimes;

        //重置强化费用
        upgradeCost = upgradeCostOrigin;
    }

    private void CloseStore()
    {
        StartCoroutine(LateCloseStore());
        MapManager.Instance.OnExitShop();
    }
    IEnumerator LateCloseStore()
    {
        yield return new WaitForSeconds(StoreUIManager.Instance.animTime * 0.75f);
        StoreUIcanvas.SetActive(false);
    }

    #region ------刷新商店------

    /// <summary>
    /// 按下重投按钮
    /// </summary>
    public void ClickReroll()
    {
        if (player.resource.currentRollTimes > 0)
        {
            player.ModResources(new ChaResource(0, 0, -1, 0));
            HalidomManager.Instance.OnReroll();
            MapManager.Instance.UpdatePlayerUI();
            OnRefreshStore?.Invoke();
        }
        else
        {
            m_Debug("重投次数不足");
        }
    }

    /// <summary>
    /// 刷新商店逻辑
    /// </summary>
    private void RerollShop()
    {
        List<BattleDice> battleDices = MapManager.Instance.playerChaState.GetBattleDiceHandler().battleDices;

        //获取骰子点数
        int diceScore = 0;
        int i = 0;
        RollUIManager.Instance.RemoveAllResultUI();
        foreach (var battleDice in battleDices)
        {
            battleDice.GetRandomDice(out SingleDiceObj singleDiceObj);
            RollUIManager.Instance.CreateResult(i, ResourcesManager.GetSingleDiceUIData(singleDiceObj),
                new Vector2Int(i, singleDiceObj.positionInDice));
            OnDiceRoll?.Invoke(battleDice.diceIndexInList, singleDiceObj);
            diceScore += singleDiceObj.idInDice;
            i++;
        }

#if UNITY_EDITOR
        if (pointRollResult >= 0)
        {
            diceScore = pointRollResult;
        }
        m_Debug("diceScore: " + diceScore);
#endif
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
            (RandomManager.GetSingleDiceModel((int)rareType1 + 1, 0), UnityEngine.Random.Range(1, 7)));

        productDices[1].InitialProduct(new SingleDiceObj
            (RandomManager.GetSingleDiceModel((int)rareType2 + 1, 0), UnityEngine.Random.Range(1, 7)));

        productDices[2].InitialProduct(new SingleDiceObj
            (RandomManager.GetSingleDiceModel((int)rareType3 + 1, 0), UnityEngine.Random.Range(1, 7)));

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

    /// <summary>
    /// 改变商店折扣，打折为0-1，涨价为>1（在商店购买后会立即生效，而不是等下次进商店）
    /// </summary>
    /// <param name="newDiscount"></param>
    [Button]
    public void ChangeDiscount(float newDiscount)
    {
        this.discount = newDiscount;

        if (StoreUIcanvas.activeSelf)
        {

            foreach (var item in productDices)
            {
                item.RefreshDiscount();
            }

            foreach (var item in productHalidoms)
            {
                item.RefreshDiscount();
            }
            StoreAreaUIManager.Instance.RefreshDiceUI();
            StoreAreaUIManager.Instance.RefreshHalidomUI();
        }

    }

    #endregion

    #region------强化商店------
    private void ClickUpgrade()
    {
        //出现教程
        if (clickNum == 0 && GameManager.Instance.ifTutorial)
        {
            TutorialManager.Instance.EnterUI(TutorPage.Strength);
        }
        clickNum++;
        
        StrengthenAreaManager.Instance.RefreshUpgradeText(upgradeCost);

        BattleDiceHandler handler = MapManager.Instance.playerChaState.GetBattleDiceHandler();

        List<BattleDice> battleDices = handler.battleDices;
        List<List<SingleDiceObj>> createList = new List<List<SingleDiceObj>>();
        List<List<Action<SingleDiceObj>>> createActionList = new List<List<Action<SingleDiceObj>>>();

        //加入使用中的骰子
        for (int i = 0; i < battleDices.Count; i++)
        {
            List<SingleDiceObj> singleDiceObjs = new List<SingleDiceObj>();
            List<Action<SingleDiceObj>> actions = new List<Action<SingleDiceObj>>();
            for (int j = 0; j < battleDices[i].GetBattleDiceSingleDices().Count; j++)
            {
                SingleDiceObj obj = battleDices[i].GetBattleDiceSingleDices()[j];
                singleDiceObjs.Add(obj);
                Action<SingleDiceObj> action = new Action<SingleDiceObj>(TryUpgrade);
                actions.Add(action);
            }

            createList.Add(singleDiceObjs);
            createActionList.Add(actions);
        }

        //加入背包中的骰子
        int unAddCount = handler.bagDiceCards.Count;

        //循环次数：6个一组，算出需要执行的组数
        for (int i = 0; i < handler.bagDiceCards.Count / 6 + 1; i++)
        {

            List<SingleDiceObj> singleDiceObjs = new List<SingleDiceObj>();
            List<Action<SingleDiceObj>> actions = new List<Action<SingleDiceObj>>();

            for (int j = 0; j < 6; j++)
            {
                if (unAddCount <= 0)
                {
                    break;//最后一组才会弹出
                }
                SingleDiceObj obj = handler.bagDiceCards[i * 6 + j];
                singleDiceObjs.Add(obj);
                Action<SingleDiceObj> action = new Action<SingleDiceObj>(TryUpgrade);
                actions.Add(action);
                StrengthenAreaManager.Instance.CreateBagDiceUI(i * 6 + j, obj, action);
                unAddCount--;
            }

            //createList.Add(singleDiceObjs);
            //createActionList.Add(actions);
        }
        StrengthenAreaManager.Instance.CreateFightDicePage(createList, createActionList);
        StoreUIManager.Instance.EnterUpgradeUI();

        //StoreUIManager.Instance.RefreshUpgradeUI();
    }

    private void TryUpgrade(SingleDiceObj singleDiceObj)
    {
        if (player.resource.currentMoney < upgradeCost)
        {
            OnUpgradeFail?.Invoke(BuyFailType.NoMoney);
            return;
        }
        else if (singleDiceObj.idInDice >= 6)
        {
            OnUpgradeFail?.Invoke(BuyFailType.DicePointMax);
            return;
        }

        //------强化成功------
        //扣钱
        player.ModResources(new ChaResource(0, -upgradeCost, 0, 0));
        upgradeCost += upgradeCostAdd;

        //判断加成是否溢出
        if (singleDiceObj.idInDice + upgradePoint > 6)
        {
            int actualUpgradePoint = 6 - singleDiceObj.idInDice;
            singleDiceObj.idInDice = 6;
            OnUpgradeSuccess?.Invoke(new UpgradeInfo(singleDiceObj, actualUpgradePoint));
        }
        else
        {
            singleDiceObj.idInDice += upgradePoint;
            OnUpgradeSuccess?.Invoke(new UpgradeInfo(singleDiceObj, upgradePoint));
        }

        StrengthenAreaManager.Instance.RefreshUpgradeText(upgradeCost);
    }

    /// <summary>
    /// 改变一次强化的点数
    /// </summary>
    /// <param name="point">小于6</param>
    public void ChangeUpgradePoint(int point)
    {
        upgradePoint = point;
    }

    #endregion
    public void SetPlayer(ChaState chaState)
    {
        player = chaState;
    }
}

/// <summary>
/// 用于传递升级信息
/// </summary>
public class UpgradeInfo
{
    private SingleDiceObj singleDiceObj;
    /// <summary>
    /// 升级完毕的骰面信息
    /// </summary>
    public SingleDiceObj _singleDiceObj { get { return singleDiceObj; } }
    private int actualUpgradePoint = 1;
    /// <summary>
    /// 此次升级增加了几点数（0-5）
    /// </summary>
    public int _actualUpgradePoint { get { return actualUpgradePoint; } }

    public UpgradeInfo(SingleDiceObj singleDiceObj, int actualUpgradePoint = 1)
    {
        this.singleDiceObj = singleDiceObj;
        this.actualUpgradePoint = actualUpgradePoint;
    }
}
