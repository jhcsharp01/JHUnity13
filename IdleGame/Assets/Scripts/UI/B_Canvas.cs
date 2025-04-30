using UnityEngine;

//�⺻ ĵ���� ���� ��ũ��Ʈ
public class B_Canvas : MonoBehaviour
{
    public static B_Canvas instance = null;
    public Transform Coin;   //Coin ���
    public Transform Layers; //Layers ���
    //#Layer 1 : �����Ǵ� ������ ���� ��ġ   ---> CoinMove.cs
    //#Layer 2 : ������ �������� ���� ��ġ   ---> Item_Object.cs
    //#Layer 3 : ����

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


    public Transform GetLayer(int value)
    {
        return Layers.GetChild(value);
    }

}
