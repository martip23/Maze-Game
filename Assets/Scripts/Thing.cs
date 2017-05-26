using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour {

	public float rotationSpeed = 80;

	public void setLocation (MazeCell cell)
	{
		transform.localPosition = cell.transform.localPosition;
	}

	void Update () 
	{
		transform.Rotate (Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
