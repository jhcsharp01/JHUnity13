using System.Collections;
using UnityEngine;

public class Item_Object : MonoBehaviour
{
    public float angle = 45.0f;
    public float gravity = 9.8f;
    public float range = 2.0f;

    public void Init(Vector3 pos)
    {
        //���޹��� ���� �������� �� �ֺ��� ��ġ�� �� �ֵ��� ���� ����
        Vector3 item_pos = new Vector3
            (
            pos.x + (Random.insideUnitSphere.x * range),
            0.0f,  //���� ��ġ
            pos.z + (Random.insideUnitSphere.z * range)
            );
        //�� ����� ���� ���� ��� �� �������� �۾� ����
        //��ü �̵� ����
        StartCoroutine(Simulate(pos));
    }
    IEnumerator Simulate(Vector3 pos)
    {
        float target_Distance = Vector3.Distance(transform.position, pos);
        float radian = angle * Mathf.Deg2Rad; //���� ��ȯ ��
        float velocity = Mathf.Sqrt(target_Distance * gravity / Mathf.Sin(2 * radian));

        float vx = velocity * Mathf.Cos(radian);
        float vy = velocity * Mathf.Sin(radian);

        float duration = target_Distance / vx;

        transform.rotation = Quaternion.LookRotation(pos - transform.position);
        //LookAt ó�� ȸ�� ���� �ٶ󺸰� ����� �ڵ�


        float simulate_time = 0.0f;

        while(simulate_time < duration)
        {
            simulate_time += Time.deltaTime;

            //�ð��� �������� ������ ���� �Ʒ���, �غ� �������� �̵�
            transform.Translate(0, (vy - (gravity * simulate_time)), vx * Time.deltaTime);
            yield return null;
        }       
    }     
}
