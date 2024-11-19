using Godot;

public partial class Enemy : PathFollow3D
{
	[Export]
	private float speed;
	[Export]
	private int hp;

	public override void _Process(double delta)
	{
		Progress += speed * (float)delta;
	}

	public void Damage(int damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			QueueFree();
		}
	}
}
