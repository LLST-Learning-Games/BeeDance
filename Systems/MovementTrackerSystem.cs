using Godot;
using Godot.Collections;

public partial class MovementTrackerSystem : Node2D
{
    [Export] private Array<Vector2> _movementSnapshots;
    public static MovementTrackerSystem Instance { get; private set; }
    
    
    public override void _Ready()
    {
        if (Instance != null && Instance != this)
        {
            GD.PrintErr("MovementTrackerSystem already exists! Removing duplicate.");
            QueueFree();
            return;
        }

        Instance = this;
        _movementSnapshots = new Array<Vector2>();
    }

    public void AddMovementSnapshot(Vector2 movementSnapshot)
    {
        _movementSnapshots.Add(movementSnapshot);
    }

    public void ClearMovementSnapshots()
    {
        _movementSnapshots.Clear();
    }
}




