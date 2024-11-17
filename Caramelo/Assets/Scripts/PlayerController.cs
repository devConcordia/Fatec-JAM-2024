using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody2D body;
	private Animator animator;
	
	[SerializeField] private GameObject gallopSound;
	private AudioSource gallopSource;
	
	[SerializeField] private GameObject itemPickupSound;
	private AudioSource itemPickupSource;
	
	
	/// -1: left
	/// 1: right
	private int direction = 1;
	private float scale = 2;
	private bool jumping = false;
	[SerializeField] public float speed = 8f;
	
	//private Transform stemTransform = null;
	
	///
	private float tutorialTime = 10f;
	[SerializeField] public GameObject canvasTutorial;
	
	//
	private Vector3 cameraOffset = new Vector3( 3.5f, -.5f, -10f );
	[SerializeField] public Camera mainCamera;
	
	/// [a primeira] [glória] [é a] [reparação dos] [erros]
	//public List<Vector3> letterList;
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		
		//Debug.Log("Player.Start");
		
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		
        //gallopSource = gallopSound.GetComponent<AudioSource>();
		//itemPickupSource = itemPickupSound.GetComponent<AudioSource>();
		
        gallopSource = (Instantiate(gallopSound, new Vector3(0f,0f,0f), Quaternion.identity)).GetComponent<AudioSource>();
		itemPickupSource = (Instantiate(itemPickupSound, new Vector3(0f,0f,0f), Quaternion.identity)).GetComponent<AudioSource>();
		
		//letterList = new List<Vector3>();
		
		/// esconde tutorial 
		StartCoroutine(HideTutorialAfterTime());
		
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
	
		/// se o player estiver em cima de um tronco, deverá acompanha sua movimentação
	//	if( stemTransform != null ) {
	//		transform.SetPositionAndRotation( 
	//			new Vector3( stemTransform.position.x, transform.position.y, 0f), 
	//			Quaternion.identity );
	//	}
		
	//	animator.SetFloat("run", x);
		
		///
		if( x != 0 ) {
			
			animator.SetBool("running", true);
			body.linearVelocityX = x * speed + WaterController.waterFlow;
		
		} else {
			
			body.linearVelocityX = WaterController.waterFlow;
			
			animator.SetBool("running", false);
			
			if( gallopSource.isPlaying )
				gallopSource.Stop();
			
		}
		
		if( !jumping ) {
			if( y > 0 ) {
				
				jumping = true;
				animator.SetBool("jump", true);
				animator.SetBool("falling", false);
				
				body.linearVelocityY = 20f;
				
				if( gallopSource.isPlaying )
					gallopSource.Stop();
				
			} else {
				if( !gallopSource.isPlaying )
					gallopSource.Play();
			}
		}
		
		if( body.linearVelocity.y < 0 ) {
			
			animator.SetBool("falling", true);
			
		} else {
			
			animator.SetBool("falling", false);
			
		}
		
		
		/// atualiza camera acompanhando o nivel da agua e player
	//	cameraOffset.x = transform.position.x;
	//	cameraOffset.y += Time.deltaTime * WaterController.Gain;
	//	mainCamera.transform.position = cameraOffset;
		
		/// atualiza camera acompanhando o player
		mainCamera.transform.position = cameraOffset + transform.position;
		
    }
	
    IEnumerator HideTutorialAfterTime() {
		
        yield return new WaitForSeconds(tutorialTime);
        
		canvasTutorial.SetActive(false);
		
    }
	
	///
	private void OnCollisionEnter2D(Collision2D collision) {
		
        if( collision.gameObject.CompareTag("Ground") ) {
		
			jumping = false;
			animator.SetBool("jump", false);
		   
        } else if( collision.gameObject.CompareTag("Stem") ) {
			
			jumping = false;
			animator.SetBool("jump", false);
			
		//	stemTransform = collision.gameObject.transform;
			
        } else if( collision.gameObject.CompareTag("Water") ) {
			
			GameManager.instance.GameOver();
			
		} else if( collision.gameObject.CompareTag("Helicopter") ) {
			
			GameManager.instance.GoodGame();
			
		} else if( collision.gameObject.CompareTag("Letter") ) {
			
			//Debug.Log( "Add.Letter" );
			
			itemPickupSource.Play();
			GameManager.letterList.Add( collision.gameObject.transform.position );
		//	letterList.Add( collision.gameObject.transform.position );
		//	letterList.Add(new Vector3(
		//		collision.gameObject.transform.position.x,
		//		collision.gameObject.transform.position.y,
		//		collision.gameObject.transform.position.z
		//	));
			
			Destroy( collision.gameObject );
			
		}
		
    }
	
//	private void OnCollisionExit2D(Collision2D collision) {
//		
//        if( collision.gameObject.CompareTag("Stem") ) {
//			
//		//	stemTransform = null;
//			
//		}
//		
//		
//    }

}
