using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private Transform _player;
	[SerializeField]
	private AudioClip []_footStepSoudns;

	public bool playerIsMoving = false;
	public float MovementSpeed = 0.1f;

	[SerializeField]
	private Vector3 _moveToLocation;
	private Room _targetRoom;

	private Animator _animation;
	public Player Player { get; set; }

	void Awake()
	{
		_player = gameObject.GetComponent<Transform>();
		Player = GetComponent<Player>();
		_animation = GetComponent<Animator>();
	}
	
	void Update()
	{
		if(Input.GetButtonDown("Jump"))
		{
			GameManager.TheCamera.TargetPosition = Player.CurrentRoom.transform.position;
		}
		if (playerIsMoving)
		{
			Move();
		}
	}

	public void PlayFootstep()
	{
		AudioManager.PlaySound(_footStepSoudns[Random.Range(0, _footStepSoudns.Length)], AudioType.Effect, parent:gameObject);
	}

	// Call this when you want to move the player to a new tile
	// and pass in the landing point of the new tile as the destination
	public void MovePlayerToTile(Room destination)
	{
		_targetRoom = destination;

		_moveToLocation = destination.transform.position;
		playerIsMoving = true;
		_animation.SetBool("Moving", true);
	}

	// This handles the physical movement of the player from the current position
	// to the destination. Also does bounce effect while moving
	private void Move()
	{
		float step = MovementSpeed * Time.deltaTime;
		_player.position = Vector3.MoveTowards(_player.position, _moveToLocation, step);
		

		if (_player.position == _moveToLocation)
		{
			Vector3 stoppingPoing = new Vector3(_moveToLocation.x, _moveToLocation.y, _moveToLocation.z);
			_player.position = stoppingPoing;
			playerIsMoving = false;
			_animation.SetBool("Moving", false);
			_targetRoom.Explore(Player);
		}
		else
		{
			_player.rotation = Quaternion.LookRotation(_moveToLocation - transform.position);
		}
	}
}
