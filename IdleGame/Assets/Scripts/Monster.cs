using System.Collections;
using UnityEngine;

//컴포넌트(Component)
//유니티 오브젝트가 사용할 기능
//제공되는 컴포넌트가 있고, 스크립트의 경우는 사용자가 만들어주는 사용자 정의
//컴포넌트로써 활용이 가능합니다.(Mono 상속)

//MonoBehavior 상속
//1. 유니티 오브젝트에 해당 클래스를 컴포넌트로써 등록할 수 있습니다.
public class Monster : Unit
{
    //유니티 인스펙터에 해당 필드 값에 대한 범위 설정
    [Range(1,5)] public float speed;
    

    //몬스터 클래스에서 상황에 맞게 애니메이션을 실행시키려 합니다.
    //이때 필요한 데이터는 무엇일까요? 2
    //1. Animation
    //2. Animator

    bool isSpawn = false; //생성 여부

    //몬스터가 생성됬을때 진행할 작업(연출)
    //서서히 커지는 느낌
    IEnumerator OnSpawn()
    {
        float current = 0.0f; //프레임 값 저장용
        float percent = 0.0f; //반복문의 종료 조건
        float start = 0.0f;  //변화 시작 값
        float end = transform.localScale.x; //변화 마지막 값

        //localScale은 게임 오브젝트의 상대적인 크기를 의미합니다.
        //현재는 오브젝트의 크기로 기억합니다.
        while(percent < 1.0f)
        {
            current += Time.deltaTime;
            percent = current / 3.0f;

            //start에서 end 지점까지 percent 간격으로 이동해라.
            var pos = Mathf.Lerp(start, end, percent);

            //계산한 수치만큼 스케일(크기)를 적용합니다.
            transform.localScale = new Vector3(pos, pos, pos);
            //탈출했다가 돌아옵니다.
            yield return null;
        }    
        yield return new WaitForSeconds(0.5f);
        isSpawn = true;
    }

    protected override void Start()
    {
        base.Start(); //Unit의 Start 호출
        //Monster가 실행할 Start 작업 구현
        MonsterInit();
    }

    public void MonsterInit() => StartCoroutine(OnSpawn());


    //유니티 라이프 싸이클 함수
    private void Update()
    {  
        transform.LookAt(Vector3.zero);

        if (isSpawn == false)
        {
            return;
        }

        var distance = Vector3.Distance(transform.position, Vector3.zero);
        //설정한 기준보다 측정 거리가 작으면
        if (distance <= 0.5f)
        {
            SetAnimator("isIDLE"); //대기모드로 변경합니다.
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * speed);
            SetAnimator("isMOVE"); //이동 모드로 변경합니다.
        }

        #region 필기
        //1. transform.position : 현재 오브젝트의 위치를 나타냅니다.
        //2. Vector3 : 3D 환경의 좌표계 (X,Y,Z 축) 구성
        //3. MoveTowards(start, end, speed); start부터 end 지점까지 speed 수치만큼 이동합니다.
        //4. Time.deltaTime : 이전 프레임이 완료되기까지 걸린 시간
        //                    (컴퓨터의 성능이 느릴수록 값이 커짐)
        //                    일반적으로 약 1초
        //                    업데이트에서 작업을 하는데 잇어서의 보정 값 역할
        //5. trasnform.LookAt(Vector3 posotion) : 특정 방향을 바라보게 설정해주는 기능


        //방향 벡터 : 기본적으로 제공해주는 Vector 값
        //Vector3.right == new Vector3(1,0,0);
        //Vector3.left  == new Vector3(-1,0,0);
        //Vector3.up    == new Vector3(0,1,0);
        //Vector3.down  == new Vector3(0,-1,0);
        //Vector3.forward == new Vector3(0,0,1);
        //Vector3.back    == new Vector3(0,0,-1);
        //Vector3.zero    == new Vector3(0,0,0);
        //Vector3.one     == new Vector3(1,1,1);
        #endregion
    }

   


}
