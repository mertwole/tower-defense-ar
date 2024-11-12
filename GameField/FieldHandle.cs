using Godot;

public partial class FieldHandle : Handle
{
    [Export]
    private Node3D field;

    Vector3 initialPosition;
    Vector3 initialHandlePosition;

    public override void Take(Vector3 position)
    {
        initialPosition = field.GlobalPosition;
        initialHandlePosition = position;
    }

    public override void Move(Vector3 position)
    {
        field.GlobalPosition = initialPosition + (position - initialHandlePosition);
    }

    public override void Release()
    {

    }
}