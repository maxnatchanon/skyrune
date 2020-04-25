using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_Manager : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    public GameObject hpBar;
    public RectTransform bar;
    public GameObject bunker;

    public Animator Button_R;
    public Animator Button_P;
    public Animator Button_B;
    public Animator Button_G;

    public GameObject buttonR;
    public GameObject buttonP;
    public GameObject buttonB;
    public GameObject buttonG;

    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;

    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    public GameObject b4;

    private float bossMaxHp;
    private float bossHp;
    private GameObject RunePickUp;

    // Start is called before the first frame update
    void Start()
    {
        boss.SetActive(true);
        hpBar.SetActive(true);
        bunker.SetActive(false);
        SetS(false);

        RunePickUp = GameObject.Find("/YellowRunePickup");
        RunePickUp.GetComponent<RunePickup>().Unlock();

        bossHp = boss.GetComponent<MiniBoss_2>().bossHp2;
        bossMaxHp = boss.GetComponent<MiniBoss_2>().bossMaxHp2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss.active &&  GameManager.instance.pickedRune == Rune.Yellow && player.transform.position.x > -15.0f && bossHp==bossMaxHp){
            boss.SetActive(true);
            hpBar.SetActive(true);
            bunker.SetActive(true);
            Button_R.SetBool("R_trigger",false);
            Button_P.SetBool("P_trigger",false);
            Button_B.SetBool("B_trigger",false);
            Button_G.SetBool("G_trigger",false);
            b1.SetActive(true);
            b2.SetActive(true);
            b3.SetActive(true);
            b4.SetActive(true);
        }
        if (boss.active){
            float health = (float)bossHp / (float)bossMaxHp;
            bar.transform.localScale = new Vector3(health, 1f, 1f);
        }
        if (buttonR.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && !Button_R.GetBool("R_trigger")){
            Button_R.SetBool("R_trigger",true);
            SetS(true,false,false,false);
            b1.GetComponent<Renderer>().enabled = false;
        }
        if (buttonP.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && !Button_P.GetBool("P_trigger")){
            Button_P.SetBool("P_trigger",true);
            SetS(false,true,false,false);
            b2.GetComponent<Renderer>().enabled = false;
        }
        if (buttonB.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && !Button_B.GetBool("B_trigger")){
            Button_B.SetBool("B_trigger",true);
            SetS(false,false,true,false);
            b3.GetComponent<Renderer>().enabled = false;
        }
        if (buttonG.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds) && !Button_G.GetBool("G_trigger")){
            Button_G.SetBool("G_trigger",true);
            SetS(false,false,false,true);
            b4.GetComponent<Renderer>().enabled = false;
        }
        if (boss.active){
            bossHp = boss.GetComponent<MiniBoss_2>().bossHp2;
            bossMaxHp = boss.GetComponent<MiniBoss_2>().bossMaxHp2;
            float health = (float)bossHp / (float)bossMaxHp;
            bar.transform.localScale = new Vector3(health, 1f, 1f);
        }
    }

    void SetS(bool x1,bool x2,bool x3,bool x4){
        s1.SetActive(x1);
        s2.SetActive(x2);
        s3.SetActive(x3);
        s4.SetActive(x4);
    }

    void SetS(bool x){
        s1.SetActive(x);
        s2.SetActive(x);
        s3.SetActive(x);
        s4.SetActive(x);
    }

    public void TurnOnButton(){
        Button_R.SetBool("R_trigger",false);
        Button_P.SetBool("P_trigger",false);
        Button_B.SetBool("B_trigger",false);
        Button_G.SetBool("G_trigger",false);
    }

    public void Clear(){
        boss.SetActive(false);
        hpBar.SetActive(false);
        bunker.SetActive(false);
        Button_R.SetBool("R_trigger",true);
        Button_P.SetBool("P_trigger",true);
        Button_B.SetBool("B_trigger",true);
        Button_G.SetBool("G_trigger",true);
    }
}
