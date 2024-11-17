using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
	
	private AudioSource audioSource;
	
	///
	private Vector3 finalPosition = new Vector3(222f,8f,0f);
	private float moveHilicopterTime = 3f;
	private Rigidbody2D helicopterBody;
	
	
	private float amplitudeMotion = 0.5f;
    private float speedMotion = 1f;
	private Vector3 referencePosition;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
		audioSource = GetComponent<AudioSource>();
		
		/// move helicoptero para o final da fase
		helicopterBody = GetComponent<Rigidbody2D>();
		
		helicopterBody.linearVelocityX = 10f;
		helicopterBody.linearVelocityY = 10f;
		
		StartCoroutine(MoveHelicopterAfterTime());
		
		referencePosition = transform.position;
		
    }

    // Update is called once per frame
    void Update()
    {
        
		if( helicopterBody.linearVelocity.magnitude == 0 ) {
			
			// Calcula o movimento baseado em uma onda senoidal
			float xOffset = Mathf.Sin(Time.time * speedMotion) * amplitudeMotion;
			float yOffset = Mathf.Sin(Time.time * speedMotion) * amplitudeMotion;
			
			transform.position = referencePosition + new Vector3(xOffset, yOffset, 0);
		
		}
		
    }
	
	
	///
    IEnumerator MoveHelicopterAfterTime() {
		
        yield return new WaitForSeconds(moveHilicopterTime);
        
		helicopterBody.linearVelocityX = 0f;
		helicopterBody.linearVelocityY = 0f;
		
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.position = finalPosition;
		referencePosition = finalPosition;
		
		
    }
	
	// Chamado automaticamente quando o objeto se torna visível pela câmera
    void OnBecameVisible()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Chamado automaticamente quando o objeto deixa de ser visível pela câmera
    void OnBecameInvisible()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
	
}
