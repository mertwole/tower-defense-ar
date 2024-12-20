using Godot;

public abstract partial class XRHandle : Area3D
{
	public abstract void Take(Vector3 position);

	public abstract void Move(Vector3 position);

	public abstract void Release();
}
