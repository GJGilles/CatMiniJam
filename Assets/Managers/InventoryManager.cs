using Assets.Types;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Managers
{
    public static class InventoryManager
    {
        private static int size = 0;
        private static List<InventoryItem> inventory = new List<InventoryItem>();

        public static List<InventoryItem> GetItems()
        {
            return inventory;
        }

        public static int GetSize()
        {
            return size;
        }

        public static List<Tuple<ItemEnum, int>> SetSize(int s)
        {
            if (s >= size)
            {
                size = s;
                return new List<Tuple<ItemEnum, int>>();
            }
            return new List<Tuple<ItemEnum, int>>();
        }

        public static bool CanAddItem(ItemEnum item)
        {
            ItemData data = ItemManager.GetItemData(item);
            return inventory.Count < size || inventory.Where(t => (t.id == item && t.number < data.stack)).Count() > 0;
        }

        public static bool TryAddItem(ItemEnum item)
        {
            ItemData data = ItemManager.GetItemData(item);
            var slots = inventory.Where(t => (t.id == item && t.number < data.stack));
            if (slots.Count() > 0)
            {
                var idx = inventory.IndexOf(slots.First());
                inventory[idx].number += 1;
                return true;
            }

            if (inventory.Count < size)
            {
                inventory.Add(new InventoryItem() { id = item, number = 0, position = Enumerable.Range(0, size).Where(i => !inventory.Select(t => t.position).Contains(i)).Min() });
            }

            return false;
        }
    }
}