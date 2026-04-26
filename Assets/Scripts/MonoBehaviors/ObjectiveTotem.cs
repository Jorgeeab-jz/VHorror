using UnityEngine;
using VContainer;

namespace VHorror.Scripts.MonoBehaviors
{
    public class ObjectiveTotem : MonoBehaviour
    {
        private IInventoryManager _inventoryManager;

        [Inject]
        public void Construct(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collider belongs to the player (proxy: FlashlightController)
            if (other.GetComponent<FlashlightController>() != null)
            {
                PickUp();
            }
        }

        private void PickUp()
        {
            Debug.Log("Totem picked up!");
            _inventoryManager?.AddTotem();
            Destroy(gameObject);
        }
    }
}
