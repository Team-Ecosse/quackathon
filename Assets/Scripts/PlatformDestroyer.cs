using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

	private Transform destroyPoint;
	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update () {
		this.destroyPoint = GameObject.Find ("DestroyPoint").transform;
		if (this.transform.position.x < this.destroyPoint.position.x)
		{
			this.gameObject.SetActive (false);
		}
	}    
}
