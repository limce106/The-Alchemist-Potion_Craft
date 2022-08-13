using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // 이동 속도 변수
    public float moveSpeed;
    // 캐릭터 콘트롤러 변수
    CharacterController cc;
    // 중력 변수
    float gravity = -20f;
    // 수직 속력 변수
    float yVelocity = 0;
    // 카메라
    private Camera maincamera;
    // 마우스 클릭 위치
    private Vector3 destination;
    // Npc(손님, 포션 상점 주인)와 플레이어의 거리
    float dist;
    // 움직임 여부
    public bool isMoving;
    // Talk 스크립트
    Talk talk;
    // 플레이어
    GameObject player;
    // 상점 주인
    GameObject storeNpc;
    // 대화데이터베이스에 접근
    TalkManager talkManager;
    // 명성 슬라이더
    Slider famebar;
    // 최대 명성 수치
    private float maxfame = 100;
    // 현재 명성 수치
    float curfame; // 싱글톤 변수로 바꾸기
    // 레벨(명성);
    Text tlevel;
    // 레벨업할 때 나타나는 패널
    GameObject levelpanel;
    // 레벨업할 때 나타나는 텍스트
    Text levelup;

    public Animator anim;
    private Behavior behavior;
    private Vector3 targetPos;
    GameObject coinPanel;
    Text cointext;

    float rotateSpeed = 10.0f;
    Vector3 lookDirection;

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        // 캐릭터 콘트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        maincamera = Camera.main;
        isMoving = true;
        player = GameObject.Find("Player");
        storeNpc = GameObject.Find("StoreNpc");
        if(SceneManager.GetActiveScene().name == "ItemStore" || SceneManager.GetActiveScene().name == "House")
        {
            talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
            talk = GameObject.Find("TalkManager").GetComponent<Talk>();
        }
        behavior = GetComponent<Behavior>();
        targetPos = this.transform.position;
        if(SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "House")
        {
            talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
            famebar = GameObject.Find("fameBar").GetComponent<Slider>();
            tlevel = GameObject.Find("tLevel").GetComponent<Text>();
            levelpanel = GameObject.Find("LevelPanel");
            cointext = GameObject.Find("coinText").GetComponent<Text>();
            coinPanel = GameObject.Find("CoinPanel");
            levelup = GameObject.Find("Levelup").GetComponent<Text>();
            levelpanel.SetActive(false);
            levelup.gameObject.SetActive(false);
        }
        if(SceneManager.GetActiveScene().name == "ItemStore")
        {
            talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
            coinPanel = GameObject.Find("CoinPanel");
            cointext = GameObject.Find("coinText").GetComponent<Text>();
            coinPanel.SetActive(false);
            cointext.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        Move();
        if (SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "House")
        {
            famebar = GameObject.Find("fameBar").GetComponent<Slider>();
            tlevel = GameObject.Find("tLevel").GetComponent<Text>();
            cointext = GameObject.Find("coinText").GetComponent<Text>();
            famebar.value = (float)PlayerDataManager.instance.fame/(float) maxfame;
            tlevel.text = "Level "+PlayerDataManager.instance.level.ToString();
            cointext.text = "코인 " + PlayerDataManager.instance.coin;
        }
    }

    // 전체 이동 제어 함수
    // 여기 Move() 함수에서 NPC라는 오브젝트가 있어야 꼭 이동할 수 있도록 구현되어 있어 오류가 납니다!
    // 처음 시작 단계에서는 NPC 생성이 되어 있지 않아서 참고할 수 없는 오브젝트라고 나와요!
    private void Move()
    {
        // 씬이름이 플레이어의 포션 가게일 때(현재는 임시로 Player)
        if (SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "House")
        {
            // // 플레이어와 손님의 거리
            // if (dist <= 1.5f)
            // {
            //     talk.ClickTalk(talkManager.customerTalk);
            // }
            // else
            // {
            //     if (isMoving)
            //     {
            //         // MouseMove();는 플레이어의 포션 가게에서만 사용
            //         MouseMove();
            //     }
            // }

            if (isMoving)
            {
                // MouseMove();는 플레이어의 포션 가게에서만 사용
                MouseMove();
            }
        }
        // 씬이름이 플레이어의 포션 가게가 아닐 때
        else
        {
            KeyMove();
            if (SceneManager.GetActiveScene().name == "ItemStore")
            {
                storeNpc = GameObject.Find("StoreNpc");
                dist = Vector3.Distance(player.transform.position, storeNpc.transform.position);
                // 플레이어와 가게 주인의 거리
                if (dist <= 3f)
                {
                    talk.ClickTalk(talkManager.storeTalk);
                    coinPanel.SetActive(true);
                    cointext.gameObject.SetActive(true);
                    cointext.text = "코인 " + PlayerDataManager.instance.coin;
                }
                else
                {
                    if (isMoving)
                    {
                        KeyMove();
                    }
                }
            }
        }
    }

    // 방향키로 이동하는 함수
    private void KeyMove()
    {
            // 사용자의 입력을 받는다
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if(v >= 0.1f)
            {
                anim.SetBool("isMove", true);
            }
            else if(v <= -0.1f)
            {
                anim.SetBool("isMove", true);
            }
            else if(h >= 0.1f)
            {
                anim.SetBool("isMove", true);
            }
            else if(h <= -0.1f)
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }

            // 이동 방향을 설정한다.
            Vector3 dir = new Vector3(h, 0, v);
            dir = dir.normalized;
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
                || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                lookDirection = v * Vector3.forward + h * Vector3.right;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            // 메인 카메라를 기준으로 방향을 변환한다.
            //dir = Camera.main.transform.TransformDirection(dir);

            // 캐릭터 수직 속도에 중력 값을 적용한다.
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;

            // 이동 속도에 맞춰 이동한다.
            //transform.Translate(dir * moveSpeed * Time.deltaTime);
            cc.Move(dir * moveSpeed * Time.deltaTime);
    }

    // 마우스로 이동하는 함수
    public void MouseMove()
    {
        // 마우스 좌측 버튼을 눌렀을 때
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // 마우스 클릭 위치를 찾아낸다.
            if(Physics.Raycast(maincamera.ScreenPointToRay(Input.mousePosition), out hit, 1000.0f))
            {
                targetPos = hit.point;
            }
        }
        //Vector3 dir = targetPos - transform.position;

        //cc.Move(dir * 3f * Time.deltaTime);

        // 캐릭터가 움직이고 있다면
        if (behavior.Run(targetPos)) //targetPos는 마우스가 찍힌 스크린좌표를 월드좌표 환산한 벡터값
        {
            // 회전도 같이 시켜준다
            behavior.Turn(targetPos);//targetPos는 마우스가 찍힌 스크린좌표를 월드좌표 환산한 벡터값
        }
        else
        {
            // 캐릭터 애니메이션(정지 상태)
            anim.Play("Witch_Wait");
        }
    }
    // private void SetDestination(Vector3 dest)
    // {
    //     destination = dest;
    // }

    // 성공과 실패 처리 구분해 주기 위해서 함수를 두 개로 나누었습니다!
    // CustomerCtrl 함수에서 주문 처리 시 호출합니다!
    public void controlFameUp(int coin)
    {   
        if(SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "House")
        {
            // 손님에게 적절한 포션을 제공했을 때 경험치가 오르도록 수정 -> 적용 완료. 위 주석 참고!
            PlayerDataManager.instance.fame += 30;
            PlayerDataManager.instance.coin += coin;

            if(PlayerDataManager.instance.fame >= 100)
            {
                PlayerDataManager.instance.level += 1;
                PlayerDataManager.instance.fame -= 100;
                StartCoroutine(LevelUp());
            }
        }
    }

    public void controlFameDown()
    {
        if(SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "House")
        {
            PlayerDataManager.instance.fame -= 10;

            if (PlayerDataManager.instance.fame <= 0)
            {
                if(PlayerDataManager.instance.level == 1)
                {
                    PlayerDataManager.instance.level = 1;
                    PlayerDataManager.instance.fame = 0;
                }
                else
                {
                    PlayerDataManager.instance.level -= 1;
                    PlayerDataManager.instance.fame += 100;
                    StartCoroutine(LevelDown());
                }
            }
        }
    }

    // 레벨업할 때 실행되는 코루틴
    IEnumerator LevelUp()
    {
        levelup.text = "Level Up!\n" + "Level" + PlayerDataManager.instance.level;
        levelpanel.SetActive(true);
        levelup.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        levelpanel.SetActive(false);
        levelup.gameObject.SetActive(false);
    }

    IEnumerator LevelDown()
    {
        levelup.text = "Level Down\n" + "Level" + PlayerDataManager.instance.level;
        levelpanel.SetActive(true);
        levelup.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        levelpanel.SetActive(false);
        levelup.gameObject.SetActive(false);
    }
}
