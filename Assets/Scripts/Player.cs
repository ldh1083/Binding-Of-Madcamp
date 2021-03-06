﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveControl();
    }

    void moveControl()
    {
        float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //아까 지정한 Axes를 통해 키의 방향을 판단하고 속도와 프레임 판정을 곱해 이동량을 정해줍니다.
        this.gameObject.transform.Translate(distanceX*3, 0, 0);
        float distanceY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        this.gameObject.transform.Translate(0, distanceY*3, 0);

        //이동량만큼 실제로 이동을 반영합니다.
    }
}
