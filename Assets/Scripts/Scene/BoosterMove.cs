using UnityEngine;

namespace Shooter.Scene
{
    public class BoosterMove : MonoBehaviour
    {
        public readonly Buff _moveBuff = new MoveBuff();

        public void Apply(PlayerBuffs playerBuff)
        {
            playerBuff.AddBuff(_moveBuff);
        }
    }
}