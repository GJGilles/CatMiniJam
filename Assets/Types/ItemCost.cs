using System;
using UnityEngine;

namespace Assets.Types
{
    [Serializable]
    public class ItemCost
    {
        public ItemEnum item = ItemEnum.Default;
        public int number = 1;
    }
}