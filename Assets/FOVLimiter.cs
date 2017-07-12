using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class FOVLimiter : MonoBehaviour {
	private Vector3 oldPosition;
	public float MaxSpeed = 6f;
	public float MaxFOV = .7f;

	public static float CRate = .01f;
	public static float RateCutOff = .25f;

	// max .7 Vignetting

	private VignetteAndChromaticAberration fovLimiter;
	// Use this for initialization
	void Start () {
		oldPosition = transform.position;
		fovLimiter = GetComponent<VignetteAndChromaticAberration> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity = (transform.position - oldPosition) / Time.deltaTime;
		oldPosition = transform.position;

		float expectedLimit = MaxFOV;
		if (velocity.magnitude < MaxSpeed) {
			expectedLimit = (velocity.magnitude / MaxSpeed) * MaxFOV;
		}

		float currLimit = fovLimiter.intensity;
		float rate = CRate;

		if (currLimit < RateCutOff) {
			rate *= 3; //fast rate since the field of view is large and fast changes are less noticeable
		} else {
			rate *= .5f; //slower rate since the field of view changes are more noticable for larger values. 
		}

		fovLimiter.intensity = Mathf.Lerp (fovLimiter.intensity, expectedLimit, rate);
	}
}
