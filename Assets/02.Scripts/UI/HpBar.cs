using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Transform myTarget;
    public Slider myBar;
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        if (pos.z < 0.0f)
            transform.position = new Vector3(0, 10000, 0);
        else
            transform.position = pos;
    }
}
