using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string levelToLoad = "MainLevel";

	public SceneFader sceneFader;

    private void Start()
    {
        //Start the menu music
        AkSoundEngine.PostEvent("MenuMusic", gameObject);
    }

    public void Play ()
	{
        //Stop menu music when player hits player
        AkSoundEngine.PostEvent("MenuMusicStop", gameObject);
        sceneFader.FadeTo(levelToLoad);
	}

	public void Quit ()
	{
		Debug.Log("Exiting...");
		Application.Quit();
	}

}
