using UnityEngine;

//SO : 스크립트처럼 사용하는 오브젝트
//특징 : 유니티에 Asset으로 저장할 수 있습니다.
//       유니티를 실행하고 종료해도, 유니티 내부에 남아 있는 데이터
//     씬에서 사용하지 않고, 데이터로써 사용합니다.

[CreateAssetMenu(fileName = "item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public Sprite Item_Image;
    public string description;
    public int value;
    private int id; //고유 번호

    public int ID { get { return id; } }
}
