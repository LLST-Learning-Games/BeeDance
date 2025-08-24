using Godot;
using System;
using Godot.Collections;

public partial class BeeBodyAnimatorController : AnimatedSprite2D
{
    private RandomNumberGenerator _rand = new(); 
    
    public void PlayRandomAnimation()
    {
        int i = SpriteFrames.Animations.Count;
        int randIndex = _rand.RandiRange(0, i-1);
        
        // forgive me for my sins
        Play(SpriteFrames.GetAnimationNames()[randIndex]);
    }
}
