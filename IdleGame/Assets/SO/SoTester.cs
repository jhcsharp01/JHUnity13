using UnityEngine;

public class SoTester : MonoBehaviour
{
    public Item[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var item in items)
        {
            Debug.Log($"아이템 이름 : {item.name} {item.description} 가격 : {item.value}");
        }
    } 
}
