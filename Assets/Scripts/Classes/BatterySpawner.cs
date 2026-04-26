using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VHorror.Scripts.MonoBehaviors;

public class BatterySpawner : IBatterySpawner, IStartable
{
    private readonly IObjectResolver _resolver;
    private readonly Battery _batteryPrefab;
    private readonly Transform[] _spawnPositions;

    private List<GameObject> _spawnedBatteries = new List<GameObject>();

    public BatterySpawner(
        IObjectResolver resolver,
        Battery batteryPrefab,
        Transform[] spawnPositions)
    {
        _resolver = resolver;
        _batteryPrefab = batteryPrefab;
        _spawnPositions = spawnPositions;
    }

    public void Start()
    {
        SpawnBatteries();
    }

    public void Respawn()
    {
        ClearBatteries();
        SpawnBatteries();
    }

    private void ClearBatteries()
    {
        foreach (var battery in _spawnedBatteries)
        {
            if (battery != null)
            {
                Object.Destroy(battery);
            }
        }
        _spawnedBatteries.Clear();
    }

    private void SpawnBatteries()
    {
        if (_batteryPrefab == null || _spawnPositions == null) return;

        Debug.Log($"BatterySpawner: Spawning {_spawnPositions.Length} batteries.");

        foreach (var spawnPoint in _spawnPositions)
        {
            if (spawnPoint == null) continue;

            var battery = _resolver.Instantiate(_batteryPrefab, spawnPoint.position, spawnPoint.rotation);
            _spawnedBatteries.Add(battery.gameObject);
        }
    }
}
