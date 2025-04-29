using System.Collections;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    Vector3 target; //��ġ
    //5���� ����
    RectTransform[] rects = new RectTransform[5];
    public float distance; //�Ÿ�
    public float speed; //���� �̵� �ӵ�
    public float item_move_speed; //�������� �������� �ӵ�

    private void Awake()
    {
        //RectTransfrom�� �ν����Ϳ��� ���� �������� �ʰ�, ��ũ��Ʈ�� ����
        //�����ϴ� ��� �߰��ϴ� �ڵ�
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }
    }

    public void Init(Vector3 pos)
    {
        target = pos;
        transform.position = Camera.main.WorldToScreenPoint(pos);

        //rects���� position�� 0,0���� �̵�
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i].anchoredPosition = Vector2.zero;
            //UI ������ �ʼ�
            //Vector2 anchoredPosition
            //�г��� ��Ŀ�κ����� ��ġ�� ��Ÿ���ϴ�.
            // --> �ν����� �󿡼� ���̴� posX, posY�� ��ġ
        }
        //ĵ���� ������ Ʈ������ ����
        transform.parent = B_Canvas.instance.transform;

        //�ڷ�ƾ �۵�
        StartCoroutine(Move());
    }

    //������ �������� �ڷ�ƾ���� ����
    IEnumerator Move()
    {
        //rects���� ��ǥ�� ���� �迭 ����
        var pos = new Vector2[rects.Length];
        for (int i = 0; i < rects.Length; i++)
        {
            pos[i] = new Vector2(target.x, target.y) + Random.insideUnitCircle * Random.Range(-distance, distance) ;
        }
        //���� ����
        while(true)
        {
            for (int i = 0; i < rects.Length; i++)
            {
                var rect = rects[i];

                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, pos[i], speed * Time.deltaTime);

                //�Ÿ��� ���� ������ �����ؼ� Ż���ϴ� �ڵ�

            }
            if(CheckDistance(pos, 0.5f))
            {
                break;
            }
            yield return null;
            //yield return null�� �� �������� ���
        }

        //�ݺ� �۾� ������ ������ �߰�
        yield return new WaitForSeconds(0.5f);

        //============ ������(����)�� ������ ȿ�� ================================

        while(true)
        {
            for (int i = 0; i < rects.Length; i++)
            {
                var rect = rects[i];

                rect.position = Vector2.MoveTowards(rect.position,
                    B_Canvas.instance.Coin.position, speed * item_move_speed *  Time.deltaTime);
            }

            if(CheckDistanceCoinUI(0.5f))
            {
                Manager.Pool.pool_dict["Coin_Move"].Release(gameObject);
                break;
            }
            yield return null;
        }
        //======== ������(����)�� UI ���� ������ �̵��ϴ� ȿ�� ================
    }

    private bool CheckDistanceCoinUI(float range)
    {
        for (int i = 0; i < rects.Length; i++)
        {
            var distance = Vector2.Distance(rects[i].anchoredPosition,
                B_Canvas.instance.Coin.position);
            if (distance > range)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckDistance(Vector2[] end, float range)
    {
        //�Ÿ� üũ�� ���� �������� ���� ã�Ƴ��ϴ�.
        for(int i = 0; i < rects.Length; i++)
        {
            var distance = Vector2.Distance(rects[i].anchoredPosition, end[i]);
            //a�� b ���� �Ÿ��� üũ�ϴ� ���� Vector2.Distance(Vector2 a,Vector2 b);

            if(distance > range)
            {
                return false;
            }
        }
        //���� ��찡 �ƴϸ� true
        return true;
    }

}
