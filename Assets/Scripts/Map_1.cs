using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Map_1 : MonoBehaviour
{
    enum State { Init, Cur, Old };
    static State state = State.Init;
    static bool visted = false;
    SpriteRenderer render;
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
        if (SceneManager.GetActiveScene().name == "First trial")
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
            render.color = new Color(1, 1, 1, 1);
        }
        else
        {
            render.color = new Color(1, 1, 1, 1);
        }

        //image.color = color;
    }
}
