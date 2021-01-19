using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move_test : MonoBehaviour
{
    public const float moveSpeed = 5.0f;
    private Animator anim;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveControl();
        anim.SetFloat("MoveX", player.transform.position.x - transform.position.x);
        anim.SetFloat("MoveY", player.transform.position.y - transform.position.y);
    }

    void moveControl()
    {
        float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //아까 지정한 Axes를 통해 키의 방향을 판단하고 속도와 프레임 판정을 곱해 이동량을 정해줍니다.
        this.gameObject.transform.Translate(distanceX * 3, 0, 0);
        float distanceY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        this.gameObject.transform.Translate(0, distanceY * 3, 0);

        //이동량만큼 실제로 이동을 반영합니다.
    }
}