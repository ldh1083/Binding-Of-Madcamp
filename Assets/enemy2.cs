using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType{
    Melee,
    Ranged,

    Boss1
};

public class enemy2 : MonoBehaviour
{
    // Start is called before the first frame update
GameObject player;
public EnemyState currState = EnemyState.Wander;
public EnemyType enemyType;

public GameObject ParticleFXExplosion;

public int enemyHealth;
public int maxHealth;
public float range;
public float speed;
public float attackRange;
public float coolDown; 
private bool chooseDir = false;
private bool dead = false;
private bool coolDownAttack = false;

public GameObject bulletPrefab;
private Vector3 randomDir;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = enemyHealth;
        if(enemyType == EnemyType.Boss1){
        StartCoroutine(SpellStart());
        }
    }

    // Update is called once per frame
    void Update()
    {
     switch(currState){
         case(EnemyState.Wander):
         Wander();
         break;
         case(EnemyState.Follow):
         Follow();
         break;
         case(EnemyState.Die):
         break;
         case(EnemyState.Attack):
         Attack();
         break;
     }   

     if(IsPlayerInRange(range) && currState != EnemyState.Die){
         currState = EnemyState.Follow;
     }
     else if(!IsPlayerInRange(range) && currState != EnemyState.Die){
         currState = EnemyState.Wander;
     }
     if(Vector3.Distance(transform.position, player.transform.position) <= attackRange){
         currState = EnemyState.Attack;
     }
    }

    private bool IsPlayerInRange(float range){
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

private IEnumerator ChooseDirection(){
    chooseDir = true;
    yield return new WaitForSeconds(Random.Range(2f, 8f));
    randomDir = new Vector3(0,0,Random.Range(0,360));
    Quaternion nextRotation = Quaternion.Euler(randomDir);
    transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
    chooseDir = false;
}

    void Wander(){
        if(!chooseDir){
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if(IsPlayerInRange(range)){
            currState = EnemyState.Follow;
        }
    }


    void Follow(){

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    void Attack(){
        if(!coolDownAttack){
            switch(enemyType){
                case(EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                break;
                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<bullet>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<bullet>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                break;
                case(EnemyType.Boss1):
                    GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet2.GetComponent<bullet>().GetPlayer(player.transform);
                    bullet2.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet2.GetComponent<bullet>().isEnemyBullet = true;
                    // GameObject bullet3;
                    
                    // for(int i = 0; i < 50; i++){
                    //     bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                    //     bullet3.AddComponent<Rigidbody2D>().gravityScale = 0;
                    //     bullet3.GetComponent<Rigidbody2D>().velocity = new Vector3(1-(2/49)*i,(2/49)*i,0);    
                    //     print(1-(2/49)*i);
                    // }
                    // for(int i = 0; i < 50; i++){
                    //     bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                    //     bullet3.AddComponent<Rigidbody2D>().gravityScale = 0;
                    //     bullet3.GetComponent<Rigidbody2D>().velocity = new Vector3(-1+(2/49)*i,-(2/49)*i,0);    
                    // }

                    StartCoroutine(CoolDown());
                    
                break;
            }
        }
        
    }

IEnumerator SpellStart() 
{ 
do{
float oneShoting = 50f;
float angle = 360 / oneShoting; 

for(int i =0;i<oneShoting;i++) 
{
    //Debug.Log(i); 
GameObject obj; 
obj=(GameObject)Instantiate(bulletPrefab , transform.position, Quaternion.identity); 
//보스의 위치에 bullet을 생성합니다.
obj.AddComponent<Rigidbody2D>().gravityScale = 0;
obj.GetComponent<bullet>().isBossBullet = true;
obj.GetComponent<Rigidbody2D>().AddForce( new Vector2(500f*Mathf.Cos(Mathf.PI*2*i/oneShoting),500f*Mathf.Sin(Mathf.PI*i*2/oneShoting)));
//Debug.Log(speed*Mathf.Cos(Mathf.PI*2*i/oneShoting));
obj.transform.Rotate(new Vector3(0f,0f,360*i/oneShoting -90)); 
}
yield return new WaitForSeconds(10f); 
} while(true);



//지정해둔 각도의 방향으로 모든 총탄을 날리고, 날아가는 방향으로 방향회전을 해줍니다.
} 



private IEnumerator CoolDown(){
    coolDownAttack = true;
    yield return new WaitForSeconds(coolDown);
    coolDownAttack = false;
}

    public void Death(){
        Destroy(gameObject);
    }

    public void DamageEnemy(int damage){
        print(damage);
        enemyHealth -= damage;

        if(enemyHealth <= 0){
            Death();
            Instantiate(ParticleFXExplosion, this.transform.position, Quaternion.identity); //폭발 이펙트를 생성합니다.
        }

    }
}
