using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] Slot[] quickSlots;  // Äü½½·Ô
    [SerializeField] Transform tf_parent;  // Äü½½·ÔµéÀÇ ºÎ¸ð ¿ÀºêÁ§Æ®   

    [SerializeField] ItemEffectDatabase myItemEffectDatabase;



    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EatItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            EatItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            EatItem(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            EatItem(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            EatItem(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            EatItem(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            EatItem(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            EatItem(7);
    }
    public void EatItem(int _num)
    {
        if (quickSlots[_num].item == null) return;
        myItemEffectDatabase.UseItem(quickSlots[_num].item);
        quickSlots[_num].SetSlotCount(-1);
    }

}
