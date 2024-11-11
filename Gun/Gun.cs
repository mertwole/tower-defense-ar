using Godot;
using System;

public partial class Gun : Node3D
{
	[Export]
	public float ShotCooldown { get; set; } = 0.1f;

	private bool shootPressed;
	private float fromPreviousShot;

	private PackedScene bullet;

	public override void _Ready()
	{
		bullet = GD.Load<PackedScene>("res://Gun/bullet.tscn");
	}

	public override void _Process(double delta)
	{
		if (shootPressed)
		{
			if (fromPreviousShot < ShotCooldown)
			{
				fromPreviousShot += (float)delta;
			}
			else
			{
				fromPreviousShot = 0;
				SpawnBullet();
			}
		}
	}

	private void SpawnBullet()
	{
		var instance = bullet.Instantiate();
		GetTree().Root.AddChild(instance);

		(instance as Node3D).Position = GlobalPosition;

		var direction = -GlobalBasis.Z;
		instance.GetNode<RigidBody3D>("RigidBody3D").LinearVelocity = direction * 10;
	}

	private void Shoot(bool pressed)
	{
		shootPressed = pressed;
	}
}
