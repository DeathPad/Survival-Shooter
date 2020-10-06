using System.Collections;
using UnityEngine;

namespace Shooter
{
    public class PlayerBuffs : MonoBehaviour
    {
        public void Initialize(Player player)
        {
            _player = player;
        }

        public void AddBuff(Buff buff)
        {
            StartCoroutine(BuffCoroutine(buff));
        }

        private IEnumerator BuffCoroutine(Buff buff)
        {
            buff.OnApply(_player);
            yield return new WaitForSeconds(buff.Duration);

            buff.UnApply(_player);
        }

        private Player _player;
    }
}