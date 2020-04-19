using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePuzzle : MonoBehaviour
{
    // Start is called before the first frame update

    public int puzzleId;
    public Sprite Fire;
    public Sprite Initial;
    public GameObject Path;
    public GameObject FirePOE;
    // public float startTimeCount = 5f;
    // float timeBtwCount;
    Transform player;
	PlayerHoverText playerText;
    int numOfEnabledFire = 0;
    public bool isCount = false;

    void Start() {
		player = GameObject.Find("Player").GetComponent<Transform>();
		playerText = GameObject.Find("Player").GetComponent<PlayerHoverText>();
        GameManager.instance.timeBtwCount = 10f;
	}

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.firePuzzle[puzzleId]){
        	//Change Sprite Image..
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = Fire;
        }
        else {
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = Initial;
        }

        checkKeyPress();
        print(GameManager.instance.timeBtwCount);
        if(isCount){
            if(GameManager.instance.timeBtwCount <= 0f && numOfEnabledFire > 0){
                for(int i = 0; i < GameManager.instance.firePuzzle.Length; i++)
                    GameManager.instance.firePuzzle[i] = false;
            }
            else if(numOfEnabledFire > 0){
                GameManager.instance.timeBtwCount -= Time.deltaTime;
            }
        }

        numOfEnabledFire = 0;
        for(int i = 0; i < GameManager.instance.firePuzzle.Length; i++)
		{
		    if (GameManager.instance.firePuzzle[i]) { numOfEnabledFire += 1;}
		}


        if(numOfEnabledFire > 0){
        	//Unlock Path
            FirePOE.transform.localScale = new Vector2((float)(6+numOfEnabledFire),(float)(6+numOfEnabledFire));
        	FirePOE.SetActive(true);
        }
        else {
        	FirePOE.SetActive(false);
        }
	}

	void checkKeyPress() {
		Vector3 tmp = transform.position;
    	float potionBottomY = transform.position.y - GetComponent<Renderer>().bounds.size.y / 2;
    	tmp.z = (player.transform.position.y <= potionBottomY + 0.7) ? 1 : -1;
    	transform.position = tmp;

    	float distance = Vector3.Distance(transform.position, player.transform.position);
    	if (distance <= 1.5f) {
    		playerText.SetText("(Press C to Interact)", 0.1f);
    		if (Input.GetKeyDown(KeyCode.C)) {
                GameManager.instance.firePuzzle[puzzleId] = !GameManager.instance.firePuzzle[puzzleId];
                if(GameManager.instance.firePuzzle[puzzleId]){
                    if(numOfEnabledFire == 0){
                         GameManager.instance.timeBtwCount = 10f;
                    }
                    else {
                         GameManager.instance.timeBtwCount += 10f;
                    }
                    
                }
    		}
    	}
	}
		    
}
