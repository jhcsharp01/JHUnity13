using UnityEngine;

public class SoTester : MonoBehaviour
{
    public Item[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var item in items)
        {
            Debug.Log($"������ �̸� : {item.name} {item.description} ���� : {item.value}");
        }
    } 
}
