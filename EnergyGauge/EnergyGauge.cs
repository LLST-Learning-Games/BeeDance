using Godot;
using System;

public partial class EnergyGauge : Node2D
{
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
	}
}
