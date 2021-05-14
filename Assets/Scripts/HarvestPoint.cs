using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Managers;
using Assets.Types;

namespace Assets.Scripts
{
    public class HarvestPoint : MonoBehaviour
    {
        public float harvestTime = 2f;
        public ItemEnum item = ItemEnum.Default;
        public GameObject itemObj;

        private float currentTime = 0f;

        private int inputLevel = 1;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void Update()
        {
            if (transform.GetChild(0).gameObject.activeSelf && InputManager.GetKey(KeyCode.F, inputLevel))
            {
                currentTime += Time.deltaTime;
                if (currentTime >= harvestTime)
                {
                    var inst = Instantiate(itemObj, transform.position + new Vector3(0, 2, 0), new Quaternion());
                    inst.GetComponent<ItemController>().item = item;
                    currentTime = 0f;
                }
            }
        }
    }
}
