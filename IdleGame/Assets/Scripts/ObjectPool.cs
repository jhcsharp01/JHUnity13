using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : IPool
{
    public Transform Trans { get; set; }
    public Queue<GameObject> pool { get ; set ;} = new Queue<GameObject>(); //ť ����

    public GameObject get(Action<GameObject> action = null)
    {
        var obj = pool.Dequeue(); //Ǯ�� �ִ� ������Ʈ �ϳ��� �������ϴ�.
        obj.SetActive(true); //���ӿ�����Ʈ.SetActive(true);�� ���� ������Ʈ�� Ȱ��ȭ
        //action���� ������ ����� �ִٸ�?
        if(action != null)
        {
            action.Invoke(obj);
            //Invoke�� ���� �ش� �Լ��� �����Ű�� ���� �����մϴ�.
        }
        return obj;
    }
    public void Release(GameObject obj, Action<GameObject> action = null)
    {
        pool.Enqueue(obj);//Ǯ�� ������Ʈ�� �ٽ� ����մϴ�.
        obj.transform.parent = Trans;
        //���� ������Ʈ�� �θ� Ʈ������ = Trans
        obj.SetActive(false); //��Ȱ��ȭ
        if (action != null)
        {
            action.Invoke(obj);
            //Invoke�� ���� �ش� �Լ��� �����Ű�� ���� �����մϴ�.
        }
    }
}
