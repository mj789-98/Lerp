using UnityEngine;
using Photon.Pun;



public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    public Animator animator;
    PhotonView view;
    public Camera cam;
    
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
         
      
        if(!view.IsMine)
        {
            Destroy(cam);
          
        }
    }
 
    void Update()
    {
        if(view.IsMine){
     
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
         animator.SetBool("IsMoving", moveZ != 0);

        transform.position += transform.forward * moveZ * moveSpeed * Time.deltaTime;
       animator.SetBool("IsMoving", moveZ != 0);
        transform.Rotate(0f, moveX * turnSpeed * Time.deltaTime, 0f);
        
 
        if (Input.GetButtonDown("Jump"))
        {
             animator.SetBool("IsJumping", true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           

        }
         if (Input.GetButtonUp("Jump"))
    {
        animator.SetBool("IsJumping", false);
    }
        
        }
       
    }
    
}

