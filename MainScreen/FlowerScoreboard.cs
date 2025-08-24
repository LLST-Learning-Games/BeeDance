using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class FlowerScoreboard : Node2D
{
	private int _score = 0;
	private List<FlowerScore> flowerScore = new List<FlowerScore>();
	
	public override void _Ready()
	{
		var children =  GetChildren();
		foreach (var child in children)
		{
			flowerScore.Add(child as FlowerScore);
		}
	}
	
	public void AddScore()
	{
		GD.Print("AddScore");
		BloomFlower(_score);
		_score++;
		
	}
	
	public void BloomFlower(int flowerNumber)
	{
		if (flowerNumber < flowerScore.Count)
			flowerScore[flowerNumber].Bloom();
	}
	
}
