using System;
using VContainer.Unity;
using UnityEngine;

public class PanicManager : IPanicManager, ITickable
{
    private const float BatteryDrainRate = 0.05f; 
    private const float PanicIncreaseRate = 0.02f; 
    private const float PanicDecreaseRate = 0.05f; 

    public float BatteryPercent { get; private set; } = 1f;
    public float PanicPercent { get; private set; } = 0f;
    public bool IsFlashlightOn { get; private set; } = false;

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
        BatteryPercent = Mathf.Clamp01(BatteryPercent + amount);
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
        
        OnStateChanged?.Invoke();
    }
}
