using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    Animator playerAnim;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
  //  public Transform camFollowingObj; //Object at which our camera is going to look at
  //  Vector3 camPosx;
  
    public Joystick joystick;
    public FixedTouchField touchField;
    public FixedButton fixedButton;

    public CinemachineFreeLook TPcamera;
  //  protected float CameraAnglex;

    protected float CameraAngleSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = this.gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        
        //Debug.Log(touchField.TouchDist.x);
        //camera movement
        //{  CameraAnglex += touchField.TouchDist.x * CameraAngleSpeed;
      
        TPcamera.m_XAxis.m_InputAxisValue = touchField.TouchDist.x * CameraAngleSpeed;
        TPcamera.m_YAxis.m_InputAxisValue = touchField.TouchDist.y * CameraAngleSpeed;
        calculateMovement();


        //{ camPosx = transform.position + Quaternion.AngleAxis(CameraAnglex, Vector3.up) * new Vector3(0, 3, -7);
      

      
        //{ camFollowingObj.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - camPosx, Vector3.up);
  
        
        if (fixedButton.Pressed)
        {
            buttonPressed();
        }
    }

    public void buttonPressed() {
        Debug.Log("HI, you pressed me");
    }

    void calculateMovement()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        playerAnim.SetFloat("PlayerMove", vertical);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
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
