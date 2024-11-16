using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	
    public GameObject canvasMainScreen;
	private bool playing = false;
	
	
	public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start() {}

    // Update is called once per frame
 //   void Update() {}
	
	void GameStart() {
		
		
		
	}
	
	void GameOver() {
		
	}
	
	
	
}
