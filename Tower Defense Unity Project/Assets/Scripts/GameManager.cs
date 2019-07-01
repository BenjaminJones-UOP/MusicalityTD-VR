using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool GameIsOver;

	public GameObject gameOverUI;
	public GameObject completeLevelUI;
    public GameObject crosshairUI;

	void Awake ()
	{
		GameIsOver = false;
	}

	// Update is called once per frame
	void Update () {
		if (GameIsOver)
			return;

		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
	}

    //Stop the music when the player wins or gets a game over
	void EndGame ()
	{
		GameIsOver = true;
		gameOverUI.SetActive(true); //game over ui here
        crosshairUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        AkSoundEngine.PostEvent("Level2_Stop", gameObject);
        AkSoundEngine.PostEvent("Level1_Stop", gameObject);
    }

	public void WinLevel ()
	{
		GameIsOver = true;
		completeLevelUI.SetActive(true); //complete level ui here
        crosshairUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        AkSoundEngine.PostEvent("Level2_Stop", gameObject);
        AkSoundEngine.PostEvent("Level1_Stop", gameObject);
    }

    public bool GetGameIsOver()
    {
        bool gameOver = GameIsOver;
        return gameOver;
    }

}
