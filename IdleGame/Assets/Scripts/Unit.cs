using UnityEngine;
//2025-04-28
//1. Animator, start , SetAnimator  Monster -> Unit

public class Unit : MonoBehaviour
{
    Animator animator;
    [Header("�÷��̾� �ɷ�ġ")]
    //�ɷ�ġ ���� �ڷ����� ��ġ�� �������δ� �����͸� �� ũ�� ��� ��
    public double HP;
    public double ATK;
    public double ATK_SPEED;

    [Header("���� ����")]
    public float A_RANGE; //��Ÿ�
    public float T_RANGE; //�߰� ����


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        //�ڵ� ������ Animator�� �ν��ϰ�, Animator�� �ʵ峪 �޼ҵ带
        //����� �� �ֽ��ϴ�.
    }
    protected void SetAnimator(string temp)
    {
        //�⺻ �Ķ���Ϳ� ���� reset
        //����Ƽ Animator�� ������ parameter�� �̸��� ��Ȯ�ϰ� �����մϴ�.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        //���ڷ� ���޹��� ���� true�� �����մϴ�.
        animator.SetBool(temp, true);
    }
}
