using Godot;

public static class ParentUtils
{
    public static T TryFindParentRecursive<T>(this Node3D node) where T : class
    {
        while (true)
        {
            if (node is T t)
            {
                return t;
            }

            var parent = node.GetParentNode3D();
            if (parent == null)
            {
                return null;
            }
            else
            {
                node = parent;
            }
        }
    }
}