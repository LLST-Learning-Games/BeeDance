using Godot;
using System;

public partial class BeeMouseInput : Node2D
{
	[Export] private Node2D _beeButt; 
	[Export] private float _distanceRatio = 3f;
	[Export] private float _maxInputDistance = 3f;
	[Export] private float _maxDistance = 3f;
	private bool _isMouseButtonDown;
	
	public override void _Input(InputEvent @event)
	{
		
		if (@event is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
			{
				if (LevelManager.Instance.GameOver)
				{
					LevelManager.Instance.ResetGameState();
				}
				
				// this means mouse button was just lifted
				if (_isMouseButtonDown && !mouseButtonEvent.Pressed)
				{
					_beeButt.Position = Vector2.Zero;
					Map.Instance.SpawnBeeAndMove();
				}
				_isMouseButtonDown = mouseButtonEvent.Pressed;
			}
		}

		if (!_isMouseButtonDown)
		{
			return;
		}
		
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			var relativePosition = eventMouseMotion.Position - GlobalPosition;
			
			if(relativePosition.Length() > _maxInputDistance)
			{   
				relativePosition = relativePosition.Normalized() * _maxInputDistance;
			}
			
			relativePosition /= _distanceRatio;
			
			if(relativePosition.Length() > _maxDistance)
			{   
				relativePosition = relativePosition.Normalized() * _maxDistance;
			}
			
			_beeButt.Position = relativePosition;
			MovementTrackerSystem.Instance.AddMovementSnapshot(relativePosition);
		}
	}
}
