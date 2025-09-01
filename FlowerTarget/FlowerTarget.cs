using Godot;
using System;
using System.Diagnostics;

public partial class FlowerTarget : Node2D
{
	public event Action OnScore;
	
	[Export] private AnimatedSprite2D _sprite;
	[Export] private CollisionShape2D _collisionShape;
	[Export] private float _duration = 0.2f; // seconds
	[Export] private float _finalScale = 2.0f; // how big it grows
	
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
		CallDeferred(nameof(DisableCollider));
		PlayTween();
	}

	private void DisableCollider()
	{
		_collisionShape.Disabled = true;
	}
	private async void PlayTween()
	{
		if (_sprite == null)
			return;

		var tween = GetTree().CreateTween();

		// Scale from current scale to larger
		tween.TweenProperty(_sprite, "scale", _sprite.Scale * _finalScale, _duration);

		// Fade alpha from 1 to 0
		tween.TweenProperty(_sprite, "modulate:a", 0.0f, _duration);

		// Wait for tween to complete
		await ToSignal(tween, Tween.SignalName.Finished);

		// Optionally free the sprite after fading
		_sprite.QueueFree();
	}
}
