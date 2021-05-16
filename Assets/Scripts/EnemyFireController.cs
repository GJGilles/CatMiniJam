using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class EnemyFireController : MonoBehaviour
    {
        public float projectileSpeed = 10f;
        public float projectileInterval = 0.2f;
        public GameObject projectileObj;
        public bool isLeft = true;

        public UnityEvent OnThrow;

        private float currTime = 0f;
        private bool isDead = false;

        public void Start()
        {
            if (OnThrow == null) OnThrow = new UnityEvent();
        }

        public void Update()
        {
            if (isDead)
                return;

            currTime += Time.deltaTime;
            if (currTime >= projectileInterval)
            {
                currTime = 0;

                var inst = Instantiate(projectileObj);
                inst.transform.position = transform.position - new Vector3(0, 0.25f);
                inst.GetComponent<Rigidbody2D>().velocity = (isLeft ? -1 : 1) * new Vector2(projectileSpeed, 0);
                OnThrow.Invoke();
            }
        }

        public void Die()
        {
            isDead = true;
        }
    }
}