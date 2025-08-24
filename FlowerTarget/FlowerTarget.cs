using Godot;
using System;
using System.Diagnostics;

public partial class FlowerTarget : Node2D
{
    public void OnBodyEntered(Node2D other)
    {
        if (other is MapBee)
        {
            HandleFlowerDespawn();
        }
    }

    private void HandleFlowerDespawn()
    {
        QueueFree();
    }
}
