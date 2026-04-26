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

        // Pause the game on end states
        if (newState == GameState.PlayerKilled || newState == GameState.Escaped)
        {
            Time.timeScale = 0f;
            Debug.Log("Game Paused (Time.timeScale = 0)");
        }
        else if (newState == GameState.Playing)
        {
            Time.timeScale = 1f;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}
