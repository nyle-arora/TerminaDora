using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    int life; 
    bool canbehurtbylava = true;
    // Start is called before the first frame update
    void Start()
    {
        life = hearts.Count; 
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0){
            GameOverScript.endGame();
        }

        float xOffset = 0.0f;
        for (int i = 0; i < hearts.Count; i++){
            Vector3 temp = hearts[i].transform.position;
            temp.y = transform.position.y + 6.5f; 
            temp.x = transform.position.x + 6.8f + xOffset; 
            hearts[i].transform.position = temp;
            xOffset = xOffset + 0.7f;
        }
    }

    void OnCollisionEnter2D(Collision2D c){
        if (c.gameObject.tag.Equals("enemy")){
            Destroy(c.gameObject);
            EnemyDamage();
        }
        if (c.gameObject.tag.Equals("SwiperBullet")){
            Destroy(c.gameObject);
            EnemyDamage();
        }
        if (c.gameObject.tag.Equals("swiper")){
            EnemyDamage();
        }
        if (c.gameObject.tag.Equals("healthy")){
            Destroy(c.gameObject);
            Heal();
        }
        /*
        if (c.gameObject.tag.Equals("healthy")){
            Destroy(c.gameObject);
            Heal();
        }
        */
    }

    void EnemyDamage(){
        if (life > 0){
            GameObject h = hearts[life-1].gameObject; 
            hearts.RemoveAt(life-1);
            Destroy(h);
            life = life - 1; 
            //Destroy(hearts[life-1].gameObject);
        }
    }

    public void LavaDamage(){
        if(canbehurtbylava)
        {
            if (life > 0){
            GameObject h = hearts[life-1].gameObject; 
            hearts.RemoveAt(life-1);
            Destroy(h);
            life = life - 1; 
            Invoke("ResetCooldown", 1f);
                canbehurtbylava = false;
        }
        }
        
    }
    private void ResetCooldown () {
        canbehurtbylava = true;
    }
    //uhhh figure out later
    
    void Heal(){
        if (life < 3){
            Vector3 posn = hearts[life-1].transform.position;
            posn.x = posn.x + 0.7f; 
            GameObject newHeart = Instantiate(hearts[0], posn, Quaternion.identity);
            hearts.Add(newHeart);
            life = life + 1; 
        }
    }
    
}
