using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    // Start is called before the first frame update
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    
}
