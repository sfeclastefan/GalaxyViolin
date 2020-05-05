using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTempo : MonoBehaviour
{
    public float beatTempo;  // beatTempo arata cat de rapid vor cadrea "stelele" pe gatul viorii

    public bool start;  // se foloseste ca la apasarea unui buton "stelele" sa inceapa sa cada pe gatul viorii 


    // Start is called before the first frame updates
    void Start()
    {
        beatTempo = beatTempo / 60f;  //Valoarea default este de 120 beati pe secunda (prea repede). Imparit la 60 pentru a fi 2 biti pe secunda (2 patratele in  Unity).                                   
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        { 
            transform.position = transform.position - new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
