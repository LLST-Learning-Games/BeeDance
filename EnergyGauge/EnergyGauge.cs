using Godot;
using System;

public partial class EnergyGauge : Node2D
{
	[Export] private Timer _energyTimer;
	[Export] private AnimatedSprite2D _gaugeBase;
	[Export] private AnimatedSprite2D _gaugeStick;
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
		_energyTimer.Timeout += OnEnergyDepleted;
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
