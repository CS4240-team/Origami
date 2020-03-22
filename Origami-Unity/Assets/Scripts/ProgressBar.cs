using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

	private GameObject barFill;
	public float speed;

	void Start()
	{
		barFill = transform.GetChild(0).GetChild(0).gameObject;
		speed = 0;
	}

	void Update()
	{
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.left);
		//transform.localScale = Vector3.one * (Vector3.Distance (Camera.main.transform.position, transform.position) * 0.1f);
		if (speed > 0 && barFill.transform.localScale.x < 1)
		{
			barFill.transform.localScale += new Vector3(0.01f * speed, 0, 0);
		}
	}

	public void fillBar(float spd)
	{
		speed = spd;
	}

	public void enlargeCanvas(bool val)
	{
		if (val)
			transform.localScale = new Vector3(1, 1, 1);
		else
		{
			transform.localScale = new Vector3(0, 0, 0.0001f);
			barFill.transform.localScale = new Vector3(0, 1, 1);
		}
	}
}
