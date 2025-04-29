using UnityEngine;
//2025-04-28
//1. Animator, start , SetAnimator  Monster -> Unit

public class Unit : MonoBehaviour
{
    Animator animator;
    [Header("플레이어 능력치")]
    //능력치 관련 자료형은 방치형 기준으로는 데이터를 좀 크게 잡는 편
    public double HP;
    public double ATK;
    public double ATK_SPEED;

    [Header("공격 범위")]
    public float A_RANGE; //사거리
    public float T_RANGE; //추격 범위

    [Header("타겟 위치")]
    public Transform target;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        //코드 내에서 Animator로 인식하고, Animator의 필드나 메소드를
        //사용할 수 있습니다.
    }
    protected void SetAnimator(string temp)
    {
        //[공격]
        if (temp == "isATTACK")
        {
            animator.SetTrigger("isATTACK");
            //트리거를 작동시키면 바로 실행
            return;
        }

        //[이동과 대기]
        //기본 파라미터에 대한 reset
        //유니티 Animator에 만들어둔 parameter의 이름을 정확하게 기재합니다.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        //인자로 전달받은 값을 true로 설정합니다.
        animator.SetBool(temp, true);
    }

    //우선 타격
     protected void StrikeFirst<T>(T[] targets) where T : Component
     {
        var enemys = targets;
        Transform closet = null;
        var max = T_RANGE;

        for (int i = 0; i < enemys.Length; i++)
        {
            var target_distance = Vector3.Distance(
                transform.position, enemys[i].transform.position);

            //최대보다 거리 작다면
            if(target_distance < max)
            {
                //현재의 그 몬스터의 위치가 가장 가까운 거리
                closet = enemys[i].transform;
                //최대 거리 == 타겟의 거리
                max = target_distance;
            }
        }
        //가장 가까운 값을 타겟으로 등록
        target = closet;

        if(target != null)
        {
            transform.LookAt(target.position);
        }
     }
}
