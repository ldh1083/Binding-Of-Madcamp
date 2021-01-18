using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewMap : MonoBehaviour
{
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            Player.changingmap = true;
            Application.LoadLevel(levelToLoad);
        }
    }
}
