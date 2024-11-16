using System;
using System.Collections.Generic;
using Godot;

public partial class Navigation : GridMap
{
	public struct Spawn
	{
		public Vector3I Position;
		public Vector3I Direction;
		public int Index;
	}

	public Vector3I End { get; private set; }
	public List<Spawn> Spawns { get; private set; } = new List<Spawn>();
	public Dictionary<Vector3I, Vector3I> Directions { get; private set; } = new Dictionary<Vector3I, Vector3I>();

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
					{
						End = cell;
						endFound = true;
						break;
					}
				case "Direction":
					{
						var orientation = GetCellItemOrientation(cell);
						var direction = OrientationToDirection(orientation);
						Directions.Add(cell, direction);
						break;
					}
				default:
					{
						if (name.StartsWith("Spawn"))
						{
							var idxString = name.Substring("Spawn".Length);
							var idx = int.Parse(idxString);

							var orientation = GetCellItemOrientation(cell);
							var direction = OrientationToDirection(orientation);

							Spawns.Add(new Spawn { Direction = direction, Position = cell, Index = idx });
						}
						else
						{
							throw new ArgumentException("Invalid navigation marker name found: " + name);
						}
						break;
					}
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

	public Vector3 CellToGlobalPosition(Vector3I cell)
	{
		return ToGlobal(MapToLocal(cell));
	}

	public Vector3 DirectionToGlobal(Vector3I direction)
	{
		return ToGlobal(MapToLocal(direction)) - ToGlobal(MapToLocal(Vector3I.Zero));
	}
}
