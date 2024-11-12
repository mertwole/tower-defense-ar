using Godot;

public partial class RightHandController : XRController3D
{
	[Export]
	private ReadInput handInput;
	[Export]
	private PackedScene debugGizmo;

	private Handle activeHandle = null;

	private Node3D debugGizmoInstance;

	public override void _Process(double delta)
	{
		activeHandle?.Move(handInput.PinchPosition);
	}

	private void OnButtonPressed(string name)
	{
		if (name == "index_pinch")
		{
			var position = handInput.PinchPosition;
			UpdateDebugGizmo(position);

			activeHandle = TryFindHandle(position);
			activeHandle?.Take(position);
		}
	}

	private void OnButtonReleased(string name)
	{
		if (name == "index_pinch")
		{
			activeHandle?.Release();
			activeHandle = null;
		}
	}

	private Handle TryFindHandle(Vector3 position)
	{
		var query = new PhysicsPointQueryParameters3D();
		query.Position = position;
		query.CollideWithAreas = true;
		query.CollideWithBodies = false;

		var space = GetWorld3D().DirectSpaceState;
		var result = space.IntersectPoint(query);

		foreach (var maybe_handle in result)
		{
			var collider = maybe_handle["collider"];

			if (collider.VariantType != Variant.Type.Object)
			{
				break;
			}

			if (collider.AsGodotObject() is Handle handle)
			{
				return handle;
			}
		}

		return null;
	}

	void UpdateDebugGizmo(Vector3 position)
	{
		if (debugGizmoInstance == null)
		{
			debugGizmoInstance = debugGizmo.Instantiate() as Node3D;
			GetTree().Root.AddChild(debugGizmoInstance);
		}

		debugGizmoInstance.Position = position;
	}
}
