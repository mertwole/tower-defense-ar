using Godot;

public partial class PrepareField : GridMap
{
	[Export]
	private GridMap snapPointMarkers;
	[Export]
	private PackedScene snapPointScene;
	[Export]
	private float terrainThickness;

	public override void _Ready()
	{
		var validSnapPoints = snapPointMarkers.GetUsedCells();
		foreach (var snapPointPosition in validSnapPoints)
		{
			var positon = MapToLocal(snapPointPosition);
			positon.Y += terrainThickness;

			var instance = snapPointScene.Instantiate() as Node3D;
			AddChild(instance);
			instance.Position = positon;
		}
	}
}
