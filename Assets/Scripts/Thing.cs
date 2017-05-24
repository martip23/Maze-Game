using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour {

	public void setLocation (MazeCell cell)
	{
		transform.localPosition = cell.transform.localPosition;
	}


}
