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


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        //코드 내에서 Animator로 인식하고, Animator의 필드나 메소드를
        //사용할 수 있습니다.
    }
    protected void SetAnimator(string temp)
    {
        //기본 파라미터에 대한 reset
        //유니티 Animator에 만들어둔 parameter의 이름을 정확하게 기재합니다.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        //인자로 전달받은 값을 true로 설정합니다.
        animator.SetBool(temp, true);
    }
}
