using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    Animator playerAnim;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public Transform camFollowingObj; //Object at which our camera is going to look at
    Vector3 camPosx;
   // Vector3 camPosy;

    public Joystick joystick;
    public FixedTouchField touchField;
    protected float CameraAnglex;
  //  protected float CameraAngley;

    protected float CameraAngleSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = this.gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        playerAnim.SetFloat("PlayerMove", vertical);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //camera movement
        CameraAnglex += touchField.TouchDist.x * CameraAngleSpeed;
       // CameraAngley += touchField.TouchDist.y * CameraAngleSpeed;

        //  Debug.Log(CameraAngle);
        //  Debug.Log(cam.position);

        camPosx = transform.position + Quaternion.AngleAxis(CameraAnglex, Vector3.up) * new Vector3(0, 3, -7);
        //camPosy = transform.position + Quaternion.AngleAxis(CameraAngley, Vector3.right) * new Vector3(0, 3, -7);

        //  Debug.Log(camPos);
        Debug.Log(camFollowingObj.rotation);
        camFollowingObj.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - camPosx, Vector3.up);
       // camFollowingObj.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - camPosy, Vector3.right);

        Debug.Log(camFollowingObj.rotation);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
