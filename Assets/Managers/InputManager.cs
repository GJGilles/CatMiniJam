using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Managers
{
    public static class InputManager
    {
        private static int keyBlockLevel = 0;

        public static float GetHorzAxis(int level)
        {
            if (level >= keyBlockLevel)
            {
                return Input.GetAxisRaw("Horizontal");
            }
            else
                return 0f;
        }

        public static bool GetJump(int level)
        {
            if (level >= keyBlockLevel)
            {
                return Input.GetAxisRaw("Jump") != 0f;
            }
            else
                return false;
        }

        public static bool GetFire(int level)
        {
            if (level >= keyBlockLevel)
            {
                return Input.GetAxisRaw("Fire1") != 0f;
            }
            else
                return false;
        }

        public static bool GetRetry(int level)
        {
            if (level >= keyBlockLevel)
            {
                return Input.GetAxisRaw("Fire2") != 0f;
            }
            else
                return false;
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
            keyBlockLevel = level;
            return true;
        }
    }
}
