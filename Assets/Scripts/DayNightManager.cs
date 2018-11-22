using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightManager: MonoBehaviour {

    public GameObject directional;
    public float totalSecondsTo16Hours = 90;
    public float startDayRotationX = 0;
    public float endDayRotationX = 270;
    public float currentRotationX;
    public Transform startDayRotation;
    public Text timeText;
    public Text dayText;

    private float elapsedTime = 0;
    private int dayNumber = -1;

    void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {

        if (!gameController.instance.gameOver)
        {
            elapsedTime += Time.deltaTime;

            float rot = (endDayRotationX - startDayRotationX) / totalSecondsTo16Hours;

            directional.transform.Rotate(-rot * Time.deltaTime, 0, 0);

            //debug
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }

            SetTimeText();

            if (directional.transform.rotation.eulerAngles.x == endDayRotationX)
            {
                gameController.instance.gameOver = true;
            }
        }

    }

    public void Reset()
    {
        totalSecondsTo16Hours--;
        elapsedTime = 0;

        directional.transform.rotation = startDayRotation.rotation;

        dayNumber++;
        SetDayText();

        //directional.transform.rotation = Quaternion.Euler(startDayRotationX, 30, 0);
    }


    void SetTimeText()
    {
        float secondsXHour = totalSecondsTo16Hours / 16.0f;
        float secondsLeft = totalSecondsTo16Hours - elapsedTime;
        int hour = (int)((totalSecondsTo16Hours - secondsLeft) / secondsXHour);
        int minutes = (int)((((totalSecondsTo16Hours - secondsLeft) / secondsXHour) - hour) * 60);

        string label;

        if ((hour + 8) > 12)
        {
            label = (hour + 8 - 12).ToString("00") + ":" + minutes.ToString("00") + "F G";
        }
        else
        {
            label = (hour + 8).ToString("00") + ":" + minutes.ToString("00") + "E G";
        }

        timeText.text = label;

    }

    public void SetDayText()
    {
        dayText.text = dayNumber.ToString();
    } 





}
