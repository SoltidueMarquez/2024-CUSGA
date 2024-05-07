

using UnityEngine;

public interface IBaseProductEffect 
{
    public  void Use();
}

public abstract class BaseProductEffect :ScriptableObject, IBaseProductEffect
{
    public abstract void Use();
}
