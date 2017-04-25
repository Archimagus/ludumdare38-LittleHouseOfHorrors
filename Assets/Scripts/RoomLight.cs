using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RoomLight : MonoBehaviour
{
	private Room _room;
	public bool RoomWasExplored = false;

	private Light _light;
	private Material []_materials;
	private Color[] _emissive;
	void Start ()
	{
		_room = GetComponentInParent<Room>();
		_light = this.GetComponent<Light>();
		_materials = GetComponentsInChildren<MeshRenderer>().SelectMany(r => r.materials).ToArray();
		_emissive = new Color[_materials.Length];
		for (int i = 0; i < _materials.Length; i++)
		{
			_emissive[i] = _materials[i].GetColor("_EmissionColor");
			_materials[i].SetColor("_EmissionColor", Color.black);
		}
		if (_light != null)
			_light.enabled = false;
	}
	
	void Update () {
		if(_room.Explored && !RoomWasExplored)
		{
			ActivateRoomLight();
		}
	}

	public void ActivateRoomLight()
	{
		RoomWasExplored = true;
		if (_light != null)
			_light.enabled = true;
		for (int i = 0; i < _materials.Length; i++)
		{
			_materials[i].SetColor("_EmissionColor", _emissive[i]);
		}
	}
}
