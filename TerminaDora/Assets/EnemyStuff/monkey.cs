using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkey : MonoBehaviour
{
    float og_y; 
    Rigidbody2D rb;
    public float speed = 8;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
        og_y = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
    	rb = GetComponent<Rigidbody2D>();
        if (rb.position.y >= og_y + 1.5){
        	speed = -8;
        }
        if (rb.position.y <= og_y - 1.5){
        	speed = 8;
        }
        rb.velocity = Vector2.up * speed;
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
}
