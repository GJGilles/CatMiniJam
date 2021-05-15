using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float stopTime = 0.5f;
        public bool isFacing = true;

        private float stopped = 0f;


        public void Turn()
        {
            stopped = stopTime;
            GetComponent<Rigidbody2D>().velocity = new Vector2();

            isFacing = !isFacing;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
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
            }
        }
    }
}
