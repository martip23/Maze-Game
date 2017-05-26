using UnityEngine;
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
	private GameObject mainMenuInstance;

	public GameObject howToPlayPrefab;
	private GameObject howToInstance;

	public GameObject victoryScreenPrefab;
	private GameObject victoryScreenInstance;



	// Use this for initialization
	private void Start () 
	{
		///*CREATE PAUSE MENU*///
		pauseMenuInstance = Instantiate (pauseMenuPrefab) as GameObject;
		pauseMenuInstance.SetActive (false);
		mainMenuInstance = Instantiate (mainMenuPrefab) as GameObject;
		assignPauseMenuButtons ();
		assignMainMenuButtons ();

	}
	
	// Update is called once per frame
	private void Update () 
	{
		if (Input.GetKeyDown (KeyCode.P))
			PauseGame ();
		if (playerInstance){
			if (playerInstance.getLocation() == mazeInstance.GetCell
				(mazeInstance.endLocation))
			{
				RestartGame ();
				victory ();
			}			
		}

	}


	private void BeginGame () 
	{
		mainMenuInstance.SetActive (false);

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
		if (mazeInstance){
			StopAllCoroutines ();
			destroyChildren (mazeInstance);
			Destroy (mazeInstance.gameObject);
			if (playerInstance != null)	{
				Destroy (playerInstance.gameObject);
			}
			if (pauseMenuInstance.activeSelf){
				PauseGame ();
			}
			BeginGame();
			}
		else {
			BeginGame ();
		}

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

	private void assignPauseMenuButtons ()
	{
		Button[] pauseButtons;
		pauseButtons = pauseMenuInstance.GetComponentsInChildren<Button>();
		pauseButtons[0].onClick.AddListener (PauseGame);
		pauseButtons [1].onClick.AddListener (RestartGame);
		pauseButtons [2].onClick.AddListener (MainMenuToggle);
	}

	private void assignMainMenuButtons ()
	{
		Button[] menuButtons;
		menuButtons = mainMenuInstance.GetComponentsInChildren<Button>();
		menuButtons[0].onClick.AddListener (RestartGame);
		menuButtons [1].onClick.AddListener (HowToPlay);
	}

	private void MainMenuToggle ()
	{
		if (mainMenuInstance.activeSelf){
			mainMenuInstance.SetActive (false);
		} else {
			mainMenuInstance.SetActive (true);
		}
	}

	private void destroyChildren(Maze parent)
	{
		foreach(Transform child in transform)
		{
			Destroy (child.gameObject);
		}
	}

	private void HowToPlay()
	{
		Button exitButton;
		howToInstance = Instantiate (howToPlayPrefab) as GameObject;
		exitButton = howToInstance.GetComponentInChildren<Button> ();
		exitButton.onClick.AddListener (exitHowToPlay);
	}

	private void exitHowToPlay()
	{
		Destroy (howToInstance);
	}

	private void victory()
	{
		Button exitButton;
		victoryScreenInstance = Instantiate (victoryScreenPrefab) as GameObject;
		exitButton = victoryScreenInstance.GetComponentInChildren<Button> ();
		exitButton.onClick.AddListener (exitVictoryScreen);
	}

	private void exitVictoryScreen()
	{
		Destroy (victoryScreenInstance);
	}
}