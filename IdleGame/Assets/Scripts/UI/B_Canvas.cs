using UnityEngine;

//기본 캔버스 관련 스크립트
public class B_Canvas : MonoBehaviour
{
    public static B_Canvas instance = null;
    public Transform Coin;   //Coin 등록
    public Transform Layers; //Layers 등록
    //#Layer 1 : 생성되는 코인이 들어가는 위치   ---> CoinMove.cs
    //#Layer 2 : 데미지 텍스트 출력하는 위치     ----> HitText.cs
    //#Layer 3 : 생성될 아이템이 들어가는 위치   ---> Item_Object.cs

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
