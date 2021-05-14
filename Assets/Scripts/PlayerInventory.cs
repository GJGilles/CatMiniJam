using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Types;
using Assets.Managers;

namespace Assets.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public float radiusSuction = 3f;
        public float radiusPickup = 1f;
        public float forceSuction = 1f;
        public float forceDamping = 10f;
        public LayerMask itemMask;
        public int itemSlots = 8;
        public GameObject screenObj;
        public GameObject inventoryObj;

        private int inputLevel = 0;

        private Collider2D Collider() { return GetComponent<Collider2D>(); }

        private void Start()
        {
            InventoryManager.SetSize(itemSlots);
        }

        private void Update()
        {
            if (InputManager.GetKeyDown(KeyCode.I, inputLevel))
            {
                Instantiate(inventoryObj, screenObj.transform);
            }
        }

        private void FixedUpdate()
        {
            Collider2D[] pickups = Physics2D.OverlapCircleAll(Collider().bounds.center, radiusPickup, itemMask);
            foreach (Collider2D item in pickups)
            {
                ItemController holder;
                if (item.gameObject.TryGetComponent(out holder))
                {
                    if (InventoryManager.TryAddItem(holder.item))
                        Destroy(item.gameObject);
                }
            }

            Collider2D[] suctions = Physics2D.OverlapCircleAll(Collider().bounds.center, radiusSuction, itemMask);
            foreach (Collider2D item in suctions)
            {
                ItemController holder;
                if (item.gameObject.TryGetComponent(out holder))
                {
                    if (!InventoryManager.CanAddItem(holder.item))
                        continue;

                    Rigidbody2D rb = item.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 force = forceSuction * (transform.position - item.gameObject.transform.position).normalized;
                    rb.velocity = Vector2.Lerp(rb.velocity, force, forceDamping * Time.deltaTime);
                }
            }
        }
    }
}
