using UnityEngine;

namespace Shooter.Scene
{
    public class RegenPotion : MonoBehaviour
    {
        public void Regen(Player player)
        {
            player.Regenerate(amount);
        }

        [SerializeField] private int amount = 15;
    }
}