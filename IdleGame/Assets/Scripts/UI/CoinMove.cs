using System.Collections;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    Vector3 target; //위치
    //5개의 코인
    RectTransform[] rects = new RectTransform[5];
    public float distance; //거리
    public float speed; //코인 이동 속도
    public float item_move_speed; //아이템이 빨려가는 속도

    private void Awake()
    {
        //RectTransfrom을 인스펙터에서 직접 연결하지 않고, 스크립트를 통해
        //연결하는 경우 추가하는 코드
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }
    }

    public void Init(Vector3 pos)
    {
        target = pos;
        transform.position = Camera.main.WorldToScreenPoint(pos);

        //rects들의 position을 0,0으로 이동
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i].anchoredPosition = Vector2.zero;
            //UI 개발의 필수
            //Vector2 anchoredPosition
            //패널의 앵커로부터의 위치를 나타냅니다.
            // --> 인스펙터 상에서 보이는 posX, posY의 위치
        }
        //캔버스 쪽으로 트랜스폼 연결
        transform.parent = B_Canvas.instance.transform;

        //코루틴 작동
        StartCoroutine(Move());
    }

    //코인의 움직임을 코루틴으로 구현
    IEnumerator Move()
    {
        //rects들의 좌표에 대한 배열 생성
        var pos = new Vector2[rects.Length];
        for (int i = 0; i < rects.Length; i++)
        {
            pos[i] = new Vector2(target.x, target.y) + Random.insideUnitCircle * Random.Range(-distance, distance) ;
        }
        //무한 루프
        while(true)
        {
            for (int i = 0; i < rects.Length; i++)
            {
                var rect = rects[i];

                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, pos[i], speed * Time.deltaTime);

                //거리에 대한 로직을 설계해서 탈출하는 코드

            }
            if(CheckDistance(pos, 0.5f))
            {
                break;
            }
            yield return null;
            //yield return null은 한 프레임을 대기
        }

        //반복 작업 끝나고 딜레이 추가
        yield return new WaitForSeconds(0.5f);

        //============ 아이템(코인)이 퍼지는 효과 ================================

        while(true)
        {
            for (int i = 0; i < rects.Length; i++)
            {
                var rect = rects[i];

                rect.position = Vector2.MoveTowards(rect.position,
                    B_Canvas.instance.Coin.position, speed * item_move_speed *  Time.deltaTime);
            }

            if(CheckDistanceCoinUI(0.5f))
            {
                Manager.Pool.pool_dict["Coin_Move"].Release(gameObject);
                break;
            }
            yield return null;
        }
        //======== 아이템(코인)이 UI 코인 쪽으로 이동하는 효과 ================
    }

    private bool CheckDistanceCoinUI(float range)
    {
        for (int i = 0; i < rects.Length; i++)
        {
            var distance = Vector2.Distance(rects[i].anchoredPosition,
                B_Canvas.instance.Coin.position);
            if (distance > range)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckDistance(Vector2[] end, float range)
    {
        //거리 체크를 통해 부적합한 값을 찾아냅니다.
        for(int i = 0; i < rects.Length; i++)
        {
            var distance = Vector2.Distance(rects[i].anchoredPosition, end[i]);
            //a와 b 사이 거리를 체크하는 문법 Vector2.Distance(Vector2 a,Vector2 b);

            if(distance > range)
            {
                return false;
            }
        }
        //위의 경우가 아니면 true
        return true;
    }

}
