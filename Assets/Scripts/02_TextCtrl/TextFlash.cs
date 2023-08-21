using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlash : MonoBehaviour
{
	public TMP_Text mText;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(BlinkText());
	}

	public IEnumerator BlinkText()
	{
		while (true)
		{
			mText.text = "";
			yield return new WaitForSeconds(.5f);
			mText.text = "PRESS TO START";
			yield return new WaitForSeconds(.5f);
		}
	}
}
