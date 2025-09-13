using Godot;
using System;

public partial class BeeArmsAnimatorController : AnimatedSprite2D
{
	[Export] private Timer _ArmAnimationTimer;
	
	private RandomNumberGenerator _rand = new();

	public void PlayRandomAnimation()
	{
		int i = SpriteFrames.Animations.Count;
		int randIndex = _rand.RandiRange(0, i - 1);

		// forgive me for my sins
		Play(SpriteFrames.GetAnimationNames()[randIndex]);
	}

	public void OnAnimationFinished()
	{
		_ArmAnimationTimer.Stop();
		_ArmAnimationTimer.WaitTime = _rand.Randf()*4+1;
		_ArmAnimationTimer.Start();
	}
}
