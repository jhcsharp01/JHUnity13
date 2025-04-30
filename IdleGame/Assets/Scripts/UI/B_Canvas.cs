using UnityEngine;

//�⺻ ĵ���� ���� ��ũ��Ʈ
public class B_Canvas : MonoBehaviour
{
    public static B_Canvas instance = null;
    public Transform Coin;   //Coin ���
    public Transform Layers; //Layers ���
    //#Layer 1 : �����Ǵ� ������ ���� ��ġ   ---> CoinMove.cs
    //#Layer 2 : ������ �ؽ�Ʈ ����ϴ� ��ġ     ----> HitText.cs
    //#Layer 3 : ������ �������� ���� ��ġ   ---> Item_Object.cs

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
