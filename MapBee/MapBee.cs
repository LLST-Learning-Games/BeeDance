using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class MapBee : RigidBody2D
{
	[Export] private float _speed = 3f;
	[Export] private CollisionShape2D collisionShape;
	
	private List<Vector2> _path;
	private Vector2 origin;
	private bool flowerReached = false;

	public override void _Ready()
	{
		base._Ready();
		
		origin = Position;
	}

	public void SetPath(List<Vector2> path)
	{
		_path = path.ToList();
	}

	public void MarkFlowerReached()
	{
		CallDeferred(nameof(DisablePhysics));
		flowerReached = true;
		LinearVelocity = Vector2.Zero;
	}

	public void DisablePhysics()
	{
		Freeze = true;
		FreezeMode = FreezeModeEnum.Kinematic;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (flowerReached) return;
		
		if (_path != null && _path.Count > 1)
		{
			LinearVelocity = _path[0].Normalized() * _path[0].DistanceTo(_path[1]) * _speed;
			Rotation = _path[0].Angle() + 90f;
			_path.RemoveAt(0);
		}
		else
		{
			if (origin.DistanceTo(Position) > 10)
			{
				collisionShape.Disabled = true;
				
				var direction = (origin - Position);
				LinearVelocity = (origin - Position) * _speed / 20;
				Rotation = direction.Angle() + 90f;
			}
			else
			{
				LinearVelocity = Vector2.Zero;
				QueueFree();
			}
		}
	}
}
