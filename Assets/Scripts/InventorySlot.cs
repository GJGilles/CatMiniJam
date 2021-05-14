using System.Collections;
using UnityEngine;
using Assets.Types;
using Assets.Managers;

namespace Assets.Scripts
{
    public class InventorySlot : MonoBehaviour
    {
        public InventoryItem item = new InventoryItem();
        public GameObject slotItem;

        public void Start()
        {
            var data = ItemManager.GetItemData(item.id);
            if (data != null && data.sprite != null)
            {
                var inst = Instantiate(slotItem, transform);
                var img = inst.GetComponent<UnityEngine.UI.Image>();
                img.sprite = data.sprite;

                inst.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = item.number.ToString();
            }
        }
    }
}