using UnityEngine;

namespace VHorror.Scripts.MonoBehaviors
{
    public class PlayerResetHandler : MonoBehaviour
    {
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private CharacterController _characterController;

        private void Awake()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _characterController = GetComponent<CharacterController>();
        }

        public void ResetPosition()
        {
            // If using CharacterController, we must disable it before teleporting
            if (_characterController != null)
            {
                _characterController.enabled = false;
            }

            transform.position = _initialPosition;
            transform.rotation = _initialRotation;

            if (_characterController != null)
            {
                _characterController.enabled = true;
            }

            Debug.Log("Player position reset.");
        }
    }
}
