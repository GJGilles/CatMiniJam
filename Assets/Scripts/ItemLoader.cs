using Assets.Types;
using Assets.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class ItemLoader : MonoBehaviour
    {
        public List<ItemData> items = new List<ItemData>();
        public UnityEvent loaded;

        public void Start()
        {
            ItemManager.LoadItems(items);
            if (loaded != null)
            {
                loaded.Invoke();
            }
        }
    }
}