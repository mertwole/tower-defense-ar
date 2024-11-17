using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Turret : Node3D
{
	[Export]
	private float cooldown;
	[Export]
	private Node3D body;

	private Dictionary<Area3D, Enemy> enemiesInRange = new Dictionary<Area3D, Enemy>();

	public override void _Process(double delta)
	{
		var closestEnemy = enemiesInRange.Keys.MinBy(area => area.GlobalPosition.DistanceSquaredTo(GlobalPosition));
		if (closestEnemy != null)
		{
			body.LookAt(closestEnemy.GlobalPosition);
		}
	}

	private void EnemyEnteredRange(Area3D enemy)
	{
		var enemyScript = TryFindEnemy(enemy);
		if (enemyScript != null)
		{
			enemiesInRange.Add(enemy, enemyScript);
		}
	}

	private void EnemyExitedRange(Area3D enemy)
	{
		enemiesInRange.Remove(enemy);
	}

	private Enemy TryFindEnemy(Area3D hitbox)
	{
		Node3D current = hitbox;
		while (true)
		{
			if (current is Enemy enemy)
			{
				return enemy;
			}

			var parent = current.GetParentNode3D();
			if (parent == null)
			{
				return null;
			}
			else
			{
				current = parent;
			}
		}
	}
}
