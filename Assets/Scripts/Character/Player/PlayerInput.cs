using UnityEngine;

namespace Shooter
{
    /// <summary>
    /// Script that handle input
    /// </summary>
    public sealed class PlayerInput : MonoBehaviour
    {
        public void Initialize(Character character)
        {
            _character = character;
            _floorMask = LayerMask.GetMask("Floor");
        }

        public void OnUpdate(ref Vector3 movement, out Quaternion rotation, out bool isShoot)
        {
            Move(ref movement);
            Rotate(out rotation);

            isShoot = Input.GetButton("Fire1");
        }

        private void Rotate(out Quaternion rotation)
        {
            Ray _camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_camRay, out RaycastHit _floorHit, CAM_RAY_LENGTH, _floorMask))
            {
                Vector3 _playerToMouse = _floorHit.point - transform.position; //delta
                _playerToMouse.y = 0f;

                rotation = Quaternion.LookRotation(_playerToMouse);
                _lastAvailableRotation = rotation;
            }
            else //if not hit
            {
                rotation = _lastAvailableRotation;
            }
        }

        private void Move(ref Vector3 movement)
        {
            movement.Set(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            movement = movement.normalized * Time.deltaTime;
        }

#pragma warning disable IDE0052 // Remove unread private members
        private Character _character;
#pragma warning restore IDE0052 // Remove unread private members

        private int _floorMask;
        private const float CAM_RAY_LENGTH = 100f;

        private Quaternion _lastAvailableRotation; //possible null?
    }
}