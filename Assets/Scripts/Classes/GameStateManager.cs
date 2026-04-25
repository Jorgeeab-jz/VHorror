using System;
using UnityEngine;

public class GameStateManager: IGameStateManager
{
    private GameState _currentState;

    public GameState CurrentState => _currentState;

    public event Action<GameState> OnGameStateChanged;

    public GameStateManager()
    {
        _currentState = GameState.Playing;
    }

    public void ChangeState(GameState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        Debug.Log($"Game State changed to: {newState}");

        OnGameStateChanged?.Invoke(newState);
    }
}
