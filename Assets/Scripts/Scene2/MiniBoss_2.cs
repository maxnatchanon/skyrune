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

    public GameObject potion_prefab;

    public Transform potion1;
    public Transform potion2;
    public Transform potion3;
    public Transform potion4;

    public float bossMaxHp2 = 400;
    public float bossHp2 = 400;

    private int state = 1; //1-8
    private bool trigger = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger){
            switch (state){
                case 1:
                    ActiveColor(GetRandomColor(1));
                    break;
                case 2:
                    ActiveColor(GetRandomColor(1));
                    break;
                case 3:
                    ActiveColor(GetRandomColor(2));
                    break;
                case 4:
                    ActiveColor(GetRandomColor(2));
                    break;
                case 5:
                    ActiveColor(GetRandomColor(3));
                    break;
                case 6:
                    ActiveColor(GetRandomColor(3));
                    break;
                case 7:
                    ActiveColor(GetRandomColor(4));
                    break;
                case 8:
                    ActiveColor(GetRandomColor(4));
                    break;
            }
            trigger = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        print(collision.gameObject.name);
        if (!(b1.GetComponent<Renderer>().enabled || b2.GetComponent<Renderer>().enabled || b3.GetComponent<Renderer>().enabled || b4.GetComponent<Renderer>().enabled)){
            if (collision.gameObject.name == "Fireball(Clone)" ){
                bossHp2 = bossHp2 - GameManager.instance.fireballPower;
            }else if (collision.gameObject.name == "Sword(Clone)"){
                bossHp2 = bossHp2 - GameManager.instance.swordPower;
            }else if (collision.gameObject.name == "Player"){
                GameManager.instance.ReduceHealth(10);
            }
            if (bossHp2<0){
                bossHp2=0;
            }
            if (bossHp2 <= 350 && state==1){
                state = 2;
                TurnOnBall();
                trigger = true;
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 300 && state==2){
                state = 3;
                TurnOnBall();
                trigger = true;
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 250 && state==3){
                state = 4;
                TurnOnBall();
                trigger = true;
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 200 && state==4){
                state = 5;
                TurnOnBall();
                trigger = true;
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
                GameObject potion = Instantiate(potion_prefab, GetRandomPotion().position , Quaternion.identity);
            }else if (bossHp2 <= 150 && state==5){
                state = 6;
                TurnOnBall();
                trigger = true;
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
            }else if (bossHp2 <= 100 && state==6){
                state = 7;
                TurnOnBall();
                trigger = true;
                scenemanager.GetComponent<Scene2_Manager>().TurnOnButton();
                GameObject potion = Instantiate(potion_prefab, GetRandomPotion().position , Quaternion.identity);
            }else if (bossHp2 <= 50 && state==7){
                state = 8;
                TurnOnBall();
                trigger = true;
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

    void ActiveColor(List<bool> x){
        scenemanager.GetComponent<Scene2_Manager>().red.SetActive(x[0]);
        scenemanager.GetComponent<Scene2_Manager>().purple.SetActive(x[1]);
        scenemanager.GetComponent<Scene2_Manager>().blue.SetActive(x[2]);
        scenemanager.GetComponent<Scene2_Manager>().green.SetActive(x[3]);
    }

    List<bool> GetRandomColor(int x){
        List<bool> l;
        if (x==1){
            l = new List<bool>{false,false,false,false};
            l[Random.Range(0,4)]=true;
            return l;
        }else if(x==2){
            int i1=0;
            int i2=0;
            l = new List<bool>{false,false,false,false};
            while(i1==i2){
                i1 = Random.Range(0,4);
                i2 = Random.Range(0,4);
            }
            l[i1]=true;
            l[i2]=true;
            return l;
        }else if(x==3){
            l = new List<bool>{true,true,true,true};
            l[Random.Range(0,4)]=false;
            return l;
        }else{
            return new List<bool>{true,true,true,true};
        }
    }

    Transform GetRandomPotion(){
        int x = Random.Range(0,4);
        if (x==0){
            return potion1;
        }else if(x==1){
            return potion2;
        }else if(x==2){
            return potion3;
        }else{
            return potion4;
        }
    }
}
