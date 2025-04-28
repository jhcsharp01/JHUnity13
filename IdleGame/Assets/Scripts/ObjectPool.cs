using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : IPool
{
    public Transform Trans { get; set; }
    public Queue<GameObject> pool { get ; set ;} = new Queue<GameObject>(); //큐 생성

    public GameObject get(Action<GameObject> action = null)
    {
        var obj = pool.Dequeue(); //풀에 있는 오브젝트 하나를 내보냅니다.
        obj.SetActive(true); //게임오브젝트.SetActive(true);는 게임 오브젝트의 활성화
        //action으로 실행할 기능이 있다면?
        if(action != null)
        {
            action.Invoke(obj);
            //Invoke를 통해 해당 함수를 실행시키는 것이 가능합니다.
        }
        return obj;
    }
    public void Release(GameObject obj, Action<GameObject> action = null)
    {
        pool.Enqueue(obj);//풀에 오브젝트를 다시 등록합니다.
        obj.transform.parent = Trans;
        //현재 오브젝트의 부모 트랜스폼 = Trans
        obj.SetActive(false); //비활성화
        if (action != null)
        {
            action.Invoke(obj);
            //Invoke를 통해 해당 함수를 실행시키는 것이 가능합니다.
        }
    }
}
