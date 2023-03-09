using UnityEngine;
using UnityEngine.UI;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNPC;
    public AudioClip hi;
    public AudioSource mySource;
    public Image talkImage;
    public Image image;
    private void Start()
    {
        if (talkImage != null)
        {
            image = Instantiate(talkImage, SceneData.Inst.NpcImage.transform);
            image.gameObject.SetActive(false);
        }
    }
    public void OnTalk()
    {
        if (hi != null)
            SoundManager.Inst.PlayOneShot(mySource, hi);

    }
    private void Update()
    {
        if (image != null)
        {
            Collider[] list = Physics.OverlapSphere(transform.position, 5.0f, 1 << LayerMask.NameToLayer("Player"));
            if (list.Length == 0)
            {
                image.gameObject.SetActive(false);
            }
            else
            {
                OnImage();
            }
        }
    }
    void OnImage()
    {
        Vector3 myPos = transform.position;
        myPos.y += 1.5f;
        Vector3 pos = Camera.main.WorldToScreenPoint(myPos);

        image.gameObject.SetActive(true);
        if (pos.z < 0.0f)
        {
            image.transform.position = new Vector3(0, 10000, 0);
        }
        else
        {
            image.transform.position = pos;
        }
    }
}
