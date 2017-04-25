using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler currentPooler;

    // Add prefabs of objects to be pooled
    // Add lists for each prefab to be pooled

    void Awake()
    {
        currentPooler = this;
    }

	void Start ()
    {
		
	}
	
    // Add functions to create the pools
    // Add functions to get an object from a pool that is available

}
