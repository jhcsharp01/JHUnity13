using System;
using System.Collections.Generic;
using UnityEngine;


//형태에 따라 풀이 제공될 수 있게, 기본 틀을 제공합니다.
public interface IPool
{
    //pool
    // -> Monster(1)
    // -> Monster(2)

    //1.만들어지는 위치
    Transform Trans { get; set;  }

    //2. 풀 안에 만들어줄 게임 오브젝트 
    //풀을 만들 때 사용되는 자료구조(컬렉션)
    //1. 큐 :    선입선출로 관리할 수 있어서 편리하게 구현이 가능
    // --> 큐는 다이얼로그, 풀 만들 때 써먹기 좋음.
    //2. 리스트 : 순차적으로 저장되기 때문에 구현이 쉬움.
    Queue<GameObject> pool {get;set;}
    //프로퍼티의 경우
    // {get; set;} return과 값 설정이 둘다 가능
    // {get; } return만 가능
    // {set; } set만 가능
    // { get {get 할 경우 실행할 명령문;}   set{set할 경우 실행할 명령문;}

    //3. 풀에서 얻어오는 작업
    GameObject get(Action<GameObject> action = null);
    //3-1. 유니티의 대리자 Action, Func, Delegate, UnityEvent
    //Action은 void 형태의 함수 처리를 진행할 때 쓰는 대리자
    //Func는 return 값이 있는 형태의 함수 처리를 진행할 때 쓰는 대리자

    //3-2. default parameter
    //메소드의 도입부 () 안에는 매개 변수를 작성할 수 있습니다.
    //매개변수에 값을 적어놓으면 default parameter가 되서
    //사용자가 인수로 값 전달을 안할 경우 설정한 값으로 자동 처리됩니다.

    //4. 풀에 대한 반납 작업
    void Release(GameObject obj, Action<GameObject> action = null);
}

public class PoolManager : MonoBehaviour
{
    //Dictionary <K,V>
    //딕셔너리 : 키와 값을 하나의 쌍으로 저장하는 자료구조
    //키 : 값을 접근하기 위한 고유한 값
    //값 : 키를 통해 얻을 수 있는 실질적인 데이터(중복 가능)

    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();
    
    //Transform b_obj;

    public IPool pooling(string path)
    {
        //ContainsKey(Key) 해당 키가 딕셔너리에 있는지를 판단하는 문법
        if(pool_dict.ContainsKey(path) == false) Add_pool(path);
            //Add_pool이 public이 아닌 private로 짜인 이유
            //pooling을 통해 Add_pool 기능 사용 가능

        //풀에 등록된 값이 없다면
        if (pool_dict[path].pool.Count <= 0) Add_Queue(path); //큐 등록

        //풀에 대한 return
        return pool_dict[path];
    }

    /// <summary>
    /// 풀 등록
    /// </summary>
    /// <param name="path">경로</param>
    private GameObject Add_pool(string path)
    {
        var obj = new GameObject($"{path} Pool");
        //전달받은 경로명 + Pool로 빈 오브젝트가 생성될 것입니다.
        var obj_pool = new ObjectPool();
        //오브젝트 풀 생성

        pool_dict.Add(path, obj_pool); //딕셔너리에 등록
        obj_pool.Trans = obj.transform; //트랜스폼 설정
        return obj; //오브젝트 리턴
    }

    /// <summary>
    /// 큐에 추가
    /// </summary>
    /// <param name="path">경로</param>
    private void Add_Queue(string path)
    {
        //1. 전달받은 경로로 게임 오브젝트 생성
        var obj = Manager.Instance.ResourceInstantiate(path);
        //Resources.Load<T>();는 유니티 프로젝트 내의 Resources 폴더로부터
        //경로에 맞는 <T>를 로드하는 기능
        obj.transform.parent = pool_dict[path].Trans;


        pool_dict[path].Release(obj); //ObjectPool 클래스의 Release 기능으로 false 처리 진행
    }

}
