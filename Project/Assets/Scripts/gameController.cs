using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Cameras;
using UnityEngine;

public class gameController : MonoBehaviour {

    public static gameController instance;
    public bool gameOver = false;
    public GameObject player;  
    public GameObject boyHead, boyHand;
    public Color zombieColor;
    public FreeLookCam myCamera;
    public GameObject gameOverPanel;

    Material headMaterial, handsMaterial;

    private void Awake()
    {
        instance = this;
       
    }

    // Use this for initialization
    void Start () {
        headMaterial = boyHead.GetComponent<SkinnedMeshRenderer>().material;
        handsMaterial = boyHand.GetComponent<SkinnedMeshRenderer>().material;  
    }
	
	// Update is called once per frame
	void Update () {
		
        if (gameOver)
        {
            ZombifyPlayer();
            Invoke("ShowGameOverPanel", 2);
            
        }

	}

    public void ZombifyPlayer()
    {
        player.GetComponent<Animator>().SetBool("Zombie", true);
        player.GetComponent<ThirdPersonUserControl>().enabled = false;
        player.gameObject.tag = "Zombified";
        headMaterial.color = Color.Lerp(Color.white, zombieColor, 10);
        handsMaterial.color = Color.Lerp(Color.white, zombieColor, 10);
    }


    void ShowGameOverPanel()
    {
        myCamera.enabled = false;

        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
