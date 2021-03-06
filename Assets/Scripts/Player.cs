﻿using UnityEngine;

public class Player : MonoBehaviour {

	private MazeCell currentCell;
	private MazeDirection currentDirection;
	public bool paused;

	public void SetLocation (MazeCell cell)
	{
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}

	public MazeCell getLocation ()
	{
		return currentCell;
	}

	private void Move (MazeDirection direction)
	{
		MazeCellEdge edge = currentCell.GetEdge (direction);
		if (edge is MazePassage)
			SetLocation (edge.otherCell);
	}

	private void Rotate (MazeDirection direction) 
	{
		transform.localRotation = direction.ToRotation ();
		currentDirection = direction;
	}

	private void Update()
	{
		if (!paused) {
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))
				Move (currentDirection);
			else if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow))
				Move (currentDirection.GetNextCounterclockwise ());
			else if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow))
				Move (currentDirection.GetOpposite ());
			else if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow))
				Move (currentDirection.GetNextClockwise ());
			else if (Input.GetKeyDown (KeyCode.Q))
				Rotate (currentDirection.GetNextCounterclockwise ());
			else if (Input.GetKeyDown (KeyCode.E))
				Rotate (currentDirection.GetNextClockwise ());
		}
	}
}
