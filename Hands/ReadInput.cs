using Godot;

public partial class ReadInput : Node3D
{
	[Export]
	private string thumbTipBoneName;
	[Export]
	private string indexTipBoneName;
	[Export]
	private string middleTipBoneName;
	[Export]
	private string ringTipBoneName;
	[Export]
	private string littleTipBoneName;

	Skeleton3D skeleton;

	int thumbFingerTipBone;
	int indexFingerTipBone;
	int middleFingerTipBone;
	int ringFingerTipBone;
	int littleFingerTipBone;

	public Vector3 ThumbTipPosition { get; private set; }
	public Vector3 IndexTipPosition { get; private set; }
	public Vector3 MiddleTipPosition { get; private set; }
	public Vector3 RingTipPosition { get; private set; }
	public Vector3 LittleTipPosition { get; private set; }

	public override void _Ready()
	{
		skeleton = FindChild("Skeleton3D", true) as Skeleton3D;

		thumbFingerTipBone = skeleton.FindBone(thumbTipBoneName);
		indexFingerTipBone = skeleton.FindBone(indexTipBoneName);
		middleFingerTipBone = skeleton.FindBone(middleTipBoneName);
		ringFingerTipBone = skeleton.FindBone(ringTipBoneName);
		littleFingerTipBone = skeleton.FindBone(littleTipBoneName);
	}

	public override void _Process(double delta)
	{
		ThumbTipPosition = skeleton.ToGlobal(skeleton.GetBoneGlobalPoseNoOverride(thumbFingerTipBone).Origin);
		IndexTipPosition = skeleton.ToGlobal(skeleton.GetBoneGlobalPoseNoOverride(indexFingerTipBone).Origin);
		MiddleTipPosition = skeleton.ToGlobal(skeleton.GetBoneGlobalPoseNoOverride(middleFingerTipBone).Origin);
		RingTipPosition = skeleton.ToGlobal(skeleton.GetBoneGlobalPoseNoOverride(ringFingerTipBone).Origin);
		LittleTipPosition = skeleton.ToGlobal(skeleton.GetBoneGlobalPoseNoOverride(littleFingerTipBone).Origin);
	}
}
