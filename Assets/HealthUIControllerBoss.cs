using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIControllerBoss : MonoBehaviour
{
    public GameObject heartContainer;
    public GameObject bossEnemy;
    private float fillValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossEnemy != null){
        fillValue = (float)(bossEnemy.GetComponent<enemy2>().enemyHealth);
        fillValue = fillValue / bossEnemy.GetComponent<enemy2>().maxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
        }
        else{
            Destroy(heartContainer);
        }
    }
}
