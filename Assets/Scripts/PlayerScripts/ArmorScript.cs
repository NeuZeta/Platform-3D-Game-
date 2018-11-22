using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArmorScript : MonoBehaviour {

    public Slider armorBar;
    public GameObject bloodPanel;

    private float totalArmor = 10f;
    public float armor = 10f;

    public AudioSource audioSource;
    public AudioClip[] gruntSounds;

    public Animator anim;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (armor <= 0)
        {
            gameController.instance.gameOver = true;
        }
	}

    public void Attack(float damage)
    {
        armor -= damage;
        if (armor < 0)
        {
            armor = 0f;
        }

        audioSource.PlayOneShot(gruntSounds[Random.Range(0, gruntSounds.Length)]);

        anim.SetTrigger("Pain");

        UpdateArmorBar();
        bloodPanel.SetActive(true);
        Invoke("HideBloodPanel", 0.325f);
    }


    void UpdateArmorBar()
    {
        armorBar.value = armor;
    }


    void HideBloodPanel()
    {
        bloodPanel.SetActive(false);
    }


}
