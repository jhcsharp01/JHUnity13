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

    [Header("Ÿ�� ��ġ")]
    public Transform target;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        //�ڵ� ������ Animator�� �ν��ϰ�, Animator�� �ʵ峪 �޼ҵ带
        //����� �� �ֽ��ϴ�.
    }
    protected void SetAnimator(string temp)
    {
        //[����]
        if (temp == "isATTACK")
        {
            animator.SetTrigger("isATTACK");
            //Ʈ���Ÿ� �۵���Ű�� �ٷ� ����
            return;
        }

        //[�̵��� ���]
        //�⺻ �Ķ���Ϳ� ���� reset
        //����Ƽ Animator�� ������ parameter�� �̸��� ��Ȯ�ϰ� �����մϴ�.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        //���ڷ� ���޹��� ���� true�� �����մϴ�.
        animator.SetBool(temp, true);
    }

    //�켱 Ÿ��
     protected void StrikeFirst<T>(T[] targets) where T : Component
     {
        var enemys = targets;
        Transform closet = null;
        var max = T_RANGE;

        for (int i = 0; i < enemys.Length; i++)
        {
            var target_distance = Vector3.Distance(
                transform.position, enemys[i].transform.position);

            //�ִ뺸�� �Ÿ� �۴ٸ�
            if(target_distance < max)
            {
                //������ �� ������ ��ġ�� ���� ����� �Ÿ�
                closet = enemys[i].transform;
                //�ִ� �Ÿ� == Ÿ���� �Ÿ�
                max = target_distance;
            }
        }
        //���� ����� ���� Ÿ������ ���
        target = closet;

        if(target != null)
        {
            transform.LookAt(target.position);
        }
     }
}
