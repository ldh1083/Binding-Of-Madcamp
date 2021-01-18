using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject laserPrefab; //발사할 레이저를 저장합니다.
    public bool canShoot = true; //레이저를 쏠 수 있는 상태인지 검사합니다.
    const float shootDelay = 0.3f; //레이저를 쏘는 주기를 정해줍니다.
    float shootTimer = 0; //시간을 잴 타이머를 만들어줍니다.
    public float moveSpeed;
    // Start is called before the first frame update
    private Animator anim;
    public Text collectedText;
    public static int collectedAmount = 0;
    
    Rigidbody2D rigidbody;

    public float bulletSpeed;

    public static bool changingmap = false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        /*if (SceneManager.GetActiveScene().name == "Room1")
        {
            this.gameObject.transform.Translate(-4.38f, 16f, 0f);
        }*/
        
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (changingmap)
            setposition();
        moveControl();
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");
        if((shootHor != 0 || shootVert != 0) && Time.time > shootTimer + shootDelay){
                Shoot(shootHor, shootVert);
                shootTimer = Time.time;
        }

        anim.SetFloat("MoveX", Input.GetAxis("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxis("Vertical"));
    }

    void moveControl()
    {

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = worldPos; //좌표를 적용한다.

        float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //아까 지정한 Axes를 통해 키의 방향을 판단하고 속도와 프레임 판정을 곱해 이동량을 정해줍니다.
        float distanceY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        

        

        rigidbody.velocity = new Vector3(distanceX*moveSpeed*10, distanceY * moveSpeed*10, 0);
        collectedText.text = "Items Collected: " + collectedAmount;

        //이동량만큼 실제로 이동을 반영합니다.
    }

    void OnTriggerEnter2D(Collider2D other)
//rigidBody가 무언가와 충돌할때 호출되는 함수 입니다.
//Collider2D other로 부딪힌 객체를 받아옵니다.
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            /* 여기서 부터 추가된 부분*/
            Instantiate(explosionPrefab,
            // Instantiate는 객체를 하나 생성(복제)합니다 첫번째 인자로는 생성할 객체의 원본을 넣어주고
                this.transform.position,
                //두번째 인자로는 생성될 위치를 넣어줍니다. this.transform.position은 자기자신의 위치를 나타냅니다.
                Quaternion.identity);
                //세번째 인자로는 객체의 회전값을 넣어주는데요, Quaternion.identity는 회전이 적용되지 않은 값을 나타냅니다.
            /* 여기까지 */
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    void Shoot(float x, float y) // 발사를 관리하는 함수 입니다.
    {
            GameObject bullet = Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
            );
    }

    void setposition()
    {
        if (SceneManager.GetActiveScene().name == "Room9")
        {
            this.gameObject.transform.position = new Vector2(16.02f, -4.68f);
        }
        else if (this.gameObject.transform.position.y<-20.0f) // Entry to Room1
        {
            this.gameObject.transform.position = new Vector2(16.02f, -13.47f);
        }
        else if (this.gameObject.transform.position.y > -5f) // N to S
        {
            this.gameObject.transform.position = new Vector2(16.02f, -13.3f);
        }
        else if (this.gameObject.transform.position.y < -13f) // S to N
        {
            this.gameObject.transform.position = new Vector2(16.02f, -4.68f);
        }
        else if (this.gameObject.transform.position.x < 5.5f) // W to E
        {
            this.gameObject.transform.position = new Vector2(27.52f, -9.03f);
        }
        else // E to W
        {
            this.gameObject.transform.position = new Vector2(4.55f, -9.03f);
        }
        changingmap = false;
    }
}
