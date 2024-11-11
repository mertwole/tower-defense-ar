using Godot;

public partial class Main : Node3D
{
	private XRInterface xrInterface;

	public override void _Ready()
	{
		xrInterface = XRServer.FindInterface("OpenXR");
		if (xrInterface != null && xrInterface.IsInitialized())
		{
			GD.Print("OpenXR initialized successfully");

			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);
			GetViewport().UseXR = true;
		}
		else
		{
			GD.Print("OpenXR not initialized, please check if your headset is connected");
		}
	}
}
