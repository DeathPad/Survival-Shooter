using UnityEngine;

namespace Shooter
{
    public sealed class PlayerSound : CharacterSound
    {
        public void OnUpdate(bool isShoot)
        {
            if (isShoot)
            {
                gunSource.Play();
            }
        }

        [SerializeField] private AudioSource gunSource = default;
    }
}