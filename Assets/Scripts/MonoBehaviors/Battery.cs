using UnityEngine;
using VContainer;
using VHorror.Scripts.MonoBehaviors;

namespace VHorror.Scripts.MonoBehaviors
{
    public class Battery : MonoBehaviour
    {
        [SerializeField] private float refillAmount = 0.2f;

        private void OnTriggerEnter(Collider other)
        {
            var flashlightController = other.GetComponent<FlashlightController>();

            flashlightController?.OnPickBattery(refillAmount);

            Destroy(gameObject);
        }
    }
}
