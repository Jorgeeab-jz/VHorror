using Microlight.MicroBar;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope: LifetimeScope
{
    [Header("Prefabs")]
    [SerializeField] private AudioManager audioManagerPrefab;


    protected override void Configure(IContainerBuilder builder)
    {
        
        builder.Register<GameStateManager>(Lifetime.Singleton).As<IGameStateManager>();
        builder.Register<InventoryManager>(Lifetime.Singleton).As<IInventoryManager>();
        builder.Register<PanicManager>(Lifetime.Singleton).AsImplementedInterfaces();

        
        builder.RegisterComponentInNewPrefab(audioManagerPrefab, Lifetime.Singleton);

    }
    
}
