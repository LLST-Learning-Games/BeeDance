using Godot;
using System;

public partial class LevelManager : Node2D
{
	[Export] public FlowerScoreboard FlowerScoreboard;
	[Export] public EnergyGauge _energyGauge;
	[Export] public int ScoretoWin = 5;
	[Export] public Node2D WinNode;
	[Export] public Node2D LoseNode;

	
	private bool gameOver = false;
	public bool GameOver => gameOver;
	
	public override void _Ready()
	{
		base._Ready();
		WinNode.Visible = false;
		LoseNode.Visible = false;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (gameOver) return;

		if (FlowerScoreboard.Score >= ScoretoWin)
		{
			WinNode.Visible = true;
			gameOver = true;
		} else if (_energyGauge.GetTimeLeft() == 0) //check timer here
		{
			LoseNode.Visible = true;
			gameOver = true;
		}
	}
}
