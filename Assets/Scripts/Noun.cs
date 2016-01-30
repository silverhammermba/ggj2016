using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Noun : MonoBehaviour
{
	bool dragging = false;
	public string english;

	Transform canvas;
	RectTransform parent;
	AudioSource asrc;
	AudioClip clip;

	void Start()
	{
		canvas = GameObject.FindWithTag("canvas").transform;
		asrc = GetComponent<AudioSource>();
		clip = Resources.Load("Audio/" + english) as AudioClip;
		if (clip == null)
			Debug.Log("Failed to load Audio/" + english);
		asrc.clip = clip;
	}

	public void Setup(string zh, string en, RectTransform grid)
	{
		parent = grid;

		transform.GetChild(0).gameObject.GetComponent<Text>().text = zh;
		english = en;
		transform.SetParent(parent);
	}

	void Update ()
	{
		if (dragging)
		{
			transform.position = Input.mousePosition;
		}
	}

	public void followMouse()
	{
		transform.SetParent(canvas);
		dragging = true;
	}

	public void backToGrid()
	{
		dragging = false;
		transform.SetParent(parent);

		Sentence sent = GameObject.FindWithTag("sentence").GetComponent<Sentence>();
		sent.FillIn(gameObject);
	}

	public void Describe()
	{
		if (dragging) return;
		asrc.Play();
	}
}
