using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour {

	public const string English = "en";
	public const string Chinese = "zh";
	public const string Japanese = "ja";

	public static string NativeLang = English;
	public static string LearningLang = Chinese;


	//Singleton...
	public static GameStateManager instance;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Singleton code copied from example...

		//Check if instance already exists
		if (instance == null)
		{
			//if not, set instance to this
			instance = this;
		}
		//If instance already exists and it's not this:
		else if (instance != this)
		{
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);
		}
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void StartLevel(string name){
		Debug.Log ("starting level: "+name);
		//TODO only start if language selected

		SceneManager.LoadScene (name);
	}
}
