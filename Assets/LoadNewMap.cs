using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewMap : MonoBehaviour
{
    public string levelToLoad;
    SpriteRenderer render;
    bool boss_room;
    bool boss_clear;
    // Start is called before the first frame update
    void Start()
    {
        if (Player.enemy_num == 0 && SceneManager.GetActiveScene().name == "Room9")
        {
            render = GetComponent<SpriteRenderer>();
            boss_room = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boss_room && !boss_clear)
        {
            if (Player.enemy_num < 1)
            {
                boss_clear = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (boss_room)
        {
            if (boss_clear)
            {
                render.color = new Color(1, 1, 1, 1);
                if (other.gameObject.name == "player")
                {
                    Player.changingmap = true;
                    Application.LoadLevel(levelToLoad);
                    Player.enemy_num = 0;
                }
            }
        }
        else
        {
            if (Player.enemy_num <= 0)
            {
                if (other.gameObject.name == "player")
                {
                    Player.changingmap = true;
                    Application.LoadLevel(levelToLoad);
                    Player.enemy_num = 0;
                }
            }
        }
    }
}
