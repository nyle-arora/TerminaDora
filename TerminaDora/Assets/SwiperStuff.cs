using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiperStuff : MonoBehaviour
{
    public Transform target;
    float pspeed = 7f;
    float pspeed1 = 7f;
    float pspeed2 = 10f;
    public GameObject bullet;
    public GameObject bullet1;
    public GameObject bullet2;
    float switchcooldown = 1f;
    float velocity = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    int weaponchoice = 0;
    public Sprite shotgunsprite;
    public Sprite mgsprite;
    private bool isInSwitchCooldown = false;
    private bool isInCooldown = false;
    float health = 50f;
    float angle=0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health < 1)
        {
            Destroy(gameObject);
        }
        //rotate to face dora
        Quaternion rotation = Quaternion.LookRotation
             (target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
         transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        angle=transform.eulerAngles.z; 
        //weapon decision phase, if can switch weapon
        if (!isInSwitchCooldown)
        {
            weaponchoice = Random.Range(0,2);
            if(weaponchoice == 0)
            {
                //shotgun
                bullet = bullet1; 
                pspeed = pspeed1; 
                sr.sprite = shotgunsprite;
                sr.color = Color.blue;
            }
            else
            {
                bullet = bullet2; 
                pspeed = pspeed2;
                sr.sprite = mgsprite;
                sr.color = Color.red;
            }
            Invoke("ResetSwitchCooldown", switchcooldown);
                isInSwitchCooldown = true;
        }
        float dist = Vector3.Distance(target.position, transform.position);
        //see if able to shoot
        if (!isInCooldown && dist < 12)
        {
            //shoot
            if(weaponchoice ==0)
            {
                for(int i = 0; i <6; i++)
                {
                    GameObject instBullet = Instantiate(bullet, 
                                                    new Vector2(transform.position.x + .1f * Mathf.Cos((angle + (i-3)*30) * 0.01745f), 
                                                                transform.position.y + .1f * Mathf.Sin((angle + (i-3)*30) * 0.01745f)),
                                                    Quaternion.AngleAxis(0, Vector3.forward));
                    instBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((angle + (i-3)*30)*0.01745f)*pspeed, 
                                                                                  Mathf.Sin((angle + (i-3)*30)*0.01745f)*pspeed);
                    Physics2D.IgnoreCollision(instBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    Destroy(instBullet, 3f);
                }
                

                Invoke("ResetCooldown", 1.5f);
                isInCooldown = true;
            }
            if(weaponchoice ==1)
            {
                GameObject instBullet = Instantiate(bullet, 
                                                    transform.position,
                                                    Quaternion.AngleAxis(0, Vector3.forward));
                instBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle*0.01745f)*pspeed, Mathf.Sin(angle*0.01745f)*pspeed);
                Physics2D.IgnoreCollision(instBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                Destroy(instBullet, 3f);
                Invoke("ResetCooldown", .4f);
                isInCooldown = true;
            }


        }
    }
    void OnCollisionEnter2D(Collision2D c){
        if (c.gameObject.tag.Equals("Bullet1")){
            Destroy(c.gameObject);
            health -= 1f;
        }
        else if (c.gameObject.tag.Equals("Bullet2"))
        {
            Destroy(c.gameObject);
            health -= 5f;
        }
        else if (c.gameObject.tag.Equals("Bullet3"))
        {
            Destroy(c.gameObject);
            health -= 15f;
        }
        else
        {
            //do nothing. another script handles this
        }
    }
    private void ResetCooldown () {
        isInCooldown = false;
    }
    private void ResetSwitchCooldown () {
        isInSwitchCooldown = false;
    }
}
