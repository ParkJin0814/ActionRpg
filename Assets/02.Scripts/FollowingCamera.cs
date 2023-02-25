using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public LayerMask crashMask;
    public Transform myCam;
    public float LookupSpeed = 10.0f;
    public float ZoomSpeed = 3.0f;
    public float Offset = 0.5f;
    Vector3 curRot = Vector3.zero;
    Vector3 camPos = Vector3.zero;
    float desireDistance = 0.0f;
    Player myPlayer;
    [SerializeField] VariableJoystick joy;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponentInParent<Player>();
        curRot.x = transform.localRotation.eulerAngles.x;
        curRot.y = transform.parent.localRotation.eulerAngles.y;

        camPos = myCam.localPosition;

        desireDistance = camPos.z;
    }

    // Update is called once per frame
    void Update()
    {

        if (myPlayer.OnLive() && !GetComponentInParent<Animator>().GetBool("IsRolling"))
        {
            curRot.y += joy.Horizontal * LookupSpeed;
            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }
        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, -camPos.z + Offset + 0.1f, crashMask))
        {
            camPos.z = -hit.distance + Offset;
        }
        else
        {
            camPos.z = Mathf.Lerp(camPos.z, desireDistance, Time.deltaTime * 3.0f);
        }
        myCam.localPosition = camPos;
    }

}
