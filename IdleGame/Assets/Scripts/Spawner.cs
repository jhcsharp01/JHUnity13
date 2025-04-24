using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 1. ������ ������ ������ �� ���� ���ٴ� �� ��������
    //    �����Ǵ� ��찡 ����.(�� Ÿ��)

    //  �� �۾��� ����Ƽ������ �ڷ�ƾ�̶�� ������� �����մϴ�.

    //�ڷ�ƾ�� ���� ���Ǵ� ���
    //1. ���� ����
    //2. ����, ��ų ��Ÿ��

    public int count;          //������ ������ ����
    public float spawnTime;    //���� �ֱ�(�� Ÿ��, ���� Ÿ��...)
    public GameObject monster_prefab; //���� ������


    private void Start()
    {
        StartCoroutine(CSpawn());
        //StartCorountine(�Լ���());
    }


    IEnumerator CSpawn()
    {
        //1. ��� ������ ���ΰ�?
        Vector3 pos;
        //2. �� �� ������ ���ΰ�?
        for (int i = 0; i < count; i++)
        {
            //3. � ���·� ������ ���ΰ�?
            pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
            //���� ���� ����(y ��ǥ = 0)
            pos.y = 0.0f;
            //������ �Ÿ��� �������� �� ���� ����
            while(Vector3.Distance(pos, Vector3.zero) <= 5.0f)
            {
                pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
                pos.y = 0.0f;
            }
            Instantiate(monster_prefab,pos,Quaternion.identity);
            //Quaternion.identity : ȸ�� �� 0
            //���� ���¸� �״�� �����ϴ� ��쿡 ����ϴ� ��
        }
        //yield return : ���� ���� �� �ٽ� ���ƿ��� �ڵ�
        //WaitForSeconds(float t) : �ۼ��� �� ��ŭ ����մϴ�.(�� ����)
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(CSpawn());
    }
}
