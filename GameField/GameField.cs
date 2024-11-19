using Godot;

public partial class GameField : Node3D
{
	public void Initialize(Aabb table)
	{
		var topCenter = table.Position + table.Size * new Vector3(.5f, .5f, 1);

		GlobalPosition = topCenter;
		GlobalRotate(new Vector3(1, 0, 0), Mathf.Pi * .5f);
	}
}
