using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour 
{	
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
	public IntVector2 coordinates;	
	public MazeRoom room;
	private int initializedEdgeCount;

	public bool IsFullyInitialized
	{
		get{
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

	public MazeCellEdge GetEdge (MazeDirection direction)
	{
		return edges [(int)direction];
	}
	
	public void SetEdge (MazeDirection direction, MazeCellEdge edge)
	{
		edges [(int)direction] = edge;
		initializedEdgeCount++;
	}

	public MazeDirection RandomUninitializedDirection 
	{
		get{
			int skips = Random.Range (0, MazeDirections.Count - initializedEdgeCount);
			for (int i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0) {
						return (MazeDirection)i;
					}
					skips--;
				}
			}
			throw new System.InvalidOperationException ("MazeCell has no unititalized" +
			" directions left.");
		}
	}

	public void Initialize (MazeRoom room)
	{
		room.Add (this);
		transform.GetChild (0).GetComponent<Renderer> ().material = 
			room.settings.floorMaterial;


	}
}
