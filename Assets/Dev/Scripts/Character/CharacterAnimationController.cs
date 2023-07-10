using UnityEngine;

namespace Dev.Scripts
{
    public interface ICharacterAnimationController
    {
        void PlayAnimation(Vector3 moveDirection);
    }

    public class CharacterAnimationController : MonoBehaviour, ICharacterAnimationController
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayAnimation(Vector3 moveDirection)
        {
            
        }
    }
}
