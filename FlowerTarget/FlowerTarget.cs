using Godot;
using System;
using System.Diagnostics;

public partial class FlowerTarget : Node2D
{
    private FlowerSpawner _spawner;
    
    public void SetSpawner(FlowerSpawner spawner) => _spawner = spawner;
    
    public void OnBodyEntered(Node2D other)
    {
        if (other is MapBee)
        {
            HandleFlowerDespawn();
        }
    }

    private void HandleFlowerDespawn()
    {
        QueueFree();
        _spawner.SpawnFlower();
    }
}
