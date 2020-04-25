using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss_2 : MonoBehaviour
{
    public GameObject scenemanager;

    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    public GameObject b4;

    public float bossMaxHp2 = 400;
    public float bossHp2 = 400;

    private int state = 1; //1-8

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        print(collision.gameObject.name);
        if (!(b1.GetComponent<Renderer>().enabled || b2.GetComponent<Renderer>().enabled || b3.GetComponent<Renderer>().enabled || b4.GetComponent<Renderer>().enabled)){
            if (collision.gameObject.name == "Fireball(Clone)"){
                bossHp2 = bossHp2 - GameManager.instance.fireballPower;
            }else if (collision.gameObject.name == "Sword(Clone)"){
                bossHp2 = bossHp2 - GameManager.instance.swordPower;
            }else if (collision.gameObject.name == "Player"){
                GameManager.instance.ReduceHealth(10);
            }
            print(bossHp2);
            print(state);
            if (bossHp2 <= 350 && state==1){
                state = 2;
                TurnOnBall();
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 300 && state==2){
                state = 3;
                TurnOnBall();
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 250 && state==3){
                state = 4;
                TurnOnBall();
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 200 && state==4){
                state = 5;
                TurnOnBall();
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 150 && state==5){
                state = 6;
                TurnOnBall();
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 100 && state==6){
                state = 7;
                TurnOnBall();
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 50 && state==7){
                state = 8;
                TurnOnBall();
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 0){
                scenemanager.GetComponent<Scene2_Manager>().Clear();
            }
        }
    }

    void TurnOnBall(){
        b1.GetComponent<Renderer>().enabled = true;
        b2.GetComponent<Renderer>().enabled = true;
        b3.GetComponent<Renderer>().enabled = true;
        b4.GetComponent<Renderer>().enabled = true;
    }
}
