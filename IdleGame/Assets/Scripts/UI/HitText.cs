using UnityEngine;
using UnityEngine.UI;

//맞았을 때, 맞은 대상 쪽에 텍스트가 뜨도록
public class HitText : MonoBehaviour
{
    Vector3 target; //대상
   //Camera cam;     //카메라
    public Text message; //텍스트

    //텍스트 출력 위치 보정 값
    float up = 0.0f;


    private void Start()
    {
        
    }

    private void Update()
    {
        var pos = new Vector3(target.x, target.y + up, target.z);
        transform.position = Camera.main.WorldToScreenPoint(pos);
        //메인 카메라 기준으로 스크린 위치로 설정합니다.
    }

    public void Init(Vector3 pos, double value)
    {
        target = pos;
        message.text = value.ToString();
    }


    //추가로 고민해볼 법한 것
    //일반 데미지와 크리티컬 데미지 구현
}
