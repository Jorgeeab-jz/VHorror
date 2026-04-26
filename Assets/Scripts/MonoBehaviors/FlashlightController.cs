using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace VHorror.Scripts.MonoBehaviors
{
    public class FlashlightController : MonoBehaviour
    {
        [SerializeField] private Light flashlight;
        [SerializeField] private InputActionReference toggleFlashlightAction;

        private IPanicManager _panicManager;

        [Inject]
        public void Construct(IPanicManager panicManager)
        {
            _panicManager = panicManager;
        }

        private void Start()
        {
            UpdateFlashlight();
        }

        private void OnEnable()
        {

            _panicManager.OnStateChanged += UpdateFlashlight;

            toggleFlashlightAction.action.performed += OnFlashlighToggle;
            toggleFlashlightAction.action.Enable();
        }

        private void OnDisable()
        {
            _panicManager.OnStateChanged -= UpdateFlashlight;


            toggleFlashlightAction.action.performed -= OnFlashlighToggle;
            toggleFlashlightAction.action.Disable();
        }

        private void UpdateFlashlight()
        {
            flashlight.enabled = _panicManager.IsFlashlightOn;
        }

        public void OnPickBattery(float amount)
        {
            _panicManager.AddBattery(amount);
            Debug.Log($"Battery collected! Refilled {amount}");
        }

        private void OnFlashlighToggle(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _panicManager.ToggleFlashlight();
            }
        }

    }
}
