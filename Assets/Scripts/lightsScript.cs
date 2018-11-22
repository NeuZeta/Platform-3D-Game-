using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsScript : MonoBehaviour {

    public GameObject sunLight;
    public Light lampLight;
    public Material litMaterial, unlitMaterial;
    public MeshRenderer Lightmesh;
    private float secondsToLight = 3.0f;

    
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
        Lightmesh.material = litMaterial;

        if (lampLight.intensity < 5.0f)
        {
            lampLight.intensity += 10* Time.deltaTime;
        }
    }

    void TurnOffLights()
    {
        Lightmesh.material = unlitMaterial;

        if (lampLight.intensity > 0)
        {
            lampLight.intensity -= 10 * Time.deltaTime;
        }
    }


}
