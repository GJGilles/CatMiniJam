using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class EnemyMoveController : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float stopTime = 0.5f;

        private bool isFacing = false;
        private float stopped = 0f;

        public UnityEvent OnWalkStart;
        public UnityEvent OnWalkEnd;

        public void Start()
        {
            if (OnWalkStart == null) OnWalkStart = new UnityEvent();
            if (OnWalkEnd == null) OnWalkEnd = new UnityEvent();
        }

        public void Turn()
        {
            stopped = stopTime;
            OnWalkEnd.Invoke();
            GetComponent<Rigidbody2D>().velocity = new Vector2();

            isFacing = !isFacing;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void BounceBack()
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(isFacing ? -moveSpeed : moveSpeed, rb.velocity.y + 3);
            stopped = stopTime;
        }

        public void Update()
        {
            if (stopped > 0)
            {
                stopped -= Time.deltaTime;
            }
            else
            {
                var rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(isFacing ? moveSpeed : -moveSpeed, rb.velocity.y);
                OnWalkStart.Invoke();
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
