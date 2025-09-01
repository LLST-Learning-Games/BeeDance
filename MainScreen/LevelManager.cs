using Godot;
using System;

public partial class LevelManager : Node2D
{
	[Export] public FlowerScoreboard FlowerScoreboard;
	[Export] public EnergyGauge _energyGauge;
	[Export] public int ScoretoWin = 5;
	[Export] public Node2D WinNode;
	[Export] public Node2D LoseNode;

	public static LevelManager Instance { get; private set; }
	
	private bool gameOver = false;
	public bool GameOver => gameOver;
	public Action<bool> OnGameOverReportWin;
	public Action OnGameStateReset;

	public override void _EnterTree()
	{
		InitSingleton();
	}

	public override void _Ready()
	{
		Initialize();
	}

	private void InitSingleton()
	{
		if (Instance != null && Instance != this)
		{
			GD.PrintErr("LevelManager already exists! Removing duplicate.");
			QueueFree();
			return;
		}

		Instance = this;
	}

	private void Initialize()
	{
		WinNode.Visible = false;
		LoseNode.Visible = false;
	}

	public override void _Process(double delta)
	{
		HandleDebugInput();

		if (gameOver) return;

		if (FlowerScoreboard.Score >= ScoretoWin)
		{
			HandleWinState();
		} 
		else if (_energyGauge.GetTimeLeft() == 0) //check timer here
		{
			HandleLoseState();
		}
	}

	private void HandleLoseState()
	{
		if(!gameOver)
		{
			LoseNode.Visible = true;
			gameOver = true;
			OnGameOverReportWin?.Invoke(gameOver);
		}
	}

	private void HandleWinState()
	{
		if (!gameOver)
		{
			WinNode.Visible = true;
			gameOver = true;
			OnGameOverReportWin?.Invoke(gameOver);
		}
	}

	private void HandleDebugInput()
	{
		if (Input.IsPhysicalKeyPressed(Key.O))
		{
			HandleWinState();
		}
		
		if (Input.IsPhysicalKeyPressed(Key.L))
		{
			HandleLoseState();
		}
		
		if (Input.IsPhysicalKeyPressed(Key.P))
		{
			ResetGameState();
		}
	}

	public void ResetGameState()
	{
		gameOver = false;
		LoseNode.Visible = false;
		WinNode.Visible = false;
		OnGameStateReset?.Invoke();
	}
}
