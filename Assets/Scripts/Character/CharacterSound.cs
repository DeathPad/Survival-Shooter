using UnityEngine;

namespace Shooter
{
    public class CharacterSound : MonoBehaviour
    {
        public void Initialize(Character character)
        {
            _character = character;
        }

        public void OnHurt()
        {
            hurtSource.Play();
        }

        public void OnDeath()
        {
            deathSource.Play();
        }

        /*
         * Single audio source probs:
         * - Cant play hurt and shoot sound at the same time
         * - What if Hurt sound have different properties with shoot sound?(ex. volume)
         */
        [SerializeField] private AudioSource hurtSource = default;
        [SerializeField] private AudioSource deathSource = default;

#pragma warning disable IDE0052 // Remove unread private members
        private Character _character;
#pragma warning restore IDE0052 // Remove unread private members
    }
}