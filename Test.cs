using Godot;

public partial class Test : XRController3D
{
	[Export]
	private ReadInput handInput;

	[Export]
	private PackedScene debugGizmo;

	private Node3D debugGizmoInstance;

	private void OnButtonPressed(string name)
	{
		if (name == "index_pinch")
		{
			UpdateDebugGizmo();
		}
	}

	private void OnButtonReleased(string name)
	{
		if (name == "index_pinch")
		{
			//
		}
	}

	void UpdateDebugGizmo()
	{
		if (debugGizmoInstance == null)
		{
			debugGizmoInstance = debugGizmo.Instantiate() as Node3D;
			GetTree().Root.AddChild(debugGizmoInstance);
		}

		var position = (handInput.IndexTipPosition + handInput.ThumbTipPosition) * new Vector3(0.5f, 0.5f, 0.5f);
		debugGizmoInstance.Position = position;
	}
}
