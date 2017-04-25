using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    private Camera _cameraComponent;
	[SerializeField]
	private float _moveTime = 0.5f;
	[SerializeField]
	private Vector3 _offset;
	public Vector3 TargetPosition { get; set; }
	private Vector3 _velocity;

    public float MaxZoomOut = 1;
    public float MaxZoomIn = 0.25f;

    private float _newSize;

	// Use this for initialization
	void Start ()
	{
		GameManager.TheCamera = this;
        _cameraComponent = this.GetComponent<Camera>();
        _newSize = _cameraComponent.orthographicSize;
    }
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.SmoothDamp(transform.position, TargetPosition+_offset, ref _velocity, _moveTime);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && _cameraComponent.orthographicSize > MaxZoomIn + 0.1f)
        {
            ZoomIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && _cameraComponent.orthographicSize < MaxZoomOut - 0.1f)
        {
            ZoomOut();
        }

        if(_cameraComponent.orthographicSize != _newSize)
        {
            _cameraComponent.orthographicSize = Mathf.Lerp(_cameraComponent.orthographicSize, _newSize, _moveTime);
        }
    }

    private void ZoomIn()
    {
        _newSize -= 0.1f;
    }

    private void ZoomOut()
    {
        _newSize += 0.1f;
    }
}
