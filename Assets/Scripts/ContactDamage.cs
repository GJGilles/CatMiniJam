using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class ContactDamage : MonoBehaviour
    {
        public int damage = 1;
        public LayerMask damageMask;

        public UnityEvent OnDamage;
        public UnityEvent OnHit;

        public void Start()
        {
            if (OnDamage == null) OnDamage = new UnityEvent();
            if (OnHit == null) OnHit = new UnityEvent();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            DealDamage(collision.gameObject);
        }

        public bool InMask(GameObject obj)
        {
            return damageMask == (damageMask | (1 << obj.layer));
        }

        public void DealDamage(GameObject obj)
        {
            HealthController hc;
            if (obj.TryGetComponent(out hc) && InMask(obj))
            {
                hc.TakeDamage(damage);
                OnDamage.Invoke();
            }
            else
            {
                OnHit.Invoke();
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}