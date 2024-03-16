using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage
{
    public int baseDamage;
    public int indexDamage;
    public Damage(int baseDamage,int indexDamage)
    {
        this.baseDamage = baseDamage;
        this.indexDamage = indexDamage;
    }


    public static Damage operator +(Damage a, Damage b)
    {
        return new Damage(a.baseDamage + b.baseDamage,a.indexDamage + b.indexDamage);
    }

}

