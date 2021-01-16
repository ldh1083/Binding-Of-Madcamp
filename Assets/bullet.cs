using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
        public GameObject ParticleFXExplosion;

    public float lifetime;
    private const float moveSpeed = 100f;
    //총알이 움직일 속도를 상수로 지정해줍시다.
    void Start () {
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * moveSpeed);
        StartCoroutine(DeathDelay());
        //Destroy(gameObject, 5);
    }
    void Update () {
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

        void OnTriggerEnter2D(Collider2D other)
    {
          if(other.tag.Equals("Enemy")){ //부딪힌 객체가 적인지 검사합니다.
              //Destroy(other.gameObject); //부딪힌 적을 지웁니다.
              other.gameObject.GetComponent<enemy2>().Death();
              Destroy(this.gameObject); //자기 자신을 지웁니다.
              Instantiate(ParticleFXExplosion, this.transform.position, Quaternion.identity); //폭발 이펙트를 생성합니다.
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
}
