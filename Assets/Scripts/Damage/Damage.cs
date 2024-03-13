using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage
{
    public int baseDamage;
    public Damage(int baseDamage)
    {
        this.baseDamage = baseDamage;
    }


    public static Damage operator +(Damage a, Damage b)
    {
        return new Damage(a.baseDamage + b.baseDamage);
    }

}

