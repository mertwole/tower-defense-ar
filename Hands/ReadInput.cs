using Godot;

public partial class ReadInput : Node3D
{
	[Export]
	private string indexTipBoneName;
	[Export]
	private string thumbTipBoneName;

	Skeleton3D skeleton;
	int indexFingerTipBone;
	int thumbFingerTipBone;

	public Vector3 IndexTipPosition { get; private set; }
	public Vector3 ThumbTipPosition { get; private set; }

	public override void _Ready()
	{
		skeleton = FindChild("Skeleton3D", true) as Skeleton3D;
		indexFingerTipBone = skeleton.FindBone(indexTipBoneName);
		thumbFingerTipBone = skeleton.FindBone(thumbTipBoneName);
	}

	public override void _Process(double delta)
	{
		IndexTipPosition = skeleton.ToGlobal(skeleton.GetBoneGlobalPoseNoOverride(indexFingerTipBone).Origin);
		ThumbTipPosition = skeleton.ToGlobal(skeleton.GetBoneGlobalPoseNoOverride(thumbFingerTipBone).Origin);
	}
}
