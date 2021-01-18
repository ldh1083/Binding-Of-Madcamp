using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
        public GameObject ParticleFXExplosion;

    public float lifetime;
    public bool isEnemyBullet = false;

    public bool isBossBullet = false;

    public bool isFamiliarBullet = false;
    private int bulletStrength;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    private const float moveSpeed = 100f;
    //총알이 움직일 속도를 상수로 지정해줍시다.
    void Start () {
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * moveSpeed);
        bulletStrength = GameController.playerBulletStrength;
        StartCoroutine(DeathDelay());
        if(!isEnemyBullet && !isBossBullet)
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
            if(isFamiliarBullet)
            {
            transform.localScale = new Vector2(GameController.BulletSize*3, GameController.BulletSize*3);
            }
        }

        //Destroy(gameObject, 5);
    }
    void Update () {
        if(isEnemyBullet){
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f*Time.deltaTime);
            if(curPos == lastPos){
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
        /*
        float moveY = moveSpeed * Time.deltaTime;
       //이동할 거리를 지정해 줍시다.
       if(transform.rotation.y==0){
       transform.Translate(transform.right*moveSpeed*Time.deltaTime);
       }
       /*else if(transform.rotation.y==180){
           print(transform.right);
       transform.Translate(transform.right*moveSpeed*Time.deltaTime);
       }
       else if(transform.rotation.y==90)
       transform.Translate(transform.up*moveSpeed*Time.deltaTime);
       else if(transform.rotation.y==270)
       transform.Translate(transform.up*-1*moveSpeed*Time.deltaTime);*/
        //transform.Translate(0, moveY, 0);
        //이동을 반영해줍시다.
    }

    public void GetPlayer(Transform player){
        playerPos = player.position;
    }
        void OnTriggerEnter2D(Collider2D other)
    {
          if(other.tag.Equals("Enemy") && (!isEnemyBullet && !isBossBullet)){ //부딪힌 객체가 적인지 검사합니다.
              //Destroy(other.gameObject); //부딪힌 적을 지웁니다.
              other.gameObject.GetComponent<enemy2>().DamageEnemy(bulletStrength);
              print("one");
              Destroy(this.gameObject); //자기 자신을 지웁니다.
              print("two");
          }

          if(other.tag.Equals("Player") && (isEnemyBullet || isBossBullet)){
              GameController.DamagePlayer(1);
              Destroy(gameObject);

          }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }


    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public void SetBulletLifetime(float time){
        lifetime = time;
    }

}
