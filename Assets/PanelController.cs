using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Acquire(string s){
        float fDelay = 2f;
        Invoke("InActiveGO", fDelay);
        ActiveGO(s);
    }

    void ActiveGO(string s)
    {
        gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Text>().text = s;
        Debug.Log("ActiveGO");
    }

    void InActiveGO()
    {
        gameObject.SetActive(false);
        Debug.Log("InActiveGO");
    }
}
