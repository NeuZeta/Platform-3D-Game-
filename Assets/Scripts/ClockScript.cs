using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour {

    public DayNightManager timeManager;
    public float rotationTime = 5.0f;
    public Transform gameAreaCenter;
    public float gameAreaSizeX = 50;
    public float gameAreaSizeZ = 50;
    public AudioClip spellSound;

    private void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update () {

        this.transform.Rotate(0, 360 * Time.deltaTime / rotationTime, 0);

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(spellSound, transform.position, 4f);
            Respawn();
        }
    }

    void Respawn()
    {
        this.transform.position = new Vector3  (gameAreaCenter.position.x + Random.Range(-gameAreaSizeX / 2.0f, gameAreaSizeX / 2.0f),
                                                50,
                                                gameAreaCenter.position.z + Random.Range(-gameAreaSizeZ / 2.0f, gameAreaSizeZ / 2.0f));

        RaycastHit hit;
        if (Physics.Raycast(new Ray(this.transform.position, Vector3.down), out hit, 100f))
        {
            this.transform.position =  new Vector3 (hit.point.x, hit.point.y + 2f, hit.point.z);
        }

        timeManager.Reset();
    
    }





}//ClockScript
