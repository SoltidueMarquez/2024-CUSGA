using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataUIManager : MonoSingleton<DataUIManager>
{
    [SerializeField, Tooltip("回合数文本")] private Text RunText;
    public void UpdateRunTimeText(int run)
    {
        RunText.text = $"当前回合:{run}";
    }
}
