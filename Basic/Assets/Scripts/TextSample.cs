using UnityEngine;
using UnityEngine.UI; //text 사용을 위한 추가
using TMPro;          //textMeshPro 사용을 위한 추가

public class TextSample : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp.text = "가위 바위 보";
        text.text = "하나 빼기";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
