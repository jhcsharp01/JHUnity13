using UnityEngine;

public class Player : Unit
{
    Vector3 pos;     //벡터는 좌표계
    Quaternion quat; //쿼터니언은 회전 값

    protected override void Start()
    {
        base.Start();
        pos = transform.position;
        quat = transform.rotation;
    }

    //메인 로직을 짜는 위치(Update)
    private void Update()
    {


        if (target == null)
        {

            StrikeFirst(Spawner.monster_list.ToArray());
            //리스트 -> 배열
            //타겟이 없다?

            //거리 계산
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
        //타겟 거리 설정
        var targetDistance = Vector3.Distance(transform.position, target.position);

        //사정 거리 안에 들어왔을 경우(공격 사정 범위에는 포함이 안되는 경우)
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
