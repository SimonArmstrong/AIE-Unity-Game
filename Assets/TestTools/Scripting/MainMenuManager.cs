using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public void PlayClick(){
		SceneManager.LoadScene (1);
	}
}
