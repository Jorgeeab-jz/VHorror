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
            PickUp();
        }

        private void PickUp()
        {
            Debug.Log("Totem picked up!");
            _inventoryManager?.AddTotem();
            Destroy(gameObject);
        }
    }
}
