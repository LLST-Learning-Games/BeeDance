using Godot;
using System;

public partial class Map : Node2D
{
	[Export] private PackedScene _beePrefab;
	private MapBee _beeInstance;
	public static Map Instance { get; private set; }
	
	
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
		if (_beeInstance == null)
		{
			_beeInstance = _beePrefab.Instantiate() as MapBee;
			AddChild(_beeInstance);
			GD.Print("Spawning bee");
			_beeInstance.Position = Vector2.Zero;
		}
		
		_beeInstance.SetPath(MovementTrackerSystem.Instance.GetMovementSnapshots());
		MovementTrackerSystem.Instance.ClearMovementSnapshots();
	}
}
