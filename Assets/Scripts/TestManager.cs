using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour
{
	public GameObject sentence;
	public GameObject noun;

	string[,] tests = new string[2, 4]
	{
		{"Tom brushes his", "teeth", ".", "\u7259\u9f7f"},
		{"Tom eats a big", "breakfast", ".", "\u65e9\u9910"}
	};

	void Start()
	{
		Transform canvas = GameObject.FindWithTag("canvas").transform;

		// create a sentence
		GameObject st = Instantiate(sentence, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		st.GetComponent<Sentence>().Setup(tests[0, 0], tests[0, 1], tests[0, 2]);
		st.transform.SetParent(canvas);

		// and a noun button
		// TODO add this button to a list that is scrambled up
		GameObject nn = Instantiate(noun, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		nn.GetComponentInChildren<Noun>().Setup(tests[0, 3], tests[0, 1]);
		nn.transform.SetParent(canvas);

		// TODO actually place them both in the correct positions
	}
}
