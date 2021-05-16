using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        private enum PlayerAnimEnum
        {
            IsWalk,
            IsJump,
            IsThrow,
            IsHurt,
            IsDead,
            IsDied
        }

        private Animator Animator() { return GetComponent<Animator>();  }

        public void SetWalk(bool walking)
        {
            Animator().SetBool(PlayerAnimEnum.IsWalk.ToString(), walking);
        }

        public void SetJump(bool jumping)
        {
            Animator().SetBool(PlayerAnimEnum.IsJump.ToString(), jumping);
        }

        public void SetThrow()
        {
            Animator().SetTrigger(PlayerAnimEnum.IsThrow.ToString());
        }

        public void SetHurt()
        {
            Animator().SetTrigger(PlayerAnimEnum.IsHurt.ToString());
        }

        public void SetDead()
        {
            Animator().SetTrigger(PlayerAnimEnum.IsDead.ToString());
        }

        public void SetDied()
        {
            Animator().SetBool(PlayerAnimEnum.IsDied.ToString(), true);
        }
    }
}