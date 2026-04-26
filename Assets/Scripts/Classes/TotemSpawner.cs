using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VHorror.Scripts.MonoBehaviors;

public class TotemSpawner : IStartable
{
    private readonly IObjectResolver _resolver;
    private readonly ObjectiveTotem _totemPrefab;
    private readonly Transform[] _spawnPositions;
    private readonly int _minTotems;
    private readonly int _maxTotems;

    public TotemSpawner(
        IObjectResolver resolver, 
        ObjectiveTotem totemPrefab, 
        Transform[] spawnPositions, 
        int minTotems, 
        int maxTotems)
    {
        _resolver = resolver;
        _totemPrefab = totemPrefab;
        _spawnPositions = spawnPositions;
        _minTotems = minTotems;
        _maxTotems = maxTotems;
    }

    public void Start()
    {
        SpawnTotems();
    }

    private void SpawnTotems()
    {

        int count = Random.Range(_minTotems, _maxTotems + 1);
        count = Mathf.Clamp(count, 0, _spawnPositions.Length);

        Debug.Log($"TotemSpawner: Spawning {count} totems at random positions.");

        // Create a copy of the positions to pick from without repeating
        List<Transform> availablePositions = new List<Transform>(_spawnPositions);

        for (int i = 0; i < count; i++)
        {
            if (availablePositions.Count == 0) break;

            int randomIndex = Random.Range(0, availablePositions.Count);
            Transform spawnPoint = availablePositions[randomIndex];
            availablePositions.RemoveAt(randomIndex);

            // Instantiate using VContainer
            _resolver.Instantiate(_totemPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
