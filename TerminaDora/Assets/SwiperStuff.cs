using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SwiperStuff : MonoBehaviour
{
    List<GameObject> squares = new List<GameObject>();

    public Transform target;
    float pspeed = 7f;
    float pspeed1 = 7f;
    float pspeed2 = 10f;
    public GameObject bullet;
    public GameObject bullet1;
    public GameObject bullet2;
    float switchcooldown = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    int weaponchoice = 0;
    public Sprite shotgunsprite;
    public Sprite mgsprite;
    private bool isInSwitchCooldown = false;
    private bool isInCooldown = false;
    float health = 50f;
    float angle=0;
    public GameObject hs; 

    AudioSource AS;
    public AudioClip mg;
    public AudioClip sg;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        AS = GetComponent<AudioSource>();
        for (int i = 0; i < 50; i++){
            GameObject hp = Instantiate(hs, new Vector2(transform.position.x+0.1f * (i - 26), 
                                        transform.position.y + 3f), 
                                        Quaternion.AngleAxis(0, Vector3.forward));
            squares.Add(hp);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health < 1)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("DoraWins");
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
                //sr.color = Color.blue;
            }
            else
            {
                bullet = bullet2; 
                pspeed = pspeed2;
                sr.sprite = mgsprite;
                //sr.color = Color.red;
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
                AS.PlayOneShot(sg, 1F);
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
                

                Invoke("ResetCooldown", 1.6f);
                isInCooldown = true;
            }
            if(weaponchoice ==1)
            {
                AS.PlayOneShot(mg, 1F);
                GameObject instBullet = Instantiate(bullet, 
                                                    transform.position,
                                                    Quaternion.AngleAxis(0, Vector3.forward));
                instBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle*0.01745f)*pspeed, Mathf.Sin(angle*0.01745f)*pspeed);
                Physics2D.IgnoreCollision(instBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                Destroy(instBullet, 3f);
                Invoke("ResetCooldown", .6f);
                isInCooldown = true;
            }


        }
    }
    void OnCollisionEnter2D(Collision2D c){
        if (c.gameObject.tag.Equals("Bullet1")){
            Destroy(c.gameObject);
            health -= 1f;
            if(health <= 0)
            {
                SceneManager.LoadScene("DoraWins");
            }
            for (int i = 0; i < 1; i++){
                if(squares.Count <= 0)
                {   SceneManager.LoadScene("DoraWins");
                    break;}
                GameObject sq = squares[squares.Count-1].gameObject;
                squares.RemoveAt(squares.Count-1);
                Destroy(sq);
            }
        }
        else if (c.gameObject.tag.Equals("Bullet2"))
        {
            Destroy(c.gameObject);
            health -= 5f;
            if(health <= 0)
            {
                SceneManager.LoadScene("DoraWins");
            }
            for (int i = 0; i < 5; i++){
                if(squares.Count <= 0)
                {   SceneManager.LoadScene("DoraWins");
                    break;}
                GameObject sq = squares[squares.Count-1].gameObject;
                squares.RemoveAt(squares.Count-1);
                Destroy(sq);
            }
        }
        else if (c.gameObject.tag.Equals("Bullet3"))
        {
            Destroy(c.gameObject);
            health -= 1f;
            if(health <= 0)
            {
                DoraMouse.keys = 0;
                SceneManager.LoadScene("DoraWins");
            }
            for (int i = 0; i < 12; i++){
                if(squares.Count <= 0)
                {   DoraMouse.keys = 0;
                    SceneManager.LoadScene("DoraWins");
                    break;}
                GameObject sq = squares[squares.Count-1].gameObject;
                squares.RemoveAt(squares.Count-1);
                Destroy(sq);
            }
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

    void healthBar(){

    }
}
