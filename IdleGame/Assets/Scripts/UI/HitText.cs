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

        //�ش� cs ������ ���� UI�� B_Canvas(�⺻ ĵ����) �ʿ� ����
        transform.parent = B_Canvas.instance.transform;
    }

    //�ǰ� �ؽ�Ʈ �ݳ� �ڵ�
    private void Release()
    {
        Manager.Pool.pool_dict["Hit"].Release(gameObject);
    }


    //�߰��� ����غ� ���� ��
    //�Ϲ� �������� ũ��Ƽ�� ������ ����
}
