using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;



public class ThirdPersonController : MonoBehaviour
{
   public Text nameDisplay;
    //public TextMeshProUGUI textMesh;
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    public Animator animator;
    PhotonView view;
    public Camera cam;
    Health healthScript;

    LineRenderer rend;
    
    
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
        healthScript = FindObjectOfType<Health>();
        rend = FindObjectOfType<LineRenderer>(); 

        if(view.IsMine)
        {
            nameDisplay.text = PhotonNetwork.NickName;
        }
        else
        {
            nameDisplay.text = view.Owner.NickName;
        }
         
      
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

        rend.SetPosition(0, transform.position); 
 
        if (Input.GetButtonDown("Jump"))
        {
             
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
                animator.SetBool("IsJumping", true);
           

        }
         if (Input.GetButtonUp("Jump"))
    {
        animator.SetBool("IsJumping", false);
    }
        
        }
        else {
            rend.SetPosition(1, transform.position);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (view.IsMine)
        {
            if (collision.gameObject.tag == "Enemy")
            {
               healthScript.TakeDamage();
            }
        }
    }

   
}

