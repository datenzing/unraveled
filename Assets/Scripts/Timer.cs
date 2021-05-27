using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text timerLabel;
    private float time;

    void Start(){
    	timerLabel = gameObject.GetComponent<TMP_Text>();
    	// time = 50f;

    }

    // Update is called once per frame
    void Update()
    {
    	time += Time.deltaTime;

    	var minutes = Mathf.FloorToInt(time / 60);
    	var seconds = Mathf.FloorToInt(time % 60);
    	var fraction = (time*100) % 100;

        timerLabel.text = string.Format("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
    }
}
