using Godot;

public partial class FlowerSpawner : Node2D
{
	[Export] private PackedScene _flowerPrefab;
	[Export] private Area2D _spawnArea;
	
	[Export] private Node2D _flowerScoreboard;
	
	private FlowerTarget _flowerInstance;
	
	private RandomNumberGenerator _rand = new RandomNumberGenerator();

	public override void _Ready()
	{
		SpawnFlower();
	}

	public void SpawnFlower()
	{
		if (_flowerInstance == null)
		{
			_flowerInstance = _flowerPrefab.Instantiate<FlowerTarget>();
			AddChild(_flowerInstance);
		}

		_flowerInstance.OnScore += FlowerInstanceOnOnScore;
		
		_flowerInstance.Position = GetRandomLocationInSpawnArea();
	}

	private void FlowerInstanceOnOnScore()
	{
		((FlowerScoreboard)_flowerScoreboard).AddScore();
		_flowerInstance = null;
		
		CallDeferred(nameof(SpawnFlower));
	}

	private Vector2 GetRandomLocationInSpawnArea()
	{
		var collisionShape = _spawnArea.GetNode<CollisionPolygon2D>("CollisionPolygon2D");
		if (collisionShape == null)
		{
			return Vector2.Zero;
		}

		Vector2[] points = collisionShape.Polygon;
		var indices = Geometry2D.TriangulatePolygon(points);
		int triIndex = _rand.RandiRange(0, indices.Length / 3 - 1) * 3;

		Vector2 a = points[indices[triIndex]];
		Vector2 b = points[indices[triIndex + 1]];
		Vector2 c = points[indices[triIndex + 2]];

		// Using barycentric coordinates (uniform distribution inside a triangle)
		float u = _rand.Randf();
		float v = _rand.Randf();

		if (u + v > 1f)
		{
			u = 1f - u;
			v = 1f - v;
		}

		return a + u * (b - a) + v * (c - a);
	}
}
