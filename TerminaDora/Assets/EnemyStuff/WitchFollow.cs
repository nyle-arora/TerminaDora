using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchFollow : MonoBehaviour
{
    public float speed = 1;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= 10.0f){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D c){
        if (c.gameObject.tag.Equals("Bullet3")){
            Destroy(c.gameObject);
            Destroy(gameObject);
        }
        else if (c.gameObject.tag.Equals("Bullet1") || c.gameObject.tag.Equals("Bullet2") || c.gameObject.tag.Equals("SwiperBullet"))
        {
            Destroy(c.gameObject);
        }
        else
        {
            //do nothing. another script handles this
        }
    }
}
