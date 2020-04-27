using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
    public GameObject r_BulletPrefab;

    private float angle=0f;
    private float shootInterval=0.4f;
    private float currentShootTime=0f;
    
    // Update is called once per frame
    void Update()
    {
        if (currentShootTime>=shootInterval){
            for (int i=0; i<=1; i++){
                float bulDirX = transform.position.x + Mathf.Sin(((angle + (180f*i))*Mathf.PI)/180f);
                float bulDirY = transform.position.y + Mathf.Cos(((angle + (180f*i))*Mathf.PI)/180f);

                Vector3 bulMoveVector = new Vector3(bulDirX,bulDirY,0f);
                Vector2 bulDir = (bulMoveVector-transform.position).normalized;
                //transform.rotation = Quaternion.Euler(Vector3.forward * angle);

                GameObject bullet = Instantiate(r_BulletPrefab, new Vector3(-7.01f,-4.84f,0f), transform.rotation);
                bullet.GetComponent<R_bullet>().SetMoveDirection(bulDir);
                //bullet.transform.position = transform.position;
                //bullet.transform.SetPositionAndRotation(new Vector3(-7.01f,-4.84f,5f),bullet.transform.rotation);
                //bullet.transform.rotation = transform.rotation;
            }
            angle+=10f;
            currentShootTime=0f;
        }
        currentShootTime+=Time.deltaTime;
        if (angle >= 360f){
            angle = 0f;
        }
    }
}
