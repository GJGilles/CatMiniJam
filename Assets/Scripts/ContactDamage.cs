using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class ContactDamage : MonoBehaviour
    {
        public int damage = 1;
        public Rigidbody2D rb;

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

        public bool IsHit(GameObject obj)
        {
            var col = GetComponent<Collider2D>();
            if (rb != null)
            {
                Vector2 pos = transform.position;
                var cast = Physics2D.BoxCast(pos - (rb.velocity * Time.deltaTime), col.bounds.size, 0, rb.velocity.normalized, rb.velocity.magnitude * Time.deltaTime * 2);
                return cast.collider != null && cast.collider.gameObject == obj;
            }
            else
                return true;
        }

        public void DealDamage(GameObject obj)
        {
            if (!IsHit(obj))
                return;

            HealthController hc;
            if (obj.TryGetComponent(out hc))
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