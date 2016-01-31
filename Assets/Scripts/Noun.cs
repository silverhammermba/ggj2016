using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Noun : MonoBehaviour
{
	bool dragging = false;
	public string key;
	public string native;

	Transform canvas;
	RectTransform parent;
	AudioSource asrc;
	AudioClip clip;

	void Start()
	{
		canvas = GameObject.FindWithTag("canvas").transform;
		asrc = GetComponent<AudioSource>();
		string audioPath = "Audio/" + GameStateManager.LearningLang + "_" + key;
		clip = Resources.Load(audioPath) as AudioClip;
		if (clip == null)
			Debug.Log("Failed to load " + audioPath);
		asrc.clip = clip;
	}

	public void Setup(string key, string foreign, string native, RectTransform grid)
	{
		this.key = key;
		parent = grid;

		transform.GetChild(0).gameObject.GetComponent<Text>().text = foreign;
		this.native = native;
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
