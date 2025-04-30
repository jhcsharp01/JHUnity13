using System.Collections;
using UnityEngine;

public class Item_Object : MonoBehaviour
{
    public float angle = 45.0f;
    public float gravity = 9.8f;
    public float range = 2.0f;

    public void Init(Vector3 pos)
    {
        //전달받은 값을 기준으로 그 주변에 위치할 수 있도록 범위 설정
        Vector3 item_pos = new Vector3
            (
            pos.x + (Random.insideUnitSphere.x * range),
            0.0f,  //생성 위치
            pos.z + (Random.insideUnitSphere.z * range)
            );
        //이 기능을 몬스터 쪽의 사망 시 판정에서 작업 진행
        //물체 이동 시작
        StartCoroutine(Simulate(pos));
    }
    IEnumerator Simulate(Vector3 pos)
    {
        float target_Distance = Vector3.Distance(transform.position, pos);
        float radian = angle * Mathf.Deg2Rad; //라디안 변환 값
        float velocity = Mathf.Sqrt(target_Distance * gravity / Mathf.Sin(2 * radian));

        float vx = velocity * Mathf.Cos(radian);
        float vy = velocity * Mathf.Sin(radian);

        float duration = target_Distance / vx;

        transform.rotation = Quaternion.LookRotation(pos - transform.position);
        //LookAt 처럼 회전 방향 바라보게 만드는 코드


        float simulate_time = 0.0f;

        while(simulate_time < duration)
        {
            simulate_time += Time.deltaTime;

            //시간이 지날수록 위에서 점점 아래로, 밑변 방향으로 이동
            transform.Translate(0, (vy - (gravity * simulate_time)), vx * Time.deltaTime);
            yield return null;
        }       
    }     
}
