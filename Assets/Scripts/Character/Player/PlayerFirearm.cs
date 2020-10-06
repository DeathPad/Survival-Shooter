using UnityEngine;

namespace Shooter
{
    public sealed class PlayerFirearm : MonoBehaviour
    {
        public void Initialize(Player player)
        {
            _player = player;
            _shootableMask = LayerMask.GetMask("Enemies");
        }

        public void OnUpdate(bool isShoot)
        {
            _timer += Time.deltaTime;
            if (isShoot &&
                _timer >= _player.AttackInterval) //If interval passed
            {
                _timer = 0f;

                Shoot();

                CancelInvoke("DisableEffects"); //Cancel prev disableeffect and call the new one
                Invoke("DisableEffects", EFFECT_DISPLAY_TIME);
            }
        }

#pragma warning disable IDE0051 // Remove unused private members
        private void DisableEffects()
#pragma warning restore IDE0051 // Remove unused private members
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }

        private void Shoot() //gun visual component?
        {
            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            _shootRay.origin = transform.position;
            _shootRay.direction = transform.forward;

            if (Physics.Raycast(_shootRay, out _shootHit, RANGE, _shootableMask))
            {
                Enemy _enemy = _shootHit.collider.GetComponent<Enemy>();
                _enemy.TakeDamage(_player.AttackDamage, _shootHit.point);

                gunLine.SetPosition(1, _shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * RANGE);
            }
        }

        [SerializeField] private ParticleSystem gunParticles = default;
        [SerializeField] private LineRenderer gunLine = default;
        [SerializeField] private Light gunLight = default;

        private Player _player;

        private const float RANGE = 100f;
        private const float EFFECT_DISPLAY_TIME = 0.2f;
        private int _shootableMask;
        private float _timer;
        private Ray _shootRay;
        private RaycastHit _shootHit;
    }
}