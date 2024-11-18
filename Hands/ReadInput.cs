using Godot;

public partial class ReadInput : Skeleton3D
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
		thumbFingerTipBone = FindBone(thumbTipBoneName);
		indexFingerTipBone = FindBone(indexTipBoneName);
		middleFingerTipBone = FindBone(middleTipBoneName);
		ringFingerTipBone = FindBone(ringTipBoneName);
		littleFingerTipBone = FindBone(littleTipBoneName);
	}

	public override void _Process(double delta)
	{
		ThumbTipPosition = ToGlobal(GetBoneGlobalPoseNoOverride(thumbFingerTipBone).Origin);
		IndexTipPosition = ToGlobal(GetBoneGlobalPoseNoOverride(indexFingerTipBone).Origin);
		MiddleTipPosition = ToGlobal(GetBoneGlobalPoseNoOverride(middleFingerTipBone).Origin);
		RingTipPosition = ToGlobal(GetBoneGlobalPoseNoOverride(ringFingerTipBone).Origin);
		LittleTipPosition = ToGlobal(GetBoneGlobalPoseNoOverride(littleFingerTipBone).Origin);
	}
}
