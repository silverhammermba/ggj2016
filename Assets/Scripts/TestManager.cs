using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TestManager : MonoBehaviour
{
	
	public GameObject sentence;
	public GameObject noun;

	/// player
	public GameObject player;

	public RectTransform topBar;
	public RectTransform bottomBar;

	int sentenceIndex = 0;

	Sentence currentSentence = null;

	Situation tests;

	public TestManager(){
		tests = LevelLoader.loadup ();
	}

	void Start()
	{
		NextSentence();

		Word[] words = new Word[tests.WordBank.Values.Count];
		int j = 0;
		foreach (Word w in tests.WordBank.Values) {
			words[j] = w;
			j++;
		}

		// scramble the word order

		int num = words.Length;

		// decide a random order to add nouns
		int[] indices = new int[num];
		for (int i = 0; i < num; ++i) indices[i] = i;
		for (int i = 0; i < num - 1; ++i)
		{
			int swp = Random.Range(i, num);
			int tmp = indices[swp];
			indices[swp] = indices[i];
			indices[i] = tmp;
		}

		// add nouns
		for (int i = 0; i < num; ++i)
		{
			CreateNoun(words[indices[i]]);
		}


	}

	public void NextSentence()
	{
		if (sentenceIndex == tests.Challenges.Count) {
			//the player wins!
			Debug.Log("you win!");
			GameStateManager.instance.StartLevel ("TitleScreen");
			return;
		}
		if (currentSentence != null)
		{
			GameObject.Destroy(currentSentence.gameObject);
		}
		CreateSentence(sentenceIndex++);
	}

	// create a sentence from the hardcoded list
	void CreateSentence(int i)
	{
		GameObject st = Instantiate(sentence, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		currentSentence = st.GetComponent<Sentence>();
		Challenge c = tests.Challenges [i];
		currentSentence.Setup(c.Phrases[GameStateManager.NativeLang], c.Answer.key, c.Animation);
		st.transform.SetParent(topBar);
		st.transform.localPosition = new Vector3(0f, -20f);


		//Starting the thinking bubble
		PlayerControl pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		Vector3 vector = (Vector3)(GameObject.FindWithTag ("Player").transform.position + new Vector3 (0.25f,0.3f, 0));
		pc.thinking(c.Answer.key, vector);
	}

	// create a noun button from the hardcoded list
	void CreateNoun(Word w)
	{
		GameObject nn = Instantiate(noun, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		//key, foreign, native
		nn.GetComponentInChildren<Noun>().Setup(w.key, w.langs[GameStateManager.LearningLang], w.langs[GameStateManager.NativeLang], bottomBar);
	}
		 
}
