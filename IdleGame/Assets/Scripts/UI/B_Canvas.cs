using UnityEngine;

//�⺻ ĵ���� ���� ��ũ��Ʈ
public class B_Canvas : MonoBehaviour
{
    public static B_Canvas instance = null;
    public Transform Coin;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
