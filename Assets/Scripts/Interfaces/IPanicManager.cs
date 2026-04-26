using System;

public interface IPanicManager
{
    float BatteryPercent { get; }
    float PanicPercent { get; }
    bool IsFlashlightOn { get; }
    
    event Action OnStateChanged;
    
    void SetFlashlightActive(bool active);
    void ToggleFlashlight();
    void AddBattery(float amount);
    void Reset();
}
