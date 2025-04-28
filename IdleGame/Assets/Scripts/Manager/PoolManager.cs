using System;
using System.Collections.Generic;
using UnityEngine;


//���¿� ���� Ǯ�� ������ �� �ְ�, �⺻ Ʋ�� �����մϴ�.
public interface IPool
{
    //pool
    // -> Monster(1)
    // -> Monster(2)

    //1.��������� ��ġ
    Transform Trans { get; set;  }

    //2. Ǯ �ȿ� ������� ���� ������Ʈ 
    //Ǯ�� ���� �� ���Ǵ� �ڷᱸ��(�÷���)
    //1. ť :    ���Լ���� ������ �� �־ ���ϰ� ������ ����
    // --> ť�� ���̾�α�, Ǯ ���� �� ��Ա� ����.
    //2. ����Ʈ : ���������� ����Ǳ� ������ ������ ����.
    Queue<GameObject> pool {get;set;}
    //������Ƽ�� ���
    // {get; set;} return�� �� ������ �Ѵ� ����
    // {get; } return�� ����
    // {set; } set�� ����
    // { get {get �� ��� ������ ��ɹ�;}   set{set�� ��� ������ ��ɹ�;}

    //3. Ǯ���� ������ �۾�
    GameObject get(Action<GameObject> action = null);
    //3-1. ����Ƽ�� �븮�� Action, Func, Delegate, UnityEvent
    //Action�� void ������ �Լ� ó���� ������ �� ���� �븮��
    //Func�� return ���� �ִ� ������ �Լ� ó���� ������ �� ���� �븮��

    //3-2. default parameter
    //�޼ҵ��� ���Ժ� () �ȿ��� �Ű� ������ �ۼ��� �� �ֽ��ϴ�.
    //�Ű������� ���� ��������� default parameter�� �Ǽ�
    //����ڰ� �μ��� �� ������ ���� ��� ������ ������ �ڵ� ó���˴ϴ�.

    //4. Ǯ�� ���� �ݳ� �۾�
    void Release(GameObject obj, Action<GameObject> action = null);
}

public class PoolManager : MonoBehaviour
{
    //Dictionary <K,V>
    //��ųʸ� : Ű�� ���� �ϳ��� ������ �����ϴ� �ڷᱸ��
    //Ű : ���� �����ϱ� ���� ������ ��
    //�� : Ű�� ���� ���� �� �ִ� �������� ������(�ߺ� ����)

    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();
    
    //Transform b_obj;

    public IPool pooling(string path)
    {
        //ContainsKey(Key) �ش� Ű�� ��ųʸ��� �ִ����� �Ǵ��ϴ� ����
        if(pool_dict.ContainsKey(path) == false) Add_pool(path);
            //Add_pool�� public�� �ƴ� private�� ¥�� ����
            //pooling�� ���� Add_pool ��� ��� ����

        //Ǯ�� ��ϵ� ���� ���ٸ�
        if (pool_dict[path].pool.Count <= 0) Add_Queue(path); //ť ���

        //Ǯ�� ���� return
        return pool_dict[path];
    }

    /// <summary>
    /// Ǯ ���
    /// </summary>
    /// <param name="path">���</param>
    private GameObject Add_pool(string path)
    {
        var obj = new GameObject($"{path} Pool");
        //���޹��� ��θ� + Pool�� �� ������Ʈ�� ������ ���Դϴ�.
        var obj_pool = new ObjectPool();
        //������Ʈ Ǯ ����

        pool_dict.Add(path, obj_pool); //��ųʸ��� ���
        obj_pool.Trans = obj.transform; //Ʈ������ ����
        return obj; //������Ʈ ����
    }

    /// <summary>
    /// ť�� �߰�
    /// </summary>
    /// <param name="path">���</param>
    private void Add_Queue(string path)
    {
        //1. ���޹��� ��η� ���� ������Ʈ ����
        var obj = Manager.Instance.ResourceInstantiate(path);
        //Resources.Load<T>();�� ����Ƽ ������Ʈ ���� Resources �����κ���
        //��ο� �´� <T>�� �ε��ϴ� ���
        obj.transform.parent = pool_dict[path].Trans;


        pool_dict[path].Release(obj); //ObjectPool Ŭ������ Release ������� false ó�� ����
    }

}
