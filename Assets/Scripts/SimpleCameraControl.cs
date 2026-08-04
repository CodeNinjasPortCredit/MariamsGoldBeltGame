using UnityEngine;

public class SimpleCameraControl : MonoBehaviour
{
    public float speed = 7.5f, jumpSpeed = 8f, gravity = 20f;
    public Camera cam; public float lookSpeed = 2f, lookXLimit = 45f;
    Vector3 moveDir = Vector3.zero; float rotX = 0;
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool run = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (run ? speed * 1.5f : speed) * Input.GetAxis("Vertical");
        float curSpeedY = (run ? speed * 1.5f : speed) * Input.GetAxis("Horizontal");
        float y = moveDir.y;
        moveDir = (fwd * curSpeedX) + (right * curSpeedY);
        rotX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotX = Mathf.Clamp(rotX, -lookXLimit, lookXLimit);
        transform.position += moveDir * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            transform.position.y));
        Vector3 forward = mouseWorld - transform.position;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}