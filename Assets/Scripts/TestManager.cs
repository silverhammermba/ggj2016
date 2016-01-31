using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestManager : MonoBehaviour
{

	public const string NativeLang = "en";
	public const string LearningLang = "en"; //zh, ja, en

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

		//TODO scramble this word order again

		foreach (Word w in tests.WordBank.Values) {
			CreateNoun (w);
		}

	}

	public void NextSentence()
	{
		if (sentenceIndex == tests.Challenges.Count) {
			//the player wins!
			Debug.Log("you win!");
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
		currentSentence.Setup(c.Phrases[NativeLang], c.Answer.langs["en"], c.Animation);
		st.transform.SetParent(topBar);
		st.transform.localPosition = new Vector3(0f, -20f);


		//Starting the thinking bubble
		PlayerControl pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		Vector3 vector = (Vector3)(GameObject.FindWithTag ("Player").transform.position + new Vector3 (0.25f,0.3f, 0));
		pc.thinking(c.Answer.langs["en"], vector);
	}

	// create a noun button from the hardcoded list
	void CreateNoun(Word w)
	{
		GameObject nn = Instantiate(noun, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		//key, foreign, native
		nn.GetComponentInChildren<Noun>().Setup(w.langs["en"], w.langs[LearningLang], w.langs[NativeLang], bottomBar);
	}
		 
}
