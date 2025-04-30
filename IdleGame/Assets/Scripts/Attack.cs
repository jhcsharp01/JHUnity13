using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ������ ���� ��ũ��Ʈ
//�÷��̾��� ���� �ִϸ��̼ǿ� ���� �̺�Ʈ�� ó��
public class Attack : MonoBehaviour
{
    public Transform target;
    public float move_speed;
    public Vector3 target_pos;
    double damage;
    public string attack_key;
    public bool hit = false;


    //���� �� ������ ������Ʈ
    Dictionary<string, GameObject> attacks = new Dictionary<string, GameObject>();
    //������ �����ϸ� ����� ����Ʈ
    Dictionary<string, ParticleSystem> attacks_enter = new Dictionary<string, ParticleSystem>();

    private void Awake()
    {
        //Attack ������Ʈ�� ������ �������� �ڽ� ���� �����ϴ� �ڵ� GetChild(0)
        var attacks_trans = transform.GetChild(0);
        var onattacks_trans = transform.GetChild(1);

        //attacks_trans�� ������ �ִ� �ڽ� ����ŭ �۾� ����
        for(int i = 0; i < attacks_trans.childCount; i++)
        {
            //��ųʸ��� ����մϴ�.
            attacks.Add(attacks_trans.GetChild(i).name, attacks_trans.GetChild(i).gameObject);
        }

        for(int i = 0; i < onattacks_trans.childCount; i++)
        {
            attacks_enter.Add(onattacks_trans.GetChild(i).name, onattacks_trans.GetChild(i).GetComponent<ParticleSystem>());
        }
    }


    public void Init(Transform t, double dmg , string key)
    {
        //���޹��� ������ Ÿ�� ����, ����
        target = t;
        transform.LookAt(target);

        //��Ʈ ó�� false
        hit = false;

        //���޹��� ������ ������ ����
        damage = dmg;

        //���޹��� ������ Ű ����
        attack_key = key;

        //���޹��� Ű�� �ش��ϴ� ���ݿ� ���� ������Ʈ�� Ȱ��ȭ
        attacks[attack_key].gameObject.SetActive(true);

    }
    private void Update()
    {
        //hit�� Ȱ��ȭ�� ���¶�� return�ؼ� hit�� ���� ó���� �� ���� ��, ���Ӱ� hit�� false�� �Ǹ� �۵��� �� �ְ� �����մϴ�.
        if (hit) return;

        target_pos.y = 1.0f; //���� ��ġ�ϰ� �ִ� Ÿ���� ��ġ�� y�� ���̸� �����ֱ�

        //���� ��ġ�� Ÿ�� �������� �̵��ϵ��� ����
        transform.position = Vector3.MoveTowards(transform.position, target_pos, move_speed * Time.deltaTime);

        //�Ÿ��� ������ ���
        if(Vector3.Distance(transform.position, target_pos) <= 0.1f)
        {
            //Ÿ�� ���� ��
            if (target != null)
            {
                //��Ʈ true
                hit = true;

                //������ ���� ü���� ��������ŭ �����մϴ�.
                target.GetComponent<Unit>().HP -= damage;
                //�����ϸ� �÷��̾ ���Ͱ� ������ ó���ϴ� �Լ��� ó���ǵ��� �������� �ʿ䰡 �ֽ��ϴ�.

                //������ ���� �� ��Ȱ��ȭ
                attacks[attack_key].gameObject.SetActive(false);

                //���� ���� ����Ʈ �÷���
                attacks_enter[attack_key].Play();
                //�÷��̸� ���� ��ƼŬ ������ ����(���� �� �������� Play on Awake ����)

                StartCoroutine(ReleaseObject(attacks_enter[attack_key].main.duration));
                //��ƼŬ�� duration�� ���� main.duration���� ���� �����մϴ�.
                //�Ϲ������� �ڷ�ƾ�� Start �������� �����ϳ�, Update���� Ư�� ������ ���� ����Ǵ� ���ؿ�����
                //����ϴ� ��쵵 �ִ�.
            }
        }
    }

    //���� �ð� �ڿ� ������Ʈ �ݳ�
    IEnumerator ReleaseObject(float time)
    {       
        //���޹��� �ʸ�ŭ ���
        yield return new WaitForSeconds(time);

        //Attack�� ���� �ݳ�
        Manager.Pool.pool_dict["Attack"].Release(gameObject);
    }


}
