using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public static GameController Instance{
        get{
            return instance;
        }
    }
    private static float health = 6;
    private static int maxHealth = 6;
    public static int playerBulletStrength = 1;

    private static int gracePeriod = 3;
    private static bool isGrace = false;
    private static float moveSpeed = 10f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;
    private static float bulletMultiple = 1;
    private static float bulletLifetime = 3;
    private static float bulletSpeed = 5f;
    private static int spawnFamiliar = 0;
    public static float Health{get =>health; set=>health = value;}
    public static int MaxHealth {get=>maxHealth; set=>maxHealth=value;}
    public static float MoveSpeed{get =>moveSpeed; set=>moveSpeed = value;}
    public static float FireRate {get=>fireRate; set=>fireRate=value;}
    public static float BulletSize {get=>bulletSize; set=>bulletSize=value;}

    public static float BulletMultiple {get => bulletMultiple; set=>bulletMultiple = value;}

    public static float BulletLifetime {get => bulletLifetime; set=>bulletLifetime = value;}
    public static float BulletSpeed{get => bulletSpeed; set=>bulletSpeed = value;}
    public static int SpawnFamiliar{get => spawnFamiliar; set=>spawnFamiliar = value;}

    public Text healthText;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage){
        if(!isGrace){
            health -= damage;
        
            GameController.Instance.Do(StaticIEnumerator());
            if(Health <= 0){
                KillPlayer();
            }
        }
    }

    

    public void Do(IEnumerator _ienumerator){
        StartCoroutine(_ienumerator);
    }

    static IEnumerator StaticIEnumerator(){
        isGrace = true;
        yield return new WaitForSeconds(gracePeriod);
        isGrace = false;
    }

    public static void MoveSpeedChange(float speed){
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate){
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size){
        bulletSize += size;
    }
    public static void BulletStrengthChange(int strength){
        playerBulletStrength += strength;
    }
    public static void HealPlayer(float healAmount){
        health = Mathf.Min(maxHealth, health + healAmount);
    }
    public static void BulletMultipleChange(int level){
        bulletMultiple += level;
    }
    public static void BulletLifetimeChange(float time){
        bulletLifetime += time;
    }
    public static void BulletSpeedChange(float speed){
        bulletSpeed += speed;
    }
    public static void SpawnFamiliarChange(int num){
        spawnFamiliar += num;
    }
    private static void KillPlayer(){

    }
}
