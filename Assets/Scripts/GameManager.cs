﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;

	public Player playerPrefab;
	private Player playerInstance;

	public IntVector2 startLocation;
	public IntVector2 endLocation;

	public GameObject pauseMenuPrefab;
	private GameObject pauseMenuInstance;

	public GameObject mainMenuPrefab;

	// Use this for initialization
	private void Start () 
	{
		///*CREATE PAUSE MENU*///
		pauseMenuInstance = Instantiate (pauseMenuPrefab) as GameObject;
		pauseMenuInstance.SetActive (false);
		assignMenuButtons ();

		BeginGame();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if (Input.GetKeyDown (KeyCode.P))
			PauseGame ();
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
		if (pauseMenuInstance.activeSelf){
			PauseGame ();
		}
		BeginGame();
	}

	public void PauseGame ()
	{
					// If paused, unpause
		if (pauseMenuInstance.activeSelf) {
			pauseMenuInstance.SetActive (false);
			Time.timeScale = 1;
			playerInstance.paused = false;
		} else {	// If unpaused, pause
			pauseMenuInstance.SetActive (true);
			Time.timeScale = 0;
			playerInstance.paused = true;
		}
	}

	private void assignMenuButtons ()
	{
		Button[] buttons;
		buttons = pauseMenuInstance.GetComponentsInChildren<Button>();
		buttons[0].onClick.AddListener (PauseGame);
		buttons [1].onClick.AddListener (RestartGame);
		buttons [2].onClick.AddListener (MainMenu);
	}

	private void MainMenu ()
	{
		
	}
}