using Shooter.Scene;
using System;
using UnityEngine;

namespace Shooter
{
    public class PlayerPhysics : MonoBehaviour
    {
        public void Initialize(Character character)
        {
            _character = character;
        }

        public virtual void OnUpdate(Vector3 movement, Quaternion rotation)
        {
            characterRigidbody.MovePosition(transform.position + (movement * _character.MovementSpeed.GetValue()));
            characterRigidbody.MoveRotation(rotation.normalized);
        }

        [SerializeField] private Rigidbody characterRigidbody = default;

#pragma warning disable IDE0052 // Remove unread private members
        private Character _character;
#pragma warning restore IDE0052 // Remove unread private members
    }
}