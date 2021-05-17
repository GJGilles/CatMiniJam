using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class HealthController : MonoBehaviour
    {
        public int maxHealth = 3;
        public float iTime = 0.5f;

        private int currHealth;
        private float iRemain = 0f;

        public UnityEvent OnDamage;
        public UnityEvent InvincibleStart;
        public UnityEvent InvincibleEnd;
        public UnityEvent OnDeath;

        public void Start()
        {
            currHealth = maxHealth;

            if (OnDamage == null) OnDamage = new UnityEvent();
            if (InvincibleStart == null) InvincibleStart = new UnityEvent();
            if (InvincibleEnd == null) InvincibleEnd = new UnityEvent();
            if (OnDeath == null) OnDeath = new UnityEvent();
        }

        public void Update()
        {
            if (iRemain > 0)
            {
                iRemain -= Time.deltaTime;
                if (iRemain <= 0)
                {
                    InvincibleEnd.Invoke();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (iRemain <= 0)
            {
                currHealth -= damage;
                if (currHealth == 0)
                {
                    OnDeath.Invoke();
                }
                else if (currHealth > 0)
                {
                    iRemain = iTime;
                    OnDamage.Invoke();
                    InvincibleStart.Invoke();
                }
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}