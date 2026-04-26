using System;
using VContainer.Unity;
using UnityEngine;
using VContainer;

public class PanicManager : IPanicManager, ITickable, IStartable
{
    private const float BatteryDrainRate = 0.05f; 
    private const float PanicIncreaseRate = 0.02f; 
    private const float PanicDecreaseRate = 0.05f; 

    public float BatteryPercent { get; private set; } = 1f;
    public float PanicPercent { get; private set; } = 0f;
    public bool IsFlashlightOn { get; private set; } = false;
    
    private readonly IGameStateManager _gameStateManager;

    [Inject]
    public PanicManager(IGameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }

    public event Action OnStateChanged;

    public void SetFlashlightActive(bool active)
    {
        if (active && BatteryPercent <= 0) return;
        
        if (IsFlashlightOn != active)
        {
            IsFlashlightOn = active;
            OnStateChanged?.Invoke();
        }
    }

    public void ToggleFlashlight()
    {
        SetFlashlightActive(!IsFlashlightOn);
    }

    public void AddBattery(float amount)
    {
        BatteryPercent += amount;

        if(BatteryPercent >= 1f)
        {
            BatteryPercent = 1f;
        }

        Debug.Log($"Battery added! Current Battery: {BatteryPercent}");

        OnStateChanged?.Invoke();
    }

    public void Tick()
    {
        if (IsFlashlightOn)
        {
            BatteryPercent = Mathf.Max(0, BatteryPercent - BatteryDrainRate * Time.deltaTime);
            if (BatteryPercent <= 0)
            {
                SetFlashlightActive(false);
            }
            
            // Panic decreases when light is on
            PanicPercent = Mathf.Max(0, PanicPercent - PanicDecreaseRate * Time.deltaTime);
        }
        else
        {
            // Panic increases when light is off
            PanicPercent = Mathf.Min(1f, PanicPercent + PanicIncreaseRate * Time.deltaTime);
        }

        if (PanicPercent >= 1.0f && _gameStateManager.CurrentState == GameState.Playing)
        {
            Debug.Log("Panic reached 100%! Player killed.");
            _gameStateManager.ChangeState(GameState.PlayerKilled);
        }

        OnStateChanged?.Invoke();
    }

    public void Start()
    {
        Debug.Log("PanicManager started. Initial Battery: " + BatteryPercent + ", Initial Panic: " + PanicPercent);
    }
}
