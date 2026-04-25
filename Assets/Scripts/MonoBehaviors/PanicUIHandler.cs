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
        }
    }
}
