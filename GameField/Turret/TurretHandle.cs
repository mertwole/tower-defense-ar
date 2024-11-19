using Godot;

public partial class TurretHandle : XRHandle
{
    [Export]
    private Node3D weapon;

    Vector3 initialPosition;
    Vector3 initialHandlePosition;

    public override void Take(Vector3 position)
    {
        initialPosition = weapon.GlobalPosition;
        initialHandlePosition = position;
    }

    public override void Move(Vector3 position)
    {
        weapon.GlobalPosition = initialPosition + (position - initialHandlePosition);
    }

    public override void Release()
    {

    }
}