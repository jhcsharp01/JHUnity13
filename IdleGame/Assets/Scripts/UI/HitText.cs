using UnityEngine;
using UnityEngine.UI;

//�¾��� ��, ���� ��� �ʿ� �ؽ�Ʈ�� �ߵ���
public class HitText : MonoBehaviour
{
    Vector3 target; //���
   //Camera cam;     //ī�޶�
    public Text message; //�ؽ�Ʈ

    //�ؽ�Ʈ ��� ��ġ ���� ��
    float up = 0.0f;


    private void Start()
    {
        
    }

    private void Update()
    {
        var pos = new Vector3(target.x, target.y + up, target.z);
        transform.position = Camera.main.WorldToScreenPoint(pos);
        //���� ī�޶� �������� ��ũ�� ��ġ�� �����մϴ�.
    }

    public void Init(Vector3 pos, double value)
    {
        target = pos;
        message.text = value.ToString();
    }


    //�߰��� ����غ� ���� ��
    //�Ϲ� �������� ũ��Ƽ�� ������ ����
}
