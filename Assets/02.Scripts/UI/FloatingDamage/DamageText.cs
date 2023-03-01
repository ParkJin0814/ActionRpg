using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float alphaSpeed;
    private float destroyTime;
    TMP_Text text;
    Color alpha;
    public int damage;
    public Transform myTarget;

    void Start()
    {
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<TMP_Text>();
        alpha = text.color;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }
    void Update()
    {
        //transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        if (pos.z < 0.0f)
        {
            transform.position = new Vector3(0, 10000, 0);
        }
        else
        {
            transform.position = pos;
        }
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}