using Godot;

public partial class Handle : Area3D
{
	[Export]
	private Node3D movedObject;

	Vector3 initialPosition;
	Vector3 initialHandlePosition;

	public void Take(Vector3 position)
	{
		initialPosition = movedObject.GlobalPosition;
		initialHandlePosition = position;
	}

	public void Move(Vector3 position)
	{
		movedObject.GlobalPosition = initialPosition + (position - initialHandlePosition);
	}

	public void Release()
	{

	}
}
