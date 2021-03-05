using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoraMouse : MonoBehaviour
{
    float pspeed = 10f;
    float pspeed1 = 10f;
    float pspeed2 = 20f;
    float pspeed3 = 30f;
    float switcher = 1f;
    float offset = 270f;
    float bulletcooldown = .3f;
    float switchcooldown = .25f;
    public GameObject bullet;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    float velocity = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Sprite gunsprite;
    public Sprite bowsprite;
    public Sprite rpgsprite;
    public Sprite rrpgsprite;
    private bool isInCooldown = false;
    private bool isInSwitchCooldown = false;
    float armx = .36f;
    float army = -.4f;
    float keys = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        rb.velocity = new Vector2(0, 0);
        float iy = Input.GetAxis("Vertical");
        float ix = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        { //0.01745 is the degree to radian conversion constant. also transform.position should be something else to make it shoot from gun
            if(!isInCooldown){
                GameObject instBullet = Instantiate(bullet, 
                                                    new Vector2 (transform.position.x + armx * Mathf.Cos(angle * 0.01745f) + army * Mathf.Cos(angle * 0.01745f - 90*0.01745f), 
                                                                 transform.position.y + armx * Mathf.Sin(angle * 0.01745f) + army * Mathf.Sin(angle * 0.01745f - 90*0.01745f)), 
                                                    Quaternion.AngleAxis(angle + offset, Vector3.forward));
                instBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle*0.01745f)*pspeed, Mathf.Sin(angle*0.01745f)*pspeed);
                Physics2D.IgnoreCollision(instBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                Destroy(instBullet, 5f);
                Invoke("ResetCooldown", bulletcooldown);
                isInCooldown = true;
            }
            
        }
        if(Input.GetKey(KeyCode.E)) //switch weapons
        {
            if(!isInSwitchCooldown)
            {
                switch(switcher % 3)
                {
                case 0: //gun dora
                    bullet = bullet1; 
                    pspeed = pspeed1; 
                    offset = 270f;
                    sr.sprite = gunsprite;
                    bulletcooldown = .3f;
                    switcher++; 
                    break; 
                case 1: //arrow dora
                    bullet = bullet2; 
                    pspeed = pspeed2; 
                    offset = 135f;
                    sr.sprite = bowsprite;
                    bulletcooldown = 1f;
                    switcher++; 
                    break;
                case 2: //rpg dora
                    bullet = bullet3; 
                    pspeed = pspeed3; 
                    offset = 180f;
                    sr.sprite = rpgsprite;
                    bulletcooldown = 2f;
                    switcher++; 
                    break;
                default: 
                    Debug.Log("ERROR");
                    break; 

                }
                Invoke("ResetSwitchCooldown", switchcooldown);
                isInSwitchCooldown = true;
            }
            
        }
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);    
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);    
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, velocity);    
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocity);    
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Reset();
        }
        if(isInCooldown && switcher % 3 == 0)
        {
            sr.sprite = rrpgsprite;
        }
        if(!isInCooldown && switcher % 3 == 0)
        {
            sr.sprite = rpgsprite;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Key")){
            Debug.Log("collected key");
            Destroy(col.gameObject);
            keys++;
        }
        if (col.gameObject.tag.Equals("door")){
            if(keys == 5)
            {
                SceneManager.LoadScene("SwiperBattle");
            }
        }
    }
    #region quicksand code
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<quicksand>())
        {
            Debug.Log("hitting");
            velocity = 1f;
            Debug.Log(velocity);
        }
        
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<quicksand>())
        {
            Debug.Log("hitting");
            velocity = 5f;
            Debug.Log(velocity);
        }
    }
    
    #endregion
    private void ResetCooldown () {
        isInCooldown = false;
    }
    private void ResetSwitchCooldown () {
        isInSwitchCooldown = false;
    }

    private void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

