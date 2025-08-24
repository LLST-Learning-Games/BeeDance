using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class MapBee : RigidBody2D
{
	[Export] private float _speed = 3f;
	
	private List<Vector2> _path;
	
	public void SetPath(Array<Vector2> path)
	{
		_path = path.ToList();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_path != null && _path.Count > 1)
		{
			LinearVelocity = _path[0].Normalized() * _path[0].DistanceTo(_path[1]) * _speed;
			Rotation = _path[0].Angle() + 90f;
			_path.RemoveAt(0);
		}
		else
		{
			LinearVelocity = Vector2.Zero;
		}
	}
}
