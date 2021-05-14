using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Managers
{
    public static class InputManager
    {
        private static int keyBlockLevel = 0;

        public static bool GetKey(KeyCode key, int level)
        {
            if (level >= keyBlockLevel)
            {
                return Input.GetKey(key);
            }
            else
                return false;
        }

        public static bool GetKeyDown(KeyCode key, int level)
        {
            if (level >= keyBlockLevel)
            {
                return Input.GetKeyDown(key);
            }
            else
                return false;
        }

        public static float GetHorzAxis(int level)
        {
            if (level >= keyBlockLevel)
            {
                return Input.GetAxisRaw("Horizontal");
            }
            else
                return 0f;
        }

        public static bool BlockKeys(int level)
        {
            if (level < keyBlockLevel)
                return false;

            keyBlockLevel = level;
            return true;
        }

        public static bool UnblockKeys(int level)
        {
            if (level != keyBlockLevel)
                return false;

            keyBlockLevel--;
            return true;
        }
    }
}
