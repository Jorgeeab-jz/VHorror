using UnityEngine;
using TMPro;
using DG.Tweening;
using VContainer;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace VHorror.Scripts.MonoBehaviors
{
    public class GameStatusUIHandler : MonoBehaviour
    {
        [Header("Messages")]
        [SerializeField] private string initialObjectiveMessage = "Collect 3 totems to escape";
        [SerializeField] private string playerKilledMessage = "Panic Overwhelmed You!";
        [SerializeField] private string escapedMessage = "You Escaped the Horror!";

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private TextMeshProUGUI currentTotems;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI restartHintText;

        [Header("Input")]
        [SerializeField] private InputActionReference resetAction;

        [Header("Settings")]
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float initialDisplayDuration = 5f;

        private IGameStateManager _gameStateManager;
        private IPanicManager _panicManager;
        private IInventoryManager _inventoryManager;
        private ITotemSpawner _totemSpawner;
        private IBatterySpawner _batterySpawner;
        private PlayerResetHandler _playerResetHandler;

        [Inject]
        public void Construct(
            IGameStateManager gameStateManager,
            IPanicManager panicManager,
            IInventoryManager inventoryManager,
            ITotemSpawner totemSpawner,
            IBatterySpawner batterySpawner,
            PlayerResetHandler playerResetHandler)
        {
            _gameStateManager = gameStateManager;
            _panicManager = panicManager;
            _inventoryManager = inventoryManager;
            _totemSpawner = totemSpawner;
            _batterySpawner = batterySpawner;
            _playerResetHandler = playerResetHandler;
        }

        private void Start()
        {
            if (restartHintText != null) restartHintText.gameObject.SetActive(false);
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Show initial objective
            ShowMessage(initialObjectiveMessage, initialDisplayDuration);
        }

        private void ResetGame()
        {
            Debug.Log("Resetting Game via Input...");

            // Reset Managers and Spawners
            _panicManager.Reset();
            _inventoryManager.Reset();
            _totemSpawner.Respawn();
            _batterySpawner.Respawn();
            
            // Reset Player
            if (_playerResetHandler != null)
            {
                _playerResetHandler.ResetPosition();
            }

            // Change state back to playing (this will resume time)
            _gameStateManager.ChangeState(GameState.Playing);

            // Hide restart hint
            if (restartHintText != null) restartHintText.gameObject.SetActive(false);

            // Re-initialize UI
            InitializeGame();
        }

        private void OnEnable()
        {
            if (_gameStateManager != null)
                _gameStateManager.OnGameStateChanged += HandleGameStateChanged;

            if (_inventoryManager != null)
                _inventoryManager.OnTotemsCountChanged += UpdateCurrentTotems;

            if (resetAction != null)
                resetAction.action.performed += OnResetAction;
        }

        private void OnDisable()
        {
            if (_gameStateManager != null)
                _gameStateManager.OnGameStateChanged -= HandleGameStateChanged;

            if (_inventoryManager != null)
                _inventoryManager.OnTotemsCountChanged -= UpdateCurrentTotems;

            if (resetAction != null)
                resetAction.action.performed -= OnResetAction;
        }

        private void OnResetAction(InputAction.CallbackContext context)
        {
            // Only allow reset if the game is over
            if (_gameStateManager.CurrentState == GameState.PlayerKilled || 
                _gameStateManager.CurrentState == GameState.Escaped)
            {
                ResetGame();
            }
        }

        private void HandleGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.PlayerKilled:
                    ShowMessage(playerKilledMessage, -1f);
                    if (restartHintText != null) restartHintText.gameObject.SetActive(true);
                    break;
                case GameState.Escaped:
                    ShowMessage(escapedMessage, -1f);
                    if (restartHintText != null) restartHintText.gameObject.SetActive(true);
                    break;
                case GameState.Playing:
                    if (restartHintText != null) restartHintText.gameObject.SetActive(false);
                    break;
            }
        }

        private void UpdateCurrentTotems(int total)
        {
            if (currentTotems != null) currentTotems.text = $"{total}";
        }

        private void ShowMessage(string message, float duration)
        {
            if (canvasGroup == null || statusText == null) return;

            // Stop any existing tweens
            canvasGroup.DOKill();

            statusText.text = message;
            canvasGroup.alpha = 0f;

            canvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                if (duration > 0)
                {
                    canvasGroup.DOFade(0f, fadeDuration).SetDelay(duration);
                }
            });
        }
    }
}
