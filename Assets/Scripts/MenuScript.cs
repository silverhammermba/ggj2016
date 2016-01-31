using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setBaseLangZh(){
		GameStateManager.NativeLang = GameStateManager.Chinese;
	}

	public void setBaseLangJa(){
		GameStateManager.NativeLang = GameStateManager.Japanese;
	}

	public void setBaseLangEn(){
		GameStateManager.NativeLang = GameStateManager.English;
	}

	public void setTargetLangZh(){
		GameStateManager.LearningLang = GameStateManager.Chinese;
	}

	public void setTargetLangJa(){
		GameStateManager.LearningLang = GameStateManager.Japanese;
	}

	public void setTargetLangEn(){
		GameStateManager.LearningLang = GameStateManager.English;
	}

	public void StartFirstLevel(){
		GameStateManager.instance.StartLevel ("Morning");
	}
}
