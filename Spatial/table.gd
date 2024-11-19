extends Node3D

@export var material: Material
@export var game_field: PackedScene

func setup_scene(entity: OpenXRFbSpatialEntity) -> void:
	var mesh_instance = entity.create_mesh_instance()
	if mesh_instance:
		mesh_instance.material_override = material
		add_child(mesh_instance)

	var instance = game_field.instantiate() as Node3D
	add_child(instance)
	instance.Initialize(entity.get_bounding_box_3d())
