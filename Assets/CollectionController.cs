using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{
    public string name;
    public string description;
    public Sprite itemImage;
}

public class CollectionController : MonoBehaviour
{
    public Item item;
    public float healthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public float bulletSizeChange;
    public int bulletStrengthChange;
    public int bulletMultipleChange;
    public float bulletLifetimeChange;
    public float bulletSpeedChange;
    public int spawnFamiliar;
    public GameObject panel;

    // Start is called before the first frame update
    private void Awake() {
        panel.SetActive(false);
    }
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Player.collectedAmount++;
            GameController.HealPlayer(healthChange);
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.FireRateChange(attackSpeedChange);
            GameController.BulletSizeChange(bulletSizeChange);
            GameController.BulletStrengthChange(bulletStrengthChange);
            GameController.BulletMultipleChange(bulletMultipleChange);
            GameController.BulletLifetimeChange(bulletLifetimeChange);
            GameController.BulletSpeedChange(bulletSpeedChange);
            GameController.SpawnFamiliarChange(spawnFamiliar);
            panel.GetComponent<PanelController>().Acquire(item.name);
            Destroy(gameObject);
        }
        
    }
    
}
