using UnityEngine;

public class MatchCameraRotation : MonoBehaviour
{
	private Transform _mainCamera;

	private void Awake()
	{
		_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	void Update()
	{
		transform.rotation = _mainCamera.rotation;
	}
}
