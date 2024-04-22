using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanvas : MonoBehaviour
{

    public void OnClickYes()
    {
        this.gameObject.SetActive(false);
        StartManager.Instance.StartNewGame();
    }

    public void OnClickNo()
    {
        this.gameObject.SetActive(false);
    }
}
