extends OpenXRFbSceneManager

func _ready() -> void:
	openxr_fb_scene_data_missing.connect(_scene_data_missing)
	openxr_fb_scene_capture_completed.connect(_scene_capture_completed)
	
func _scene_data_missing() -> void:
	request_scene_capture()

func _scene_capture_completed(success: bool) -> void:
	if success == false:
		return

	if are_scene_anchors_created():
		remove_scene_anchors()

	create_scene_anchors()
