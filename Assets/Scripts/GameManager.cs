using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;

	public Player playerPrefab;
	private Player playerInstance;

	public IntVector2 startLocation;
	public IntVector2 endLocation;

	// Use this for initialization
	private void Start () 
	{
		BeginGame();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space))
			RestartGame ();
	}


	private void BeginGame () 
	{
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect (0f, 0f, 1f, 1f);
		mazeInstance = Instantiate (mazePrefab) as Maze;
		mazeInstance.Generate ();
		playerInstance = Instantiate (playerPrefab) as Player;
		playerInstance.SetLocation (mazeInstance.GetCell (startLocation));
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect (0f, 0f, 0.5f, 0.5f);
	}

	private void RestartGame () 
	{
		StopAllCoroutines ();
		Destroy (mazeInstance.gameObject);
		if (playerInstance != null)
		{
			Destroy (playerInstance.gameObject);
		}
		BeginGame();
	}
}

