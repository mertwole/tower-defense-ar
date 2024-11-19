using Godot;

public partial class GeneratePathMesh : MeshInstance3D
{
	public override void _Ready()
	{
		var curve = GetParent<Path3D>().Curve;
		var points = curve.GetBakedPoints();

		var surface = new SurfaceTool();
		surface.Begin(Mesh.PrimitiveType.LineStrip);

		foreach (var point in points)
		{
			surface.AddVertex(point);
		}

		Mesh = surface.Commit();
	}
}
