using Godot;
using System;

public partial class EnergyGauge : Node2D
{
	[Export] private Timer _energyTimer;
	[Export] private AnimatedSprite2D _gaugeBase;
	[Export] private Node2D _gaugeStickRotator;
	[Export] private float GaugeEmptyPoint;
	[Export] private float GaugeFullPoint;
	[Export] private float DurationOfGame;
	[Export] private float PercentToStartFlashing;
	[Export] private float AnimationSpeed;
	private double previousRotation;
	public static EnergyGauge Instance { get; private set; }

	public override void _Ready()
	{
		if (Instance != null && Instance != this)
		{
			GD.PrintErr("EnergyGauge already exists! Removing duplicate.");
			QueueFree();
			return;
		}

		Instance = this;
		RestartTimer();
		BindListeners();
	}

	private void BindListeners()
	{
		LevelManager.Instance.OnGameOverReportWin += PauseTimer;
		LevelManager.Instance.OnGameStateReset += RestartTimer;
		_energyTimer.Timeout += OnEnergyDepleted;
	}

	private void RestartTimer()
	{
		_gaugeStickRotator.RotationDegrees = GaugeFullPoint;
		_energyTimer.WaitTime = DurationOfGame;
		_energyTimer.SetPaused(false);
		_energyTimer.Start();
		previousRotation = GetGaugeRotationRange();
		_gaugeBase.Stop();
	}
	
	private void PauseTimer(bool _)
	{
		_energyTimer.SetPaused(true);
	}


	public override void _Process(double delta)
	{
		base._Process(delta);

		double targeRotation = GetPercentRemainingGauge() * GetGaugeRotationRange();
		//GD.Print("PreviousRotation: " + previousRotation.ToString() + ", Target: " + targeRotation.ToString());
		_gaugeStickRotator.Rotate((float)Mathf.DegToRad((targeRotation - previousRotation)));
		previousRotation = targeRotation;
		//GD.Print("EnergyWait: " + _energyTimer.WaitTime.ToString() + "GameDuration: " + DurationOfGame.ToString());
		//GD.Print("PercentRemaining: " + GetPercentRemainingGauge().ToString() + "PercentToStartFlashing: " + PercentToStartFlashing.ToString());
		if (GetPercentRemainingGauge() < PercentToStartFlashing && !_gaugeBase.IsPlaying())
		{
			_gaugeBase.Play();
		}
		if (_gaugeBase.IsPlaying())
		{
			//start flashing at 0.2? at 0.15 left we'll have speedScaler == 1-(0.15/0.2) == 0.25, later that will be clamped at 0.3333 and multiplied by 3 so we're at 1x speed for a while
			//or if we're at 0.01 left we'll have 1-(0.01/0.2) == 0.95, multiplied by 3 to be nearly 3x speed
			double speedScalerFromTimeLeft = 1 - (GetPercentRemainingGauge()/Math.Max(0.0000001, PercentToStartFlashing));
			_gaugeBase.SpeedScale = (float)(3.0 * Math.Max(0.3333, speedScalerFromTimeLeft));
		}
	}

	private double GetGaugeRotationRange()
	{
		return Math.Abs(GaugeFullPoint - GaugeEmptyPoint);
	}

	private double GetPercentRemainingGauge()
	{
		if (_energyTimer == null)
			return -1.0;

		//GD.Print("GetTimeLeft: " + GetTimeLeft().ToString() + ", waitTime: " + _energyTimer.WaitTime.ToString());
		return (GetTimeLeft() / _energyTimer.WaitTime);
	}
	public double GetTimeLeft()
	{
		if (_energyTimer != null)
		{
			return _energyTimer.TimeLeft;
		}

		return -1.0;
	}

	public void OnEnergyDepleted()
	{
		_energyTimer.Stop();
	}
}
