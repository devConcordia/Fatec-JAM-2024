using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody2D body;
	private Animator animator;
	
	/// -1: left
	/// 1: right
	private int direction = 1;
	private float scale = 2;
	private bool jumping = false;
	[SerializeField] public float speed = 8f;
	
	private Vector3 cameraOffset = new Vector3( 3.5f, 0f, -10f );
	[SerializeField] public Camera mainCamera;
	
	private Vector3 waterOffset = new Vector3( -15f, -8f, 0f );
	public float waterFlow = 0f;
	[SerializeField] public GameObject water;
	[SerializeField] public float waterGain = .05f;
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		
    }

    // Update is called once per frame
    void Update() {
		
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
		
		/// inverte a direção do player
		/// lembrando que x pode ser 0
		if( x < 0 ) direction = -1;
		else if( x > 0 ) direction = 1;
		///
		transform.localScale = new Vector3(direction * scale, scale, scale);
		
		
		///
		if( x != 0 ) {
			
			body.linearVelocityX = x * speed + waterFlow;
		
		} else {
			
			body.linearVelocityX = waterFlow;
			
		}
		
		
		
		
		if( !jumping ) {
			if( y > 0 ) {
				
				jumping = true;
				animator.SetBool("jump", true);
				animator.SetBool("falling", false);
				
				body.linearVelocityY = 20f;
				
			}
		}
		
		if( body.linearVelocity.y < 0 ) {
			
			animator.SetBool("falling", true);
			
		} else {
			
			animator.SetBool("falling", false);
			
		}
		
		///
	//	waterOffset.x += transform.position.x;
		waterOffset.y += Time.deltaTime * waterGain;
		water.transform.position = new Vector3( waterOffset.x + transform.position.x, waterOffset.y, 0f );
		
		///
		cameraOffset.x = transform.position.x;
		cameraOffset.y += Time.deltaTime * waterGain;
		mainCamera.transform.position = cameraOffset;
		
    }
	
	
	private void OnCollisionEnter2D(Collision2D collision)
    {
		
        if( collision.gameObject.CompareTag("Ground") ) {
		
			jumping = false;
			animator.SetBool("jump", false);
		//	animator.SetBool("falling", false);
		   
        } else if( collision.gameObject.CompareTag("Water") ) {
			
			waterFlow = -1f;
			
		}
		
    }
	
	private void OnCollisionExit2D(Collision2D collision)
    {
		
        if( collision.gameObject.CompareTag("Water") ) {
			
			waterFlow = 0f;
			
		}
		
    }
	
	
	
}
