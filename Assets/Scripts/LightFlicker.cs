using UnityEngine;
using System.Collections;

[AddComponentMenu("Dungeon/Generic/Light Flicker")]
public class LightFlicker : MonoBehaviour
{
	public float MinIntensity;
	public float MaxIntensity;
	public float FlickerDelay;
	public float updateRate= 0.01f;
	//float baseRange;
	float targetIntensity;
	float flickerVelocity;
	float changeTime = 0;
	Light _light;
	WaitForSeconds updateWait;
	// Use this for initialization
	void Start () 
	{
		//baseRange = light.range;
		_light = GetComponent<Light>();
		updateWait = new WaitForSeconds(updateRate);
		StartCoroutine(UpdateLight());
	}
	
	// Update is called once per frame
	IEnumerator UpdateLight () 
	{
		while (true)
		{
			if (Time.time - changeTime > FlickerDelay)
			{
				targetIntensity = Random.Range(MinIntensity, MaxIntensity);
				changeTime = Time.time;
			}
			_light.intensity = Mathf.SmoothDamp(_light.intensity, targetIntensity, ref flickerVelocity, FlickerDelay);
			//light.range = baseRange * (light.intensity/MaxIntensity);
			yield return updateWait;
		}
	}
}
