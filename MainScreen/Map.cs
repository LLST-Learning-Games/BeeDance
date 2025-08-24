using Godot;
using System;
using System.Linq;

public partial class Map : Node2D
{
	[Export] private PackedScene _beePrefab;
	private MapBee _beeInstance;
	public static Map Instance { get; private set; }
	private float _maxMovement = 10;
	
	public override void _Ready()
	{
		if (Instance != null && Instance != this)
		{
			GD.PrintErr("MovementTrackerSystem already exists! Removing duplicate.");
			QueueFree();
			return;
		}

		Instance = this;
	}

	public void SpawnBeeAndMove()
	{
		var bee = _beePrefab.Instantiate() as MapBee;
		AddChild(bee);
		GD.Print("Spawning bee");
		bee.Position = Vector2.Zero;

		var movementSnapshots = MovementTrackerSystem.Instance.GetMovementSnapshots().ToList();
		
		bee.SetPath(movementSnapshots);
		MovementTrackerSystem.Instance.ClearMovementSnapshots();
	}
}
