using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public string map_name;
    // Start is called before the first frame update
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        panel.SetActive(false);
        map_name = SceneManager.GetActiveScene().name;

    }
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != map_name)
        {
            Player.items.Add(gameObject);
            gameObject.SetActive(false);
        }
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
            Destroy(this.gameObject);
        }
        
    }
    
}
