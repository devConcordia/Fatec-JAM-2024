using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody2D body;
	private Animator animator;
	
	/// -1: left
	/// 1: right
	private int direction = 1;
	private float scale = 2;
	private bool jumping = false;
	
	private Vector3 cameraOffset = new Vector3( 3.5f, 2f, -10f );
	[SerializeField] public Camera mainCamera;
	
	[SerializeField] public float speed = 5;
//	[SerializeField] public float jumpForce = 30f;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		
    }

    // Update is called once per frame
    void Update() {
		
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
		
		/// lembrando que x pode ser 0
		if( x < 0 ) direction = -1;
		else if( x > 0 ) direction = 1;
		
		///
		transform.localScale = new Vector3(direction * scale, scale, scale);
		
		
		if( !jumping ) {
			
			if( y > 0 ) {
				
				animator.SetBool("jump", true);
				
				jumping = true;
				
			//	body.linearVelocity = new Vector2( 20f, 30f );
				body.linearVelocity = new Vector2( direction * 10f, 20f );
				
			} else {
				
				animator.SetBool("jump", false);
				
				body.linearVelocity = new Vector2( x*5f, 0f );
				
			}
			
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
