using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public void startGame(){
		//Change scene by one.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void quitGame(){
		Debug.Log("Exiting Application");
		Application.Quit();
	}
}
