using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUIManager : MonoBehaviour
{
    private SpriteRenderer theSR; // butonul este un obiect de tipul SpriteRenderer
    public Sprite imagineaPrincipala; // in sectiunea "Sprite" se incarca imaginea
    public Sprite imagineaApasata;

    public KeyCode tastaApasata; // tasta pe care vrem sa o apasam
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>(); // variabila theSR preia componentele obiectului buton
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(tastaApasata))
        {
            theSR.sprite = imagineaApasata;
        }

        if(Input.GetKeyUp(tastaApasata))
        {
            theSR.sprite = imagineaPrincipala;
        }
    }
}
