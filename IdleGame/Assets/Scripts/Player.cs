using UnityEngine;

public class Player : Unit
{
    Vector3 pos;     //���ʹ� ��ǥ��
    Quaternion quat; //���ʹϾ��� ȸ�� ��

    protected override void Start()
    {
        base.Start();
        pos = transform.position;
        quat = transform.rotation;
    }

    //���� ������ ¥�� ��ġ(Update)
    private void Update()
    {


        if (target == null)
        {

            StrikeFirst(Spawner.monster_list.ToArray());
            //����Ʈ -> �迭
            //Ÿ���� ����?

            //�Ÿ� ���
            var targetPos = Vector3.Distance(transform.position, pos);

            if(targetPos > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime);
                transform.LookAt(pos);
                SetAnimator("isMOVE");
            }
            else
            {
                transform.rotation = quat;
                SetAnimator("isIDLE");
            }
            return;
        }    
        //Ÿ�� �Ÿ� ����
        var targetDistance = Vector3.Distance(transform.position, target.position);

        //���� �Ÿ� �ȿ� ������ ���(���� ���� �������� ������ �ȵǴ� ���)
        if(targetDistance <= T_RANGE && targetDistance > A_RANGE)
        {
            SetAnimator("isMOVE");
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
        }
        else if(targetDistance <= A_RANGE)
        {
            SetAnimator("isATTACK");
        }


    }
}
