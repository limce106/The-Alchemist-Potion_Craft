using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // 손님 대화데이터베이스
    public List<string> customerTalk = new List<string> ();
    // 이후 약초 상점 주인 대화데이터베이스도 여기에 구현
    // 약초 상점 주인의 tag도 Npc로 설정
    public List<string> storeTalk = new List<string> ();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // 손님 대화데이터베이스

        // 소화되는 포션 (0~2)
        customerTalk.Add("오늘 저녁을 너무 과하게\n먹었나 봐요.");
        customerTalk.Add("소화가 잘 되게 해 주는\n포션도 있나요?");
        customerTalk.Add("배가 터질 것 같아요.\n도움되는 포션 하나 주세요.");

        // 성장 촉진되는 포션 (3~5)
        customerTalk.Add("친구들이 키가 작다고 놀려요.");
        customerTalk.Add("성장 촉진용 포션 하나 주세요.");
        customerTalk.Add("얼른 어른이 되고 싶어요.\n저한테 딱 맞는 포션으로 주세요.");

        // 회복 포션 (6~8)
        customerTalk.Add("요즘 공부하느라 매일 피곤해요.");
        customerTalk.Add("체력을 보충할 수 있는 포션\n있을까요?");
        customerTalk.Add("원기 회복이 필요한 타이밍이에요!");

        // 마나 포션 (9~11)
        customerTalk.Add("마법 연습을 하다가 지쳤어요.");
        customerTalk.Add("마나를 회복할 수 있는 포션\n있나요?");
        customerTalk.Add("급하게 마법을 써야 하는데\n힘이 모자라요.");

        // 빙결 포션 (12~14)
        customerTalk.Add("날씨가 너무 더워서 힘들어요!");
        customerTalk.Add("열사병에 걸린 것 같아요.");
        customerTalk.Add("시원한 게 마시고 싶은데\n얼음이 없어요.");

        // 집중력 강화 포션 (15~17)
        customerTalk.Add("당장 내일이 시험인데\n글자가 눈에 안 들어와요.");
        customerTalk.Add("공부가 너무 어려워요!");
        customerTalk.Add("집중력을 올려 주는 포션 주세요.");

        // 피를 멈추게 하는 포션 (18~20)
        customerTalk.Add("피를 멈추게 하는 포션 있나요?");
        customerTalk.Add("무릎이 심하게 까져서\n피가 안 멈춰요.");
        customerTalk.Add("친구가 많이 다친 것 같아요.\n출혈이 심해요.");

        // 진실만 말하게 되는 포션 (21~23)
        customerTalk.Add("애인이 거짓말을 하는 것 같아요.");
        customerTalk.Add("진실만 말하게 되는 포션 하나 주세요.");
        customerTalk.Add("진실 게임을 하기로 했어요.\n포션 하나 주세요.");

        // 행운을 올려주는 포션 (24~26)
        customerTalk.Add("요즘 운이 없는 것 같아요.\n도움되는 포션 있나요?");
        customerTalk.Add("행운을 올려 주는 포션 하나 주세요!");
        customerTalk.Add("내일은 중요한 날이에요!\n행운이 필요해요.");

        // 수면 포션 (27~29)
        customerTalk.Add("불면증 때문에 죽겠어요.");
        customerTalk.Add("요즈음 잠이 모자라요.\n포션 추천해 주실 수 있죠?");
        customerTalk.Add("수면에 도움되는 포션도 파나요?");


        storeTalk.Add("어떤 약초를 구매하시겠습니까?");
        storeTalk.Add("구매할 수 없습니다.");
    }

    void Update()
    {
        
    }
}
