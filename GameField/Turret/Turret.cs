using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Turret : Node3D
{
	[Export]
	private float cooldown;
	[Export]
	private Node3D body;
	[Export]
	private BulletSpawn bulletSpawn;

	private Dictionary<Area3D, Enemy> enemiesInRange = new Dictionary<Area3D, Enemy>();

	private float fromLatestFire = 0.0f;

	public override void _Process(double delta)
	{
		fromLatestFire += (float)delta;

		var closestEnemy = enemiesInRange.Keys.MinBy(area => area.GlobalPosition.DistanceSquaredTo(GlobalPosition));
		if (closestEnemy != null)
		{
			body.LookAt(closestEnemy.GlobalPosition);

			if (fromLatestFire >= cooldown)
			{
				fromLatestFire = 0;
				bulletSpawn.Fire();
			}
		}
	}

	private void EnemyEnteredRange(Area3D enemy)
	{
		var enemyScript = enemy.TryFindParentRecursive<Enemy>();
		if (enemyScript != null)
		{
			enemiesInRange.Add(enemy, enemyScript);
		}
	}

	private void EnemyExitedRange(Area3D enemy)
	{
		enemiesInRange.Remove(enemy);
	}
}
