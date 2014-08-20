using UnityEngine;
using System.Collections;

public class IntroTextScroller : MonoBehaviour {
	string story = @"It is year 32XX. A huge population growth
has led to a situation where economic
and social gaps have grown huge.
Different kind of urban gladiator
tournaments are being held for the
amusement of wealthier citizens.



In these tournaments the poorest of the
society are fighting with their mechanical
combat suits, trying to win their way out of
poverty. For the champions of the arena
eternal wealth and glory awaits.



After losing most of his friends and
relatives for these barbaric feasts,
a young candidate from city slum is
participating in one of the most infamous 
tournament's of them all…










";
	
	TextMesh textMesh;
	float speed = 1;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
		textMesh.alignment = TextAlignment.Center;

		Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));

		float width = -(vec.x * 2);

		textMesh.transform.position = new Vector3(0, vec.y, 0);
		//textMesh.text = story;
		SetText(story, width);

		AudioClip clip = GetComponent<AudioSource>().clip;
		float length = clip.length;
		float size = textMesh.renderer.bounds.size.y - 2 * vec.y;

		speed = size / length;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<AudioSource>().isPlaying) {
			Application.LoadLevel("MainMenu");
		}

		textMesh.transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
	}

	void SetText(string text, float width)
	{
		string result = "";

		string[] words = text.Split(' ');

		for (int i = 0; i < words.Length; i++) {
			textMesh.text += words[i] + " ";

			if(renderer.bounds.size.x > width) {
				result += "\n";
				textMesh.text = "";
			}

			result += words[i] + " ";
		}

		textMesh.text = result;
	}
}
