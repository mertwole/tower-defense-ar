using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BulletSpawn : Node3D
{
	[Export]
	private PackedScene bullet;

	private List<Node3D> spawnPoints;
	private int lastFired = -1;

	public override void _Ready()
	{
		spawnPoints = GetChildren().Select(c => c as Node3D).ToList();
	}

	public void Fire()
	{
		lastFired = (lastFired + 1) % spawnPoints.Count;

		var instance = bullet.Instantiate() as Bullet;
		GetTree().Root.AddChild(instance);
		instance.GlobalTransform = spawnPoints[lastFired].GlobalTransform;
	}
}
