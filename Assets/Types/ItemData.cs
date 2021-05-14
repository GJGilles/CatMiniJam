using System;
using UnityEngine;

namespace Assets.Types
{
    public enum ItemEnum
    {
        None = -1,
        Default
    }

    [Serializable]
    public class ItemData
    {
        public ItemEnum id;
        public string name = "";
        public int stack = 0;
        public Sprite sprite;

       public ItemData() { }
       public ItemData(string name, int stack) 
       {
            this.name = name;
            this.stack = stack;
       }
    }
}