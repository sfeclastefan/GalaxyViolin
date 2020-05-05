using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterUIManager : MonoBehaviour
{

    public static CounterUIManager instantaa;

    float currentTime = 0f;
    float startingTime = 5f;

    public float finished = 0;

    public bool startSunetCron = false;

    [SerializeField] private AudioSource cronometruSound;

    // Start is called before the first frame update
    void Start()
    {
        instantaa = this;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(finished==0)
        {
            if(startSunetCron==false)
            {
                cronometruSound.Play();
                startSunetCron = true;
            }
            currentTime -= 1 * Time.deltaTime;
            GetComponent<TextMeshProUGUI>().text = currentTime.ToString("0");

        }
        if (currentTime<=0 && finished==0)
        {
            finished = 1;
            GetComponent<TextMeshProUGUI>().text = "START";
        }

        
    }
}

