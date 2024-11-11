using Godot;
using System;

public partial class RandomColor : OmniLight3D
{
	public override void _Ready()
	{
		var components = new byte[3];
		new Random().NextBytes(components);
		LightColor = Color.Color8(components[0], components[1], components[2]);
	}
}
