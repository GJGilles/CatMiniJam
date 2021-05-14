using System.Collections;
using UnityEngine;
using Assets.Types;
using Assets.Managers;

namespace Assets.Scripts
{
    public class ItemController : MonoBehaviour
    {
        public ItemEnum item = ItemEnum.Default;

        private void Start()
        {
            var data = ItemManager.GetItemData(item);
            GetComponent<SpriteRenderer>().sprite = data.sprite;
            GetComponent<BoxCollider2D>().size = data.sprite.bounds.size;
        }
    }
}