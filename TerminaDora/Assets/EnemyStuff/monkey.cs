using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkey : MonoBehaviour
{
    private Transform target;

    public GameObject bullet;
    float pspeed = 7f;
    private bool isInCooldown = false;
    AudioSource AS;
    public AudioClip mg;
    float angle=0;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rotation = Quaternion.LookRotation
             (target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
         
        angle=transform.eulerAngles.z; 
        float dist = Vector3.Distance(target.position, transform.position);
    	if (!isInCooldown && dist < 12)
        {
            
                AS.PlayOneShot(mg, 1F);
                for(int i = 0; i <8; i++)
                {
                    GameObject instBullet = Instantiate(bullet, 
                                                    new Vector2(transform.position.x + .1f * Mathf.Cos((angle + (i-3)*45) * 0.01745f), 
                                                                transform.position.y + .1f * Mathf.Sin((angle + (i-3)*45) * 0.01745f)),
                                                    Quaternion.AngleAxis(0, Vector3.forward));
                    instBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((angle + (i-3)*45)*0.01745f)*pspeed, 
                                                                                  Mathf.Sin((angle + (i-3)*45)*0.01745f)*pspeed);
                    Physics2D.IgnoreCollision(instBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    Destroy(instBullet, 3f);
                }
                

                Invoke("ResetCooldown", 2f);
                isInCooldown = true;
        }
    }
    void OnCollisionEnter2D(Collision2D c){
        if (c.gameObject.tag.Equals("Bullet1")){
            Destroy(c.gameObject);
            Destroy(gameObject);
        }
        else if (c.gameObject.tag.Equals("Bullet2") || c.gameObject.tag.Equals("Bullet3")|| c.gameObject.tag.Equals("SwiperBullet"))
        {
            Destroy(c.gameObject);
        }
        else
        {
            //do nothing. another script handles this
        }
    }
    private void ResetCooldown () {
        isInCooldown = false;
    }
}
