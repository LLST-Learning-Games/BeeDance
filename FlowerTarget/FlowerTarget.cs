using Godot;
using System;
using System.Diagnostics;

public partial class FlowerTarget : Node2D
{
	public event Action OnScore;
	
	public void OnBodyEntered(Node2D other)
	{
		if (other is MapBee)
		{
			OnScore?.Invoke();
			var mapBee = other as MapBee;
			mapBee.MarkFlowerReached();
			HandleFlowerDespawn();
		}
	}

	private void HandleFlowerDespawn()
	{
		QueueFree();
	}
}
