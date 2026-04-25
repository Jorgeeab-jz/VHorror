using UnityEngine;
using VContainer;
using Microlight.MicroBar;

namespace VHorror.Scripts.MonoBehaviors
{
    public class PanicUIHandler : MonoBehaviour
    {
        [SerializeField] private MicroBar batteryBar;
        [SerializeField] private MicroBar panicBar;

        private IPanicManager _panicManager;

        [Inject]
        public void Construct(IPanicManager panicManager)
        {
            _panicManager = panicManager;
        }

        private void Start()
        {
            // Initialize bars with 1.0 as max value (0-1 range)
            if (batteryBar != null) batteryBar.Initialize(1f);
            if (panicBar != null) panicBar.Initialize(1f);
            
            UpdateUI();
        }

        private void OnEnable()
        {
            if (_panicManager != null)
            {
                _panicManager.OnStateChanged += UpdateUI;
            }
        }

        private void OnDisable()
        {
            if (_panicManager != null)
            {
                _panicManager.OnStateChanged -= UpdateUI;
            }
        }

        private void UpdateUI()
        {
            if (_panicManager == null) return;

            if (batteryBar != null)
            {
                batteryBar.UpdateBar(_panicManager.BatteryPercent);
            }
            
            if (panicBar != null)
            {
                panicBar.UpdateBar(_panicManager.PanicPercent);
            }

            Debug.Log($"Panic UI Updated - Battery: {_panicManager.BatteryPercent}%, Panic: {_panicManager.PanicPercent}");
        }
    }
}
