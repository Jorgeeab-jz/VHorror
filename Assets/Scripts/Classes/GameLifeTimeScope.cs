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

    [Space(10)]
    [Header("Totems Positions")]
    [Tooltip("Posible positions for the totems to spawn")]
    [SerializeField] private Transform[] _totemsPositions;
    [SerializeField] private int _minTotems = 1;
    [SerializeField] private int _maxTotems = 3;


    protected override void Configure(IContainerBuilder builder)
    {
        
        builder.Register<GameStateManager>(Lifetime.Singleton).As<IGameStateManager>();
        builder.Register<InventoryManager>(Lifetime.Singleton).As<IInventoryManager>();
        builder.Register<PanicManager>(Lifetime.Singleton).AsImplementedInterfaces();

        
        builder.RegisterComponentInNewPrefab(audioManagerPrefab, Lifetime.Singleton);

        builder.RegisterEntryPoint<TotemSpawner>(Lifetime.Singleton)
            .WithParameter("totemPrefab", _totemPrefab)
            .WithParameter("spawnPositions", _totemsPositions)
            .WithParameter("minTotems", _minTotems)
            .WithParameter("maxTotems", _maxTotems);

    }
    
}
