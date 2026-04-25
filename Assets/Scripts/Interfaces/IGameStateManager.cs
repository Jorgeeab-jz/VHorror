using System;
using UnityEngine;

public interface IGameStateManager
{
    GameState CurrentState { get; }
    void ChangeState(GameState newState);

    event Action<GameState> OnGameStateChanged;
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    PlayerKilled,
    Escaped
}