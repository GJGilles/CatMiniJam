using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SceneManager: MonoBehaviour
    {
        public enum SceneEnum
        {
            None = -1,
            Game,
        }

        public float speed = 0.5f;

        private SceneEnum next = SceneEnum.None;
        private float opacity = 1;


        public void Update()
        {
            if (next != SceneEnum.None)
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

        private void LoadScene(SceneEnum scene)
        {
            if (next == SceneEnum.None)
            {
                next = scene;
            }
        }

        #region Scene Loaders

        public void LoadSceneGame() { LoadScene(SceneEnum.Game); }

        #endregion

    }
}