using Godot;

public partial class Spawn : Node3D
{
	[Export]
	private Navigation navigation;
	[Export]
	private PackedScene enemy;

	bool first = true;

	public override void _Process(double delta)
	{
		if (first)
		{
			InstantiateEnemy(navigation.Spawns[0]);
			InstantiateEnemy(navigation.Spawns[1]);

			first = false;
		}
	}

	private void InstantiateEnemy(Navigation.Spawn spawnPoint)
	{
		var instance = enemy.Instantiate() as Enemy;
		AddChild(instance);
		instance.GlobalPosition = navigation.CellToGlobalPosition(spawnPoint.Position);
		instance.Initialize(spawnPoint, navigation);
	}
}
