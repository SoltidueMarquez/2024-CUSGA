using UnityEngine;

namespace Map
{
    public enum NodeType
    {
        EasyEnemy,
        NormalEnemy,
        HardEnemy,
        RestSite,
        Treasure,
        Store,
        Boss,
        Mystery
    }
}

namespace Map
{
    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodeType;
    }
}