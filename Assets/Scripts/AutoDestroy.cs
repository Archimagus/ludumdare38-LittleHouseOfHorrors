using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	[SerializeField]
	float DestroyTime = 2.0f;
	
	// Update is called once per frame
	void Update ()
	{
		DestroyTime -= Time.deltaTime;
		if (DestroyTime <= 0)
			Destroy(gameObject);
	}
}
