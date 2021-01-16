using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour {
    public bool enableSpawn = true;
    public GameObject Enemy; //Prefab을 받을 public 변수 입니다.
    void SpawnEnemy()
    {
        float randomX = Random.Range(-5f, 5f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
        if (enableSpawn)
        {
            GameObject enemy =(GameObject)Instantiate(Enemy, new Vector3(randomX,3f, 0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
        }
    }
    void Start () {
        InvokeRepeating("SpawnEnemy", 3, 1); //3초후 부터, SpawnEnemy함수를 1초마다 반복해서 실행 시킵니다.
    }
    void Update () {

    }
}
