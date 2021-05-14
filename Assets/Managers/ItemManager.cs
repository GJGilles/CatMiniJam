using Assets.Types;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Managers
{
    public static class ItemManager
    {
        private static Dictionary<ItemEnum, ItemData> defs = new Dictionary<ItemEnum, ItemData>();

        public static void LoadItems(List<ItemData> items) 
        {
            foreach(var item in items)
            {
                defs.Add(item.id, item);
            }
        }

        public static ItemData GetItemData(ItemEnum item)
        {
            if (defs.ContainsKey(item))
            {
                return defs[item];
            }
            else
            {
                return new ItemData();
            }
        }
    }
}