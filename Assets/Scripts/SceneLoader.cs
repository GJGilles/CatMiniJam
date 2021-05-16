using System.Collections;
using UnityEngine;
using Assets.Managers;

namespace Assets.Scripts
{
    public class SceneLoader: MonoBehaviour
    {
        public enum SceneEnum
        {
            None = -1,
            Level1,
            Level2,
        }

        public float entryTime = 0.5f;
        public float exitTime = 0.5f;

        private string next;
        private float currentTime = 0f;

        private int inputLevel = 1;

        public void Start()
        {
            Time.timeScale = 1;
            InputManager.UnblockKeys(0);
        }

        public void Update()
        {
            if (InputManager.GetKey(KeyCode.R, inputLevel))
            {
                string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                LoadScene(scene);
            }

            if (next != null)
            {
                if (currentTime >= exitTime)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(next.ToString());
                }
                currentTime += Time.unscaledDeltaTime;
                var rect = GetComponent<RectTransform>();
                rect.anchoredPosition = rect.rect.width * new Vector2(1 - currentTime / exitTime, 0);
            } 
            else if (currentTime < entryTime)
            {
                currentTime += Time.unscaledDeltaTime;
                var rect = GetComponent<RectTransform>();
                rect.anchoredPosition = rect.rect.width * new Vector2(-currentTime / entryTime, 0);
            }
        }

        private void LoadScene(string scene)
        {
            if (next == null)
            {
                next = scene;
                currentTime = 0;
            }
        }

        #region Scene Loaders

        public void LoadSceneLevel1() { LoadScene(SceneEnum.Level1.ToString()); }
        public void LoadSceneLevel2() { LoadScene(SceneEnum.Level2.ToString()); }

        #endregion

    }
}