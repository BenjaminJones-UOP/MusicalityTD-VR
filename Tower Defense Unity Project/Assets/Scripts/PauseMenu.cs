using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;
    public GameObject crosshairUI;

	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	public void Toggle ()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
            Cursor.lockState = CursorLockMode.None;
            crosshairUI.SetActive(false);
			Time.timeScale = 0f;
		} else
		{
            Cursor.lockState = CursorLockMode.Locked;
            crosshairUI.SetActive(true);
			Time.timeScale = 1f;
		}
	}

    //Stop the menus when the player hits Retry or Menu so that the music doesn't double up
	public void Retry ()
	{
		Toggle();
        AkSoundEngine.PostEvent("Level1_Stop", gameObject);
        AkSoundEngine.PostEvent("Level2_Stop", gameObject);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
		Toggle();
        Cursor.lockState = CursorLockMode.None;
        AkSoundEngine.PostEvent("Level1_Stop", gameObject);
        AkSoundEngine.PostEvent("Level2_Stop", gameObject);
        sceneFader.FadeTo(menuSceneName);
	}

}
