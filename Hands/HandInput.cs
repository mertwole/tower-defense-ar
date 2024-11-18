using Godot;

public partial class HandInput : XRController3D
{
	enum PinchGesture
	{
		Index,
		Middle,
		Ring,
		Little
	}

	[Export]
	private ReadInput handInput;

	private XRHandle activeHandle = null;
	private PinchGesture? pinchGesture = null;

	public override void _Process(double delta)
	{
		activeHandle?.Move(GetPinchPosition());
	}

	private void OnButtonPressed(string name)
	{
		pinchGesture = EventNameToPinchGesture(name);

		if (pinchGesture != null)
		{
			var position = GetPinchPosition();

			activeHandle = TryFindHandle(position);
			activeHandle?.Take(position);
		}
	}

	private void OnButtonReleased(string name)
	{
		var gesture = EventNameToPinchGesture(name);

		if (gesture != null && gesture == pinchGesture)
		{
			activeHandle?.Release();
			activeHandle = null;
		}
	}

	private Vector3 GetPinchPosition()
	{
		var fingerPosition = pinchGesture switch
		{
			PinchGesture.Index => handInput.IndexTipPosition,
			PinchGesture.Middle => handInput.MiddleTipPosition,
			PinchGesture.Ring => handInput.RingTipPosition,
			PinchGesture.Little => handInput.LittleTipPosition,
			_ => handInput.ThumbTipPosition
		};

		return (handInput.ThumbTipPosition + fingerPosition) * new Vector3(.5f, .5f, .5f);
	}

	private PinchGesture? EventNameToPinchGesture(string name)
	{
		return name switch
		{
			"index_pinch" => PinchGesture.Index,
			"middle_pinch" => PinchGesture.Middle,
			"ring_pinch" => PinchGesture.Ring,
			"little_pinch" => PinchGesture.Little,
			_ => null
		};
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
