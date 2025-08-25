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
		_gaugeStickRotator.RotationDegrees = GaugeFullPoint;
		_energyTimer.Timeout += OnEnergyDepleted;
		_energyTimer.WaitTime = DurationOfGame;
		_energyTimer.Start();
		previousRotation = GetGaugeRotationRange();
		_gaugeBase.Frame = 0;
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
		if (GetPercentRemainingGauge() < PercentToStartFlashing)
		{
			_gaugeBase.Frame = 1;
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
		//call something to end the game here? Or change what we bind to the signal in the _Ready() above
	}
}
