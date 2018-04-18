using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	//public GameObject _GM; 

	public void startGame(){
		float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
		delay(fadeTime);
		//Change scene by one.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private IEnumerable delay(float time){
		yield return new WaitForSeconds(time);
	}

	public void quitGame(){
		Debug.Log("Exiting Application");
		Application.Quit();
	}

	public void backScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
	}

	public void changeModuleGlobalwarming(){

	}
}
