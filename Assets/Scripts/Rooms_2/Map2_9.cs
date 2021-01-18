using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Map2_9 : MonoBehaviour
{
    enum State { Init, Cur, Old };
    static State state = State.Init;
    static bool visted = false;
    SpriteRenderer render;
    public string scene_name;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //image = GetComponents<Image>();
        if (SceneManager.GetActiveScene().name == scene_name)
        {
            state = State.Cur;
            visted = true;
        }
        else if (visted)
        {
            state = State.Old;
        }

        //Color color = image.color;
        if (state == State.Cur)
        {
            render.color = new Color(1, 1, 1, 1);
        }
        else if (state == State.Init)
        {
            render.color = new Color(1, 1, 1, 0);
        }
        else
        {
            render.color = new Color(1, 1, 1, 0.5f);
        }

        //image.color = color;
    }
}
