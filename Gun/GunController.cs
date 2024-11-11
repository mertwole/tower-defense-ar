using Godot;

public partial class GunController : XRController3D
{
	[Signal]
	public delegate void TriggerStateChangedEventHandler(bool state);

	private void OnButtonPressed(string name)
	{
		if (name == "trigger_click")
		{
			var _ = EmitSignal(SignalName.TriggerStateChanged, true);
		}
	}

	private void OnButtonReleased(string name)
	{
		if (name == "trigger_click")
		{
			var _ = EmitSignal(SignalName.TriggerStateChanged, false);
		}
	}
}
