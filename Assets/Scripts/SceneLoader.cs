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
            Game,
        }

        public float speed = 0.5f;

        private string next;
        private float opacity = 1;

        private int inputLevel = 1;

        public void Start()
        {
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
                if (opacity >= 1)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(next.ToString());
                }
                opacity += Time.deltaTime * speed;
            } 
            else if (opacity > 0)
            {
                opacity -= Time.deltaTime * speed;

            }

            GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, opacity);
        }

        private void LoadScene(string scene)
        {
            if (next == null)
            {
                next = scene;
            }
        }

        #region Scene Loaders

        public void LoadSceneGame() { LoadScene(SceneEnum.Game.ToString()); }

        #endregion

    }
}