using System;
using UnityEngine;
using VContainer;

public class InventoryManager : IInventoryManager
{
    private int _totemsCollected;
    public int TotemsCollected => _totemsCollected;
    public event Action<int> OnTotemsCountChanged;

    private readonly IGameStateManager _gameStateManager;

    [Inject]
    public InventoryManager(IGameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }

    public void AddTotem()
    {
        _totemsCollected++;
        Debug.Log($"Totem collected! Total: {_totemsCollected}");
        
        OnTotemsCountChanged?.Invoke(_totemsCollected);

        if (_totemsCollected >= 3)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("3 Totems collected! Ending game...");
        _gameStateManager.ChangeState(GameState.Escaped);
    }
}
