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
            batteryBar.Initialize(1f);
            panicBar.Initialize(1f);

            UpdateUI();
        }

        private void OnEnable()
        {
            _panicManager.OnStateChanged += UpdateUI;
        }

        private void OnDisable()
        {
            _panicManager.OnStateChanged -= UpdateUI;
        }

        private void UpdateUI()
        {

            batteryBar.UpdateBar(_panicManager.BatteryPercent);


            panicBar.UpdateBar(_panicManager.PanicPercent);

        }
    }
}
