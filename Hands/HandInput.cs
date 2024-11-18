using Godot;

public partial class HandInput : XRController3D
{
	[Export]
	private ReadInput handInput;

	private XRHandle activeHandle = null;

	public override void _Process(double delta)
	{
		activeHandle?.Move(handInput.PinchPosition);
	}

	private void OnButtonPressed(string name)
	{
		if (name == "index_pinch")
		{
			var position = handInput.PinchPosition;

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

	private XRHandle TryFindHandle(Vector3 position)
	{
		var query = new PhysicsPointQueryParameters3D();
		query.Position = position;
		query.CollideWithAreas = true;
		query.CollideWithBodies = false;

		var space = GetWorld3D().DirectSpaceState;
		var result = space.IntersectPoint(query);

		foreach (var maybeHandle in result)
		{
			var collider = maybeHandle["collider"];

			if (collider.VariantType != Variant.Type.Object)
			{
				continue;
			}

			if (collider.AsGodotObject() is XRHandle handle)
			{
				return handle;
			}
		}

		return null;
	}
}
