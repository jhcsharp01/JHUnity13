using System;
using UnityEngine;

//������Ʈ ���� �Ŵ����� �����ϴ� �Ŵ���(Brain)
public class Manager : MonoBehaviour
{
    #region Singleton
    //1. �ڱ� �ڽſ� ���� ���� �ν��Ͻ��� �ʵ� �Ǵ� ������Ƽ�� �����ϴ�.
    //�� �ʵ�� �⺻ ���� null�Դϴ�.
    public static Manager Instance = null;

    //2. ������ ���۵Ǳ� �� �ܰ迡�� �ʱ�ȭ�� ����˴ϴ�.
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //���� �ν��Ͻ��� null�� ���
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //���ӿ�����Ʈ�� DDOL�� �Ѱ��ݴϴ�.
            //�� ���� �ִ� ������Ʈ�� ���� �ε��ص� �ı����� �ʰ�
            //�״�� ���޵˴ϴ�.
        }
        else
        {
           Destroy(gameObject);
           //null�� �ƴ϶��, �ı��ع����ϴ�.
        }
    }
    #endregion


    //��ϵ� �Ŵ���
    private static PoolManager PoolManager = new PoolManager();

    //�Ŵ��� ������ ���� ������Ƽ
    public static PoolManager Pool { get  { return PoolManager; } }

    //��...
    public GameObject ResourceInstantiate(string path) => Instantiate(Resources.Load<GameObject>(path));

}

