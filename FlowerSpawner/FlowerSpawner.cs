using Godot;
using System;

public partial class FlowerSpawner : Node2D
{
    [Export] private PackedScene _flowerPrefab;
    [Export] private Area2D _spawnArea;
    
    private FlowerTarget _flowerInstance;
    
    private Random _rand = new Random();

    public override void _Ready()
    {
        SpawnFlower();
    }

    public void SpawnFlower()
    {
        //if (_flowerInstance == null)
        {
            _flowerInstance = _flowerPrefab.Instantiate<FlowerTarget>();
            _flowerInstance.SetSpawner(this);
            AddChild(_flowerInstance);
        }
        _flowerInstance.Position = GetRandomLocationInSpawnArea();
    }

    private Vector2 GetRandomLocationInSpawnArea()
    {
        // Assuming the Area2D has a CollisionShape2D child
        var collisionShape = _spawnArea.GetNode<CollisionShape2D>("CollisionShape2D");
        if (collisionShape == null || collisionShape.Shape == null)
        {
            return Vector2.Zero; // fallback
        }

        if (collisionShape.Shape is RectangleShape2D rect)
        {
            float x = (float)(_rand.NextDouble() * rect.Size.X - rect.Size.X / 2);
            float y = (float)(_rand.NextDouble() * rect.Size.Y - rect.Size.Y / 2);
            return new Vector2(x, y);
        }
        
        GD.PrintErr("Unsupported shape for random point sampling");
        return Vector2.Zero;
    }
}
