extends StaticBody3D

@onready var label: Label3D = $Label3D

@export var material: Material

func setup_scene(entity: OpenXRFbSpatialEntity) -> void:
	var semantic_labels: PackedStringArray = entity.get_semantic_labels()

	# label.text = semantic_labels[0]

	#if semantic_labels[0] != "table":
	return

	var collision_shape = entity.create_collision_shape()
	if collision_shape:
		add_child(collision_shape)

	var mesh_instance = entity.create_mesh_instance()
	if mesh_instance:
		material.resource_local_to_scene = true

		var rand = RandomNumberGenerator.new()
		material.set("albedo_color", Color(rand.randf(), rand.randf(), rand.randf(), 0))
		
		mesh_instance.material_override = material
		add_child(mesh_instance)
