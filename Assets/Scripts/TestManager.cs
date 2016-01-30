using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour
{
	public GameObject sentence;
	public GameObject noun;

	public RectTransform topBar;
	public RectTransform bottomBar;

	string[,] tests = new string[2, 4]
	{
		{"Tom brushes his", "teeth", ".", "\u7259\u9f7f"},
		{"Tom eats a big", "breakfast", ".", "\u65e9\u9910"}
	};

	void Start()
	{
		CreateSentence(0);

		for (int i = 0; i < tests.GetLength(1); ++i)
		{
			CreateNoun(i);
		}
	}

	void CreateSentence(int i)
	{
		// create a sentence
		GameObject st = Instantiate(sentence, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		st.GetComponent<Sentence>().Setup(tests[i, 0], tests[i, 1], tests[i, 2]);
		st.transform.SetParent(topBar);
		st.transform.localPosition = new Vector3(0f, -50f);
	}

	void CreateNoun(int i)
	{
		// and a noun button
		// TODO add this button to a list that is scrambled up
		GameObject nn = Instantiate(noun, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		nn.GetComponentInChildren<Noun>().Setup(tests[i, 3], tests[i, 1], bottomBar);
	}
}
