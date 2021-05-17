using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class HealthUI : MonoBehaviour
    {
        public HealthController hc;
        public GameObject fullHeart;
        public GameObject emptyHeart;
        public Vector2 offset;

        private int currentHealth;

        public void Start()
        {
            currentHealth = hc.maxHealth;
            for (int i = 0; i < currentHealth; i++)
            {
                Instantiate(fullHeart, transform).GetComponent<RectTransform>().anchoredPosition = offset + new Vector2(100 * i, 0);
            }
        }

        public void TakeDamage()
        {
            currentHealth--;
            Destroy(transform.GetChild(currentHealth + 1).gameObject);
            Instantiate(emptyHeart, transform).GetComponent<RectTransform>().anchoredPosition = offset + new Vector2(100 * currentHealth, 0);

            if (currentHealth == 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }
}