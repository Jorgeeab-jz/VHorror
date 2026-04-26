using Microlight.MicroBar;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VHorror.Scripts.MonoBehaviors;

public class GameLifeTimeScope: LifetimeScope
{
    [Header("Prefabs")]
    [SerializeField] private AudioManager audioManagerPrefab;
    [SerializeField] private ObjectiveTotem _totemPrefab;
    [SerializeField] private Battery _batteryPrefab;

    [Space(10)]
    [Header("Spawn Positions")]
    [SerializeField] private Transform[] _totemsPositions;
    [SerializeField] private Transform[] _batteryPositions;
    
    [SerializeField] private int _minTotems = 1;
    [SerializeField] private int _maxTotems = 3;


    [Header("UI & Reset Handlers")]
    [SerializeField] private GameStatusUIHandler _gameStatusUIHandler;
    [SerializeField] private PlayerResetHandler _playerResetHandler;


    protected override void Configure(IContainerBuilder builder)
    {
        
        builder.Register<GameStateManager>(Lifetime.Singleton).As<IGameStateManager>();
        builder.Register<InventoryManager>(Lifetime.Singleton).As<IInventoryManager>();
        builder.RegisterEntryPoint<PanicManager>(Lifetime.Singleton).As<IPanicManager>();

        
        builder.RegisterComponentInNewPrefab(audioManagerPrefab, Lifetime.Singleton);
        builder.RegisterComponent(_gameStatusUIHandler);
        builder.RegisterComponent(_playerResetHandler);

        builder.RegisterEntryPoint<TotemSpawner>(Lifetime.Singleton)
            .As<ITotemSpawner>()
            .WithParameter("totemPrefab", _totemPrefab)
            .WithParameter("spawnPositions", _totemsPositions)
            .WithParameter("minTotems", _minTotems)
            .WithParameter("maxTotems", _maxTotems);

        builder.RegisterEntryPoint<BatterySpawner>(Lifetime.Singleton)
            .As<IBatterySpawner>()
            .WithParameter("batteryPrefab", _batteryPrefab)
            .WithParameter("spawnPositions", _batteryPositions);

    }
    
}
