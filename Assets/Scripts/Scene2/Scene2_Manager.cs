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

    private float bossMaxHp = 100;
    private float bossHp = 100;
    private GameObject RunePickUp;

    // Start is called before the first frame update
    void Start()
    {
        boss.SetActive(false);
        hpBar.SetActive(false);
        bunker.SetActive(false);
        RunePickUp = GameObject.Find("/YellowRunePickup");
        RunePickUp.GetComponent<RunePickup>().Unlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss.active &&  GameManager.instance.pickedRune == Rune.Yellow && player.transform.position.x > -15.0f){
            boss.SetActive(true);
            hpBar.SetActive(true);
            bunker.SetActive(true);
        }
        if (boss.active){
            float health = (float)bossHp / (float)bossMaxHp;
            bar.transform.localScale = new Vector3(health, 1f, 1f);
        }
        if (buttonR.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
            Button_R.SetBool("R_trigger",true);
        }
        if (buttonP.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
            Button_P.SetBool("P_trigger",true);
        }
        if (buttonB.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
            Button_B.SetBool("B_trigger",true);
        }
        if (buttonG.GetComponent<Renderer>().bounds.Intersects(player.GetComponent<Renderer>().bounds)){
            Button_G.SetBool("G_trigger",true);
        }
    }
}
