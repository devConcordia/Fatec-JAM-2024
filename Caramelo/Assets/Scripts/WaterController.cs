using UnityEngine;

public class WaterController : MonoBehaviour
{
	
	///
	public static Vector3 waterOffset;// = new Vector3( -15f, -8f, 0f );
	public static float waterFlow = 0f;
	[SerializeField] public GameObject horse;
	//[SerializeField] public GameObject water;
	[SerializeField] public static float Gain = .075f;

	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
		waterOffset = new Vector3( -15f, -8f, 0f );
		
	}

	// Update is called once per frame
	void Update()
	{
        
		/// atualiza nivel da agua
		waterOffset.y += Time.deltaTime * Gain;
		transform.position = new Vector3( waterOffset.x + horse.transform.position.x, waterOffset.y, 0f );
		
	}
	
}
