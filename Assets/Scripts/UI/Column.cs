using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [Serializable]
    public class Column
    {
        public Transform transform;
        public GameObject bagObject;
        public EditState state;
    }
}
