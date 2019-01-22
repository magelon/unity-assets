using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatFade : MonoBehaviour {
    Text text;
    public float fadeDuration = 2.0f;
    public float speed = 2.0f;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        StartCoroutine(Fade());
	}

    public IEnumerator Fade() {
        float speed = (float)1.0 / fadeDuration;
        Color c = text.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * speed) {
            c.a = Mathf.Lerp(1, 0, t);
            text.color = c;
            yield return true;
        }
        Destroy(this.gameObject);

    }

	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
