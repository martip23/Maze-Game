using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBeacon : MonoBehaviour {

	public float rotationSpeed;



	void Update () 
	{
		transform.Rotate (Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
