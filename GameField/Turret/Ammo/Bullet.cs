using Godot;

public partial class Bullet : Node3D
{
	[Export]
	private float velocity;
	[Export]
	private int damage;

	public override void _Process(double delta)
	{
		GlobalPosition += -GlobalBasis.Z * velocity * (float)delta;
	}

	private void EnemyHit(Area3D hitbox)
	{
		var enemy = hitbox.TryFindParentRecursive<Enemy>();
		if (enemy != null)
		{
			enemy.Damage(damage);
			QueueFree();
		}
	}
}
