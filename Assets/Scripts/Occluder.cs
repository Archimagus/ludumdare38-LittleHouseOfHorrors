using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occluder : MonoBehaviour
{

    private GameObject _objectToBeOccluded;
    private GameObject _theCamera;

    private float _occlusionDistance = 4.15f;
    public float Distance;

    public MeshRenderer[] Renderers;
    public Light[] Lights;

    private void Awake()
    {
        _objectToBeOccluded = this.gameObject;

        Renderers = _objectToBeOccluded.GetComponentsInChildren<MeshRenderer>();
        Lights = _objectToBeOccluded.GetComponentsInChildren<Light>();
    }

    void Start ()
    {
        _objectToBeOccluded = this.gameObject;
        _theCamera = GameObject.Find("Main Camera");
	}
	
	void Update ()
    {
        if(_objectToBeOccluded != null && _objectToBeOccluded.activeInHierarchy)
        {
            Distance = Vector3.Distance(_objectToBeOccluded.transform.position, _theCamera.transform.position);

            if (Distance > _occlusionDistance)
            {
                foreach (MeshRenderer renderer in Renderers)
                {
                    renderer.enabled = false;
                }

                foreach (Light light in Lights)
                {
                    light.enabled = false;
                }
            }
            else
            {
                foreach (MeshRenderer renderer in Renderers)
                {
                    renderer.enabled = true;
                }

                foreach (Light light in Lights)
                {
                    if(light.gameObject.GetComponent<RoomLight>().RoomWasExplored)
                    {
                        light.enabled = true;
                    }
                }
            }
        }
        
	}
}
