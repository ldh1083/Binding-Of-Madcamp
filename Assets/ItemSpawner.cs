using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable{
        public GameObject gameObject;
        public float weight;
    }
    public List<Spawnable> items = new List<Spawnable>();
    float totalWeight;
    public GameObject panel;
    private void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().find_item();
        totalWeight = 0;
        foreach(var spawnable in items){
            totalWeight += spawnable.weight;
        }
        if (SceneManager.GetActiveScene().name.Length < 7)
        {
            if (Player.clear1[int.Parse(SceneManager.GetActiveScene().name.Substring(4)) - 1])
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (Player.clear2[int.Parse(SceneManager.GetActiveScene().name.Substring(6)) - 1])
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;

        while(pick > cumulativeWeight && chosenIndex  < items.Count -1){
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;
        }

        GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
        i.GetComponent<CollectionController>().panel = panel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
