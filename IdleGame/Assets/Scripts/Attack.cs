using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공격 보조를 위한 스크립트
//플레이어의 공격 애니메이션에 맞춰 이벤트로 처리
public class Attack : MonoBehaviour
{
    public Transform target;
    public float move_speed;
    public Vector3 target_pos;
    double damage;
    public string attack_key;
    public bool hit = false;


    //공격 시 생성될 오브젝트
    Dictionary<string, GameObject> attacks = new Dictionary<string, GameObject>();
    //공격이 적중하면 적용될 이펙트
    Dictionary<string, ParticleSystem> attacks_enter = new Dictionary<string, ParticleSystem>();

    private void Awake()
    {
        //Attack 컴포넌트를 연결한 기준으로 자식 값에 접근하는 코드 GetChild(0)
        var attacks_trans = transform.GetChild(0);
        var onattacks_trans = transform.GetChild(1);

        //attacks_trans가 가지고 있는 자식 값만큼 작업 진행
        for(int i = 0; i < attacks_trans.childCount; i++)
        {
            //딕셔너리에 등록합니다.
            attacks.Add(attacks_trans.GetChild(i).name, attacks_trans.GetChild(i).gameObject);
        }

        for(int i = 0; i < onattacks_trans.childCount; i++)
        {
            attacks_enter.Add(onattacks_trans.GetChild(i).name, onattacks_trans.GetChild(i).GetComponent<ParticleSystem>());
        }
    }


    public void Init(Transform t, double dmg , string key)
    {
        //전달받은 값으로 타겟 설정, 응시
        target = t;
        transform.LookAt(target);

        //히트 처리 false
        hit = false;

        //전달받은 값으로 데미지 설정
        damage = dmg;

        //전달받은 값으로 키 설정
        attack_key = key;

        //전달받은 키에 해당하는 공격용 게임 오브젝트만 활성화
        attacks[attack_key].gameObject.SetActive(true);

    }
    private void Update()
    {
        //hit가 활성화된 상태라면 return해서 hit에 대한 처리가 다 끝난 뒤, 새롭게 hit가 false가 되면 작동할 수 있게 설정합니다.
        if (hit) return;

        target_pos.y = 1.0f; //현재 위치하고 있는 타겟의 위치와 y축 길이를 맞춰주기

        //공격 위치가 타겟 지점으로 이동하도록 설정
        transform.position = Vector3.MoveTowards(transform.position, target_pos, move_speed * Time.deltaTime);

        //거리가 근접할 경우
        if(Vector3.Distance(transform.position, target_pos) <= 0.1f)
        {
            //타겟 지정 후
            if (target != null)
            {
                //히트 true
                hit = true;

                //유닛이 가진 체력을 데미지만큼 감소합니다.
                target.GetComponent<Unit>().HP -= damage;
                //감소하면 플레이어나 몬스터가 데미지 처리하는 함수가 처리되도록 설정해줄 필요가 있습니다.

                //데미지 적용 후 비활성화
                attacks[attack_key].gameObject.SetActive(false);

                //공격 명중 이펙트 플레이
                attacks_enter[attack_key].Play();
                //플레이를 통해 파티클 실행을 진행(따라서 이 시점부터 Play on Awake 제거)

                StartCoroutine(ReleaseObject(attacks_enter[attack_key].main.duration));
                //파티클의 duration은 현재 main.duration으로 접근 가능합니다.
                //일반적으로 코루틴은 Start 영역에서 진행하나, Update여도 특정 조건일 때만 실행되는 수준에서는
                //사용하는 경우도 있다.
            }
        }
    }

    //일정 시간 뒤에 오브젝트 반납
    IEnumerator ReleaseObject(float time)
    {       
        //전달받은 초만큼 대기
        yield return new WaitForSeconds(time);

        //Attack에 대한 반납
        Manager.Pool.pool_dict["Attack"].Release(gameObject);
    }


}
