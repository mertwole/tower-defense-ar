using Godot;

public partial class Bullet : Node3D
{
	[Export]
	private float velocity;

	public override void _Process(double delta)
	{
		GlobalPosition += -GlobalBasis.Z * velocity * (float)delta;
	}
}
