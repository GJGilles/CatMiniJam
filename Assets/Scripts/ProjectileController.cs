using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectileController : MonoBehaviour
    {
        public float destroyTime = 2f;
        private float currTime = 0f;
        
        public void Update()
        {
            currTime += Time.deltaTime;
            if (currTime >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }
}