using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
	public float minSize = 5;
	public float maxSize = 10;
	public float multiplier = 5f;
	public Transform followTransform;
	private Rigidbody2D transformBody;
	private Camera cam;

	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	Vector3 originalPos = new Vector3(0,0,0);

	public Transform camTransform;

	void AssignMissingReferences()
	{
		transformBody = transformBody == null ? followTransform.gameObject.GetComponent<Rigidbody2D>() : transformBody;

		cam = cam == null ? GetComponentInChildren<Camera>() : cam;
	}


	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}

	void LateUpdate ()
	{
		AssignMissingReferences();
		this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, -10f);

		cam.orthographicSize = Mathf.Clamp(transformBody.velocity.magnitude * multiplier, minSize, maxSize);
	}
}
