using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	
	///
	[SerializeField] public GameObject stem;
	private List<GameObject> stemList;
	
	/// posições dos troncos
	/// Vector3( xinicial, xfinal, velocidade, delay )
	private Vector4[] stemPositions = {
		new Vector4(40f,  47f, .6f, 0f),
		new Vector4(97.5f, 107f, .6f, 0f),
		new Vector4(160f, 170f, .6f, 2f),
		new Vector4(172f, 182f, .6f, 0f)
	};
	
	///
	private Vector3 backgroundOffset = new Vector3( -1f, 0f, 0f );
	[SerializeField] public GameObject background;
	
	///
	[SerializeField] public GameObject letter;
	public static Vector3[] letterPositions = {
		new Vector3(  3f,   0f, 0f),
		new Vector3( 32f,   1f, 0f),
		new Vector3(125f, 8.5f, 0f),
	//	new Vector3(172f, 2.85f, 0f),
		new Vector3(203f,   7f, 0f)
	};
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
		///
		stemList = new List<GameObject>();
		
		/// inicia troncos
		foreach( var v in stemPositions ) {
			
			GameObject newStem = Instantiate(stem, new Vector3( Random.Range(v.x,v.y), 0f, 0f ), Quaternion.identity);
			
			float flipx = ( Random.Range(0f,1f) > .5f )? 1f : -1f;
			float flipy = ( Random.Range(0f,1f) > .5f )? 1f : -1f;
			
			newStem.transform.localScale = new Vector3( flipx, flipy, 0f );
			
			stemList.Add( newStem );
			
		}
		
		/// inicia troncos
		foreach( var p in letterPositions )
			Instantiate(letter, p, Quaternion.identity);
		
    }

    // Update is called once per frame
    void Update()
    {
        
		/// atualiza troncos
		for( int i = 0; i < stemList.Count; i++ ) {
			
			Vector4 v = stemPositions[i];
			GameObject s = stemList[i];
			
			Vector3 va = new Vector3( v.x, WaterController.waterOffset.y + 4f, 0f );
			Vector3 vb = new Vector3( v.y, WaterController.waterOffset.y + 4f, 0f );
			
			float t = Mathf.PingPong( (Time.time + v.w) * v.z, 1.0f );
			
			s.transform.position = Vector3.Lerp( va, vb, t );
			
		}
		
		/// atualiza o fundo
	//	backgroundOffset.x = transform.position.x * .25f;
		backgroundOffset.y += Time.deltaTime * WaterController.Gain;
		background.transform.position = backgroundOffset;
		
		
    }
}
