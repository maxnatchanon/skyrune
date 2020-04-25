using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    public GameObject player;
    private float Duration = 0;
    public float speed;
    private Vector3 dir;
    private Transform playerpos;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
 	dir = player.transform.position - this.transform.position;
	dir = dir.normalized;
	playerpos = player.transform;
	
        //transform.rotation = Quaternion.LookRotation(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
	Duration+= Time.deltaTime;
        if (Duration >= 10){
            Destroy(this.gameObject);
}
        //transform.position += dir * speed * Time.deltaTime;
	//transform.position = Vector3.MoveTowards(transform.position ,playerpos.position, speed * Time.deltaTime);
	//transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision) {
	if (collision.collider.gameObject.name != "Sentinel"){
	Destroy(GetComponent<Rigidbody2D>());
	Destroy(GetComponent<BoxCollider2D>());
	StartCoroutine(Collision());
	    }	
	}
    IEnumerator Collision() {
		//animator.SetTrigger("Collide");
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
	}

}
