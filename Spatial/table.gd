extends Node3D

@export var game_field: PackedScene

func setup_scene(entity: OpenXRFbSpatialEntity) -> void:
	var instance = game_field.instantiate() as Node3D
	add_child(instance)
	instance.Initialize(entity.get_bounding_box_3d())
