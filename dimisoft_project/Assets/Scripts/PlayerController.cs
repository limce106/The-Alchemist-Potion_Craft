using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // �̵� �ӵ� ����
    public float moveSpeed;
    // ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;
    // �߷� ����
    float gravity = -20f;
    // ���� �ӷ� ����
    float yVelocity = 0;
    // ī�޶�
    private Camera maincamera;
    // ���콺 Ŭ�� ��ġ
    private Vector3 destination;
    // Npc(�մ�, ���� ���� ����)�� �÷��̾��� �Ÿ�
    float dist;
    // ������ ����
    public bool isMoving;
    // Talk ��ũ��Ʈ
    Talk talk;
    // �÷��̾�
    GameObject player;
    // ���� ����
    GameObject storeNpc;
    // ��ȭ�����ͺ��̽��� ����
    TalkManager talkManager;
    // �� �����̴�
    Slider famebar;
    // �ִ� �� ��ġ
    private float maxfame = 100;
    // ���� �� ��ġ
    float curfame; // �̱��� ������ �ٲٱ�
    // ����(��);
    Text tlevel;
    // �������� �� ��Ÿ���� �г�
    GameObject levelpanel;
    // �������� �� ��Ÿ���� �ؽ�Ʈ
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
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
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
            cointext.text = "���� " + PlayerDataManager.instance.coin;
        }
    }

    // ��ü �̵� ���� �Լ�
    // ���� Move() �Լ����� NPC��� ������Ʈ�� �־�� �� �̵��� �� �ֵ��� �����Ǿ� �־� ������ ���ϴ�!
    // ó�� ���� �ܰ迡���� NPC ������ �Ǿ� ���� �ʾƼ� ������ �� ���� ������Ʈ��� ���Ϳ�!
    private void Move()
    {
        // ���̸��� �÷��̾��� ���� ������ ��(����� �ӽ÷� Player)
        if (SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "House")
        {
            // // �÷��̾�� �մ��� �Ÿ�
            // if (dist <= 1.5f)
            // {
            //     talk.ClickTalk(talkManager.customerTalk);
            // }
            // else
            // {
            //     if (isMoving)
            //     {
            //         // MouseMove();�� �÷��̾��� ���� ���Կ����� ���
            //         MouseMove();
            //     }
            // }

            if (isMoving)
            {
                // MouseMove();�� �÷��̾��� ���� ���Կ����� ���
                MouseMove();
            }
        }
        // ���̸��� �÷��̾��� ���� ���԰� �ƴ� ��
        else
        {
            KeyMove();
            if (SceneManager.GetActiveScene().name == "ItemStore")
            {
                storeNpc = GameObject.Find("StoreNpc");
                dist = Vector3.Distance(player.transform.position, storeNpc.transform.position);
                // �÷��̾�� ���� ������ �Ÿ�
                if (dist <= 3f)
                {
                    talk.ClickTalk(talkManager.storeTalk);
                    coinPanel.SetActive(true);
                    cointext.gameObject.SetActive(true);
                    cointext.text = "���� " + PlayerDataManager.instance.coin;
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

    // ����Ű�� �̵��ϴ� �Լ�
    private void KeyMove()
    {
            // ������� �Է��� �޴´�
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

            // �̵� ������ �����Ѵ�.
            Vector3 dir = new Vector3(h, 0, v);
            dir = dir.normalized;
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
                || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                lookDirection = v * Vector3.forward + h * Vector3.right;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            // ���� ī�޶� �������� ������ ��ȯ�Ѵ�.
            //dir = Camera.main.transform.TransformDirection(dir);

            // ĳ���� ���� �ӵ��� �߷� ���� �����Ѵ�.
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;

            // �̵� �ӵ��� ���� �̵��Ѵ�.
            //transform.Translate(dir * moveSpeed * Time.deltaTime);
            cc.Move(dir * moveSpeed * Time.deltaTime);
    }

    // ���콺�� �̵��ϴ� �Լ�
    public void MouseMove()
    {
        // ���콺 ���� ��ư�� ������ ��
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // ���콺 Ŭ�� ��ġ�� ã�Ƴ���.
            if(Physics.Raycast(maincamera.ScreenPointToRay(Input.mousePosition), out hit, 1000.0f))
            {
                targetPos = hit.point;
            }
        }
        //Vector3 dir = targetPos - transform.position;

        //cc.Move(dir * 3f * Time.deltaTime);

        // ĳ���Ͱ� �����̰� �ִٸ�
        if (behavior.Run(targetPos)) //targetPos�� ���콺�� ���� ��ũ����ǥ�� ������ǥ ȯ���� ���Ͱ�
        {
            // ȸ���� ���� �����ش�
            behavior.Turn(targetPos);//targetPos�� ���콺�� ���� ��ũ����ǥ�� ������ǥ ȯ���� ���Ͱ�
        }
        else
        {
            // ĳ���� �ִϸ��̼�(���� ����)
            anim.Play("Witch_Wait");
        }
    }
    // private void SetDestination(Vector3 dest)
    // {
    //     destination = dest;
    // }

    // ������ ���� ó�� ������ �ֱ� ���ؼ� �Լ��� �� ���� ���������ϴ�!
    // CustomerCtrl �Լ����� �ֹ� ó�� �� ȣ���մϴ�!
    public void controlFameUp(int coin)
    {   
        if(SceneManager.GetActiveScene().name == "Player_customer" || SceneManager.GetActiveScene().name == "House")
        {
            // �մԿ��� ������ ������ �������� �� ����ġ�� �������� ���� -> ���� �Ϸ�. �� �ּ� ����!
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

    // �������� �� ����Ǵ� �ڷ�ƾ
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
