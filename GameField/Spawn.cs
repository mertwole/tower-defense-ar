using Godot;

public partial class Spawn : Path3D
{
	[Export]
	private PackedScene enemy;

	public override void _Ready()
	{
		var instance = enemy.Instantiate();
		AddChild(instance);
	}
}
