using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// verificam cand stelele intra in perimetrul butoanelor si daca intra acestea vor putea fi apasate

public class StarUIManager : MonoBehaviour
{
    public bool poateFiApasat;

    public KeyCode tastaDeApasat;

    public GameObject nimeritEfect, ratatEfect, perfectEfect;

    

    public AudioSource notaPierduta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(tastaDeApasat)) // tasta setata notelor corespunzatoare butonului care trebuie apasat
        {
            if(poateFiApasat)
            {
                gameObject.SetActive(false);
                /* GameManager.instanta.NotaNimerita(); */

                
                 if(Mathf.Abs(transform.position.y)>=64.3)
                 {
                    GameManager.getInstanta().song.mute = false;
                    GameManager.getInstanta().notaNormala();       
                    var v3 = transform.position;
                    v3.x = 300f;
                    v3.y = 66f;
                    Instantiate(nimeritEfect, v3, nimeritEfect.transform.rotation);           
                  }

                else if(Mathf.Abs(transform.position.y) >= 63.9)
                {
                    GameManager.getInstanta().song.mute = false;
                    GameManager.getInstanta().notaBuna();
                    var v3 = transform.position;
                    v3.x = 300f;
                    v3.y = 66f;
                    Instantiate(nimeritEfect, v3, nimeritEfect.transform.rotation);

                }
                else
                {
                    GameManager.getInstanta().song.mute = false;
                    GameManager.getInstanta().notaPerfecta();
                    var v3 = transform.position;
                    v3.x = 267f;
                    v3.y = 73f;
                    Instantiate(perfectEfect, v3, perfectEfect.transform.rotation);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)     // Cand obiectul de tip collider intra in zona trigger a butoanelor si daca acestea au atribuite tagul "Activator" se va bifata casuta "Poate fi apasat"
                                                        // Collider2D = obiectul din raza notelor si butoanelor
    {
        if(other.tag == "Activator")    // Activator = tagul atribuit notelor 
        {
            poateFiApasat = true;
        }
    }

   
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            poateFiApasat = false;
            if (gameObject.activeInHierarchy)   // verifica daca obiectul mai este activ (daca steaua a fost capturata devine inactiva deci nu mai este considerata "ratata")
            {
                GameManager.getInstanta().NotaRatata();
                var v3 = transform.position;
                v3.x = 295f;
                v3.y = 73f;
                Instantiate(ratatEfect, v3, ratatEfect.transform.rotation);
                GameManager.getInstanta().song.mute = true;
                notaPierduta.Play();
                StartCoroutine("stopMute");

            }
            

        }
    }

    IEnumerator stopMute()
    {
        yield return new WaitForSeconds(1);
        GameManager.getInstanta().song.mute = false;

    }


}

