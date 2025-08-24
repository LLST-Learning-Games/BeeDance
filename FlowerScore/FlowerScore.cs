using Godot;
using System;

public partial class FlowerScore : Node2D
{
	[Export] private Sprite2D _flowerSprite2D;
	
	[Export] private Texture2D _flowerBloomed;
	
	public override void _Ready()
	{
	}
	
	public void Bloom()
	{
		_flowerSprite2D.SetTexture(_flowerBloomed);
	}
}
