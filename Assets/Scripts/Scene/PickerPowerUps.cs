using Shooter.Scene;
using UnityEngine;

namespace Shooter
{
    public class PickerPowerUps : MonoBehaviour
    {
        public void Initialize(Player player)
        {
            _player = player;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boosters"))
            {
                other.GetComponent<BoosterMove>().Apply(playerBuffs);
                Destroy(other.gameObject);

                return;
            }

            if (other.CompareTag("Regen"))
            {
                other.GetComponent<RegenPotion>().Regen(_player);
                Destroy(other.gameObject);

                return;
            }
        }

        [SerializeField] private PlayerBuffs playerBuffs = default;

        private Player _player;
    }
}