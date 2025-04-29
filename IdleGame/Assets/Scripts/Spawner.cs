using System.Collections;
using System.Collections.Generic; //List<T> 사용을 위한 추가
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 1. 몬스터의 생성은 프레임 당 생성 보다는 초 간격으로
    //    생성되는 경우가 많다.(젠 타임)

    //  이 작업을 유니티에서는 코루틴이라는 기법으로 설계합니다.

    //코루틴이 자주 사용되는 경우
    //1. 몬스터 생성
    //2. 물약, 스킬 쿨타임
    public int count;          //생성될 몬스터의 개수
    public float spawnTime;    //생성 주기(젠 타임, 스폰 타임...)
    //public GameObject monster_prefab; //몬스터 프리팹

    public static List<Monster> monster_list = new List<Monster>();
    public static List<Player> player_list = new List<Player>();
    //방치형 게임에서 캐릭터를 여러 개 사용하는 경우가 존재하기 때문

    private void Start()
    {
        StartCoroutine(CSpawn());
        //StartCorountine(함수명());
    }
    IEnumerator CSpawn()
    {
        //1. 어디에 생성할 것인가?
        Vector3 pos;
        //2. 몇 번 생성할 것인가?
        for (int i = 0; i < count; i++)
        {
            //3. 어떤 형태로 생성할 것인가?
            pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
            //생성 높이 통일(y 좌표 = 0)
            pos.y = 0.0f;
            //생성된 거리를 기준으로 재 생성 유도
            while(Vector3.Distance(pos, Vector3.zero) <= 5.0f)
            {
                pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
                pos.y = 0.0f;
            }
            //Instantiate(monster_prefab,pos,Quaternion.identity);

            //Action 대리자를 활용해 몬스터 풀링
            //var go = Manager.Pool.pooling("Monster").get();

            var go = Manager.Pool.pooling("Monster").get((value) =>
            {
                value.GetComponent<Monster>().MonsterInit();
                value.transform.position = pos;
                value.transform.LookAt(Vector3.zero);
                var go = value.GetComponent<Monster>();
                //monster_list.Add(go);
            });
            //Quaternion.identity : 회전 값 0
            //기존 형태를 그대로 생성하는 경우에 사용하는 값

            //반납
           //StartCoroutine(CRelease(go));
        }

        //yield return : 일정 시점 후 다시 돌아오는 코드
        //WaitForSeconds(float t) : 작성한 값 만큼 대기합니다.(초 단위)
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(CSpawn());
    }

    IEnumerator CRelease(GameObject obj)
    {
        //1초 대기
        yield return new WaitForSeconds(5.0f);

        Manager.Pool.pool_dict["Monster"].Release(obj);
    }


}
