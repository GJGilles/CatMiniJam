using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {

        public float wait = 1f;
        private float current = 0f;

        public UnityEvent OnDone;

        private void Start()
        {
            if (OnDone == null) OnDone = new UnityEvent();
        }

        private void Update()
        {
            current += Time.deltaTime;
            if (current >= wait)
            {
                OnDone.Invoke();
                Destroy(this);
            }
        }
    }
}