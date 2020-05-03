using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    //movement
    float speed = 20f;
    public float jump = 1.4f;
    public bool isjumping = false;
    public CharacterController controller;
    CharacterController characterCollider;
    bool crouch = false;
     
    
    public GameObject cameraObj;
    public Transform Standing, Ducking;
    public bool run = false;
    
    


    //Gravity
    public float gravity = -9.81f;
    
    public Vector3 velocity;
    
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool isGrounded2;
    bool isFreemovement = false;
   
    //FlightMovemnt
    public float flight = 10f;
    public bool useFlight = false;
    uint spaceCount = 0;
    const uint reset = 0;
    float time = 0f;
    public float runTime = 0f;
    public float duration = 0.25f;
    public float runDur = 0.5f;
    public bool running = false;
    public float runSpeed = 6f;
    public float NorSpeed = 5f;   
   
    //Wall Run
    public Transform wallCheck;
    public float wallDistance = 0.3f;
    public LayerMask wallMask;
    
    public bool isJumpfree = false;
    public bool isJump = false;
    private void Start()
    {
        Instance = this;
        isGrounded = true;
        
}
    // Start is called before the first frame update
    void Update()
    {
        if (NorSpeed >= 0f)
        {
            speed = NorSpeed;
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {

            running = true;

        }

        characterCollider = gameObject.GetComponent<CharacterController>();
        
        
        



        if (spaceCount > 0)
        {
            time += Time.deltaTime;
            if (time >= duration)
            {
                time = reset;
                ResetFlightValue();
                
            } 
        }


        
        
            if (isGrounded2 && Input.GetKeyDown(KeyCode.LeftShift))
            {

                switch (crouch)
                {
                    case false:
                        crouch = true;
                        transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
                        break;
                    case true:
                        crouch = false;
                        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
                        break;
                }
            } 
        
        
        if (allowed == true)
        {
            if (running == true)
            {
                speed = runSpeed;
                run = true;

            }
            if (run == true)
            {
                runTime += Time.deltaTime;
                if (runTime >= runDur)
                {
                    runTime = reset;
                    ResetRunTime();
                }
            } 
        }

        if (coolDownTime == true)
        {
            coolTime += Time.deltaTime;
            if (coolTime >= coolDur)
            {

                ResetCoolDown();
                coolTime = reset;

            } 
        }




    }
    
    




    // Update is called once per frame
    void FixedUpdate()
    {
        isjumping = Input.GetButtonDown("Jump");

        if (isGrounded == false)
        {
            isjumping = true;
            isJumpfree = true;
            
            
            

        }
        else if (isGrounded == true)
        {
            isjumping = false;
            isJumpfree = false;
            
        }
       

        //BasicMovemnt
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        controller.Move(move * speed * Time.fixedDeltaTime);
                
       


        if (isFreemovement == false)
        {
            //Gravity
            RaycastHit hit;
            isGrounded = Physics.Raycast(groundCheck.position, -groundCheck.TransformDirection(Vector3.up), groundDistance);

            Debug.DrawRay(groundCheck.position, -groundCheck.TransformDirection(Vector3.up), Color.red);




            {
                if (isGrounded && velocity.y < 0)
                {
                    controller.slopeLimit = 45.0f;
                    velocity.y = -2f;
                }
            }


            if (Input.GetButtonDown("Jump") && spaceCount < 2)
            {
                spaceCount++;
            }
            else if (spaceCount == 2)
            {
                isFreemovement = true;
                ResetFlightValue();
                return;
            }
            if (Input.GetButton("Jump") && spaceCount < 2)
            {
                
                if (isGrounded)
                {                   
                    controller.slopeLimit = 100.0f;
                    velocity.y = Mathf.Sqrt(jump * -2f * gravity);
                }

            }

            

            velocity.y += gravity * Time.fixedDeltaTime;

            controller.Move(velocity * Time.fixedDeltaTime);
        }
        else
        {
            float acend = Input.GetKey(KeyCode.Space) ? 1 : 0;
            float decend = Input.GetKey(KeyCode.LeftShift) ? -1 : 0;
            Vector3 flightMove = (transform.up * (acend + decend)).normalized;
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(flightMove * flight * Time.fixedDeltaTime);
            }
            if (Input.GetButtonDown("Jump") && spaceCount < 2)
            {
                spaceCount++;
            }
            else if (spaceCount == 2)
            {
                isFreemovement = false;
                ResetFlightValue();
                return;
            }

            


        }

    if (isjumping)
        {
            runSpeed = 32f;   
        }
    if (isjumping == false)
        {
            runSpeed = 20f;
            
        }


        


        if (isjumping)
        {
            RaycastHit hit;
            isJump = Physics.Raycast(wallCheck.position, -wallCheck.TransformDirection(Vector3.left), wallDistance);

            Debug.DrawRay(wallCheck.position, -wallCheck.TransformDirection(Vector3.left), Color.red);



            if (isJump)
            {
                Debug.Log("Jumping next to wall");
            }
           
            
        }


        
        



    }
    private void ResetFlightValue()
    {
        isjumping = false;
        spaceCount = reset;
    }
    private void ResetRunTime()
    {
        coolDown = true;
        CoolDown();
        running = false;
        run = false;
        speed = NorSpeed;
    }
    float coolTime = 0f;
    public float coolDur = 3f;
    bool coolDown = false;
    public bool allowed = true;
    bool coolDownTime = false;
    void CoolDown()
    {
        if (coolDown == true)
        {
            allowed = false;
            running = false;
            
            coolDownTime = true;
        }
    }
    void ResetCoolDown()
    {
        allowed = true;
        coolDown = false;
        coolDownTime = false;
    }
   

     

}
