using Godot;

public partial class WeaponHandle : Handle
{
    [Export]
    private Node3D weapon;

    Vector3 initialPosition;
    Vector3 initialHandlePosition;

    SnapPoint currentSnapPoint;

    public override void Take(Vector3 position)
    {
        initialPosition = weapon.GlobalPosition;
        initialHandlePosition = position;
    }

    public override void Move(Vector3 position)
    {
        var snapPoint = TryGetClosestSnapPoint(position);
        if (snapPoint != null)
        {
            currentSnapPoint = snapPoint;
            weapon.GlobalPosition = snapPoint.GlobalPosition;
        }
        else
        {
            weapon.GlobalPosition = initialPosition + (position - initialHandlePosition);
        }

    }

    public override void Release()
    {
        if (currentSnapPoint != null)
        {
            currentSnapPoint.Taken = true;
        }
    }

    private SnapPoint TryGetClosestSnapPoint(Vector3 position)
    {
        var query = new PhysicsPointQueryParameters3D();
        query.CollideWithAreas = false;
        query.CollideWithBodies = true;
        query.Position = position;

        var space = GetWorld3D().DirectSpaceState;
        var result = space.IntersectPoint(query);

        SnapPoint closest = null;

        foreach (var maybeSnapPoint in result)
        {
            var collider = maybeSnapPoint["collider"];

            if (collider.VariantType != Variant.Type.Object)
            {
                continue;
            }

            if (collider.AsGodotObject() is SnapPoint snapPoint)
            {
                if (snapPoint.Taken)
                {
                    continue;
                }

                if (closest == null)
                {
                    closest = snapPoint;
                }
                else
                {
                    var distance = snapPoint.GlobalPosition.DistanceSquaredTo(position);
                    var closestDistance = closest.GlobalPosition.DistanceSquaredTo(position);

                    if (distance < closestDistance)
                    {
                        closest = snapPoint;
                    }
                }
            }
        }

        return closest;
    }
}