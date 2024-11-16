using System;
using System.Collections.Generic;
using Godot;

public partial class PrepareNavigation : GridMap
{
	Vector3I end;
	List<Vector3I> spawns = new List<Vector3I>();
	Dictionary<Vector3I, Vector3I> directions = new Dictionary<Vector3I, Vector3I>();

	public override void _Ready()
	{
		var usedCells = GetUsedCells();

		bool endFound = false;

		foreach (var cell in usedCells)
		{
			var id = GetCellItem(cell);
			var name = MeshLibrary.GetItemName(id);

			switch (name)
			{
				case "End":
					end = cell;
					endFound = true;
					break;
				case "Direction":
					var orientation = GetCellItemOrientation(cell);
					var direction = OrientationToDirection(orientation);
					directions.Add(cell, direction);
					break;
				default:
					if (name.StartsWith("Spawn"))
					{
						spawns.Add(cell);
					}
					else
					{
						throw new ArgumentException("Invalid navigation marker name found: " + name);
					}
					break;
			};
		}

		if (!endFound)
		{
			throw new Exception("No ending position is found in navigation map");
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
