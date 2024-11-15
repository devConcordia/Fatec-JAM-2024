using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	private bool jumping = false;
	
	private Rigidbody2D body;
	private Animator animator;
	
	private Vector3 cameraOffset = new Vector3( 3.5f, 2f, -10f );
	[SerializeField] public Camera mainCamera;
	
	[SerializeField] public float speed = 5;
//	[SerializeField] public float jumpForce = 30f;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		
    }

    // Update is called once per frame
    void Update()
    {
		
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
		
	//	animator.SetFloat("x", input.x);
	//	animator.SetFloat("y", input.y);
		
		if( !jumping ) {
			
			if( y > 0 ) {
				
				animator.SetBool("jump", true);
				
				jumping = true;
				
			//	body.linearVelocity = new Vector2( 20f, 30f );
				body.linearVelocity = new Vector2( 10f, 20f );
				
			} else {
				
				animator.SetBool("jump", false);
				
				body.linearVelocity = new Vector2( x*5f, 0f );
				
			}
			
		} else {
			
			/// debug
			if( x < 0 )
				transform.position = new Vector3(-7f,-2f,0f);
			
		}
				
		mainCamera.transform.position = transform.position + cameraOffset;
		
		
		
    }
	
	
	private void OnCollisionEnter2D(Collision2D collision)
    {
		
        if( collision.gameObject.CompareTag("Ground") ) {
		
			jumping = false;
		   
        }
		
		
    }
	
	
	
}
