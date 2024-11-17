using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

///
public class GameManager : MonoBehaviour {
	
	///
	//[SerializeField] public GameObject player;
	
	[SerializeField] public GameObject canvasMainScreen;
	[SerializeField] public GameObject canvasGoodGameScreen;
	[SerializeField] public GameObject canvasGameOverScreen;
	
	
	[SerializeField] public GameObject textCredits;
	
	[SerializeField] public GameObject ggAuthor;
	
	[SerializeField] public GameObject ggTimeMessage;
	private TMP_Text textTimeMessage;
	
	[SerializeField] public GameObject ggSecretMessage;
	private TMP_Text textSecretMessage;
	private string[] secretMessage = {
		"... a primeira ",
		"glória é ",
		"a reparação ",
		"dos erros."
	};
	public static List<Vector3> letterList;
	
	private bool playing = false;
	private bool needRestart = false;
	private float startTime = 0f;
	
	public static GameManager instance = null;
	
	
    private void Awake() {
		
		//Debug.Log("GameManager.Awake");
		
		if( instance != null ) 
            Destroy(instance);
		
        instance = this;
        
    }
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		
		//Debug.Log("GameManager.Start");
		
		playing = false;
		
		canvasMainScreen.SetActive(true);
        Time.timeScale = .0001f;
		
		textTimeMessage = ggTimeMessage.GetComponent<TMP_Text>();
		textSecretMessage = ggSecretMessage.GetComponent<TMP_Text>();
		
	}

    // Update is called once per frame
    void Update() {
		
		if( !playing ) {
		
			if( Input.GetKeyDown(KeyCode.Space) ) {
				if( needRestart ) {
					
					Restart();
					
				} else {
					
					GameStart();
					
				}
			}
		
		}
		
	}
	
	public void GameStart() {
		
		//Debug.Log("GameManager.GameStart");
		
		playing = true;
		
		startTime = Time.time;
		
		ggAuthor.SetActive(false);
		canvasMainScreen.SetActive(false);
        Time.timeScale = 1f;
		
		letterList = new List<Vector3>();
		
	}
	
	public void Restart() {
		
		//Debug.Log("GameManager.Restart");
		
		ggAuthor.SetActive(false);
		SceneManager.LoadScene(0);
        Time.timeScale = 1f;
		
		letterList = new List<Vector3>();
		
	}
	
	public void GoodGame() {
		
		//Debug.Log("GameManager.GoodGame");
		
		playing = false;
		needRestart = true;
		
		canvasGoodGameScreen.SetActive(true);
        Time.timeScale = .0001f;
		
		///
		textSecretMessage.text = "";
		
		for( int k = 0; k < SceneController.letterPositions.Length; k++ ) {
			
			bool captured = false;
			
			foreach( var p in letterList ) {
				if( p.Equals( SceneController.letterPositions[k] ) ) {
					captured = true;
					break;
				}
			}
			
			///
			textSecretMessage.text += ( captured )? secretMessage[k] : " *** ";
			
		}
		
		
		///
		textTimeMessage.text = "Seu tempo foi de "+ (Time.time - startTime).ToString("0") +" segundos.";
		
		
		if( letterList.Count == 4 ) {
			
			ggAuthor.SetActive(true);
		
		} else {
			
			textTimeMessage.text += "\nVocê não pegou todas as cartas.";
			
		}
		
	}
	
	public void GameOver() {
		
		//Debug.Log("GameManager.GameOver");
		
		playing = false;
		needRestart = true;
		
		canvasGameOverScreen.SetActive(true);
        Time.timeScale = .0001f;
		
	}
	
	
	
	public void ToggleCredits() {
		
		textCredits.SetActive( !textCredits.activeSelf );
		
	}
	
	
}
