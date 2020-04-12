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
    Transform player;
	PlayerHoverText playerText;
    bool allEnabled = true;

    void Start() {
		player = GameObject.Find("Player").GetComponent<Transform>();
		playerText = GameObject.Find("Player").GetComponent<PlayerHoverText>();
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

        allEnabled = true;
        for(int i = 0; i < GameManager.instance.firePuzzle.Length; i++)
		{
		    if (!GameManager.instance.firePuzzle[i]) { allEnabled = false; break;}
		}


        if(allEnabled){
        	//Unlock Path
        	Path.SetActive(false);
        }
        else {
        	Path.SetActive(true);
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
    			if(puzzleId == 0){
    				GameManager.instance.firePuzzle[2] = !GameManager.instance.firePuzzle[2];
    			}
    			else if(puzzleId == 1){
    				GameManager.instance.firePuzzle[0] = !GameManager.instance.firePuzzle[0];
    				GameManager.instance.firePuzzle[2] = !GameManager.instance.firePuzzle[2];
    			}
    			else if(puzzleId == 2){
    				GameManager.instance.firePuzzle[1] = !GameManager.instance.firePuzzle[1];
    			}
    			else if(puzzleId == 3){
    				GameManager.instance.firePuzzle[0] = !GameManager.instance.firePuzzle[0];
    				GameManager.instance.firePuzzle[1] = !GameManager.instance.firePuzzle[1];
    			}

                GameManager.instance.firePuzzle[puzzleId] = !GameManager.instance.firePuzzle[puzzleId];
    		}
    	}
	}
		    
}
