using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carLightsScript : MonoBehaviour {

    public GameObject sunLight;
    public Light[] lampLights;
    public GameObject lightsMesh;

    private float secondsToLight = 1.0f;

    
    
	void Start () {

	}
	
	void Update () {

        var currentRotationX = Mathf.Atan2(sunLight.transform.up.z, sunLight.transform.up.y) * Mathf.Rad2Deg;
        if (currentRotationX < 0) currentRotationX += 360f;

        if (currentRotationX < 4 || currentRotationX > 164)
        {
            TurnOnLights();
        }
        else
        {
            TurnOffLights();
        }

    }

    void TurnOnLights()
    {
        if (!lightsMesh.activeSelf)
        {
            lightsMesh.SetActive(true);
        }

        foreach (Light lampLight in lampLights)
        {
            if (lampLight.intensity < 5.0f)
            {
                lampLight.intensity += 10 * Time.deltaTime;
            }
        }
        
    }

    void TurnOffLights()
    {
        if (lightsMesh.activeSelf)
        {
            lightsMesh.SetActive(false);
        }

        foreach (Light lampLight in lampLights)
        {
            if (lampLight.intensity > 0.0f)
            {
                lampLight.intensity -= 10 * Time.deltaTime;
            }
        }
    }


}
