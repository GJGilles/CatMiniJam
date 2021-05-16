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

        public UnityEvent OnThrow;

        private float currTime = 0f;

        public void Start()
        {
            if (OnThrow == null) OnThrow = new UnityEvent();
        }

        public void Update()
        {
            currTime += Time.deltaTime;
            if (currTime >= projectileInterval)
            {
                currTime = 0;

                var inst = Instantiate(projectileObj);
                inst.transform.position = transform.position - new Vector3(0, 0.25f);
                inst.transform.rotation = transform.rotation;
                inst.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
            }
        }
    }
}