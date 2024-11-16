using Godot;

public partial class Enemy : Node3D
{
	[Export]
	private float speed;

	Navigation navigation;
	Vector3I position;
	Vector3I direction;

	public void Initialize(Navigation.Spawn spawnPoint, Navigation navigation)
	{
		position = spawnPoint.Position;
		direction = spawnPoint.Direction;

		this.navigation = navigation;
	}

	public override void _Process(double delta)
	{
		if (navigation == null)
		{
			return;
		}

		Vector3 targetPosition = navigation.CellToGlobalPosition(position);
		Vector3 movement = navigation.DirectionToGlobal(direction);

		var toDestination = targetPosition - GlobalPosition;
		var arrived = toDestination.Dot(movement) < .0f;
		if (!arrived)
		{
			GlobalPosition += movement * speed * (float)delta;
		}
		else
		{
			if (navigation.End == position)
			{
				// TODO: Lose.
				return;
			}

			if (navigation.Directions.TryGetValue(position, out Vector3I newDirection))
			{
				direction = newDirection;
			}

			position += direction;
			GlobalPosition = targetPosition;
		}
	}
}
