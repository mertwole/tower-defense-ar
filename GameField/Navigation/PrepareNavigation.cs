using System;
using Godot;

public partial class PrepareNavigation : GridMap
{
	public override void _Ready()
	{
		var usedCells = GetUsedCells();

		foreach (var cell in usedCells)
		{
			var orientation = GetCellItemOrientation(cell);
			var direction = OrientationToDirection(orientation);

			// TODO: Fill map.
		}
	}

	Vector3I OrientationToDirection(int orientation)
	{
		return orientation switch
		{
			(>= 16) and (<= 19) => new Vector3I(0, 0, -1),
			(>= 20) and (<= 23) => new Vector3I(0, 0, 1),
			2 or 6 or 10 or 14 => new Vector3I(-1, 0, 0),
			0 or 4 or 8 or 12 => new Vector3I(1, 0, 0),
			3 or 7 or 11 or 15 => new Vector3I(0, -1, 0),
			1 or 5 or 9 or 13 => new Vector3I(0, 1, 0),
			_ => throw new ArgumentException()
		};
	}
}
