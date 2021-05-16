using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class SceneExit : MonoBehaviour
    {
        public GameObject player;
        public UnityEvent PlayerEnter;

        public void Start()
        {
            if (PlayerEnter == null) PlayerEnter = new UnityEvent();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                Time.timeScale = 0;
                PlayerEnter.Invoke();
            }
        }
    }
}