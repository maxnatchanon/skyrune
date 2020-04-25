using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkRain : MonoBehaviour {
	//public Animator animator;
	//SpriteRenderer.size = 1;
	private float lifeTime = 6f;
	private float timer = 0f;
	private float intervalmove = 0f;
	private float random1;
	void Start(){
	random1 = Random.Range(0.8F,2F);
	transform.localScale = new Vector3(random1, random1, random1);
}

	void Update() {
		timer += Time.deltaTime;
		if (timer >= lifeTime) {
			Destroy(gameObject);
		}
		intervalmove++;
		if (intervalmove >= 15){
		intervalmove = 0;
		transform.position -= new Vector3(1,1,0);
		}
	}

	
}
