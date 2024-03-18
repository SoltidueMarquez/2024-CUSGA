using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIManager : MonoSingleton<CharacterUIManager>
{
    [SerializeField, Tooltip("血条")] private Slider healthSlider;

    public void ChangeHealthSlider(int currentHealth,int maxHealth)
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}
