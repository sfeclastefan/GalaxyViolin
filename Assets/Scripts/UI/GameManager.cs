using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject panelCronometru;

    [SerializeField] private GameObject gameCanvas;

    
     public AudioSource song;
    [SerializeField] private bool startPlaying;
    [SerializeField] private BeatTempo theBT;

    private static GameManager instanta;   // creem o instanta a clasei GameManager de tip static pentru a o putea folosi in clasa NoteObject. Folosim design pattern-ul Singleton pentru a avea aceeasi instanta de fiecare data (pentru a fi acelasi obiect).

    private int scorCurent;
    private readonly int scorPerNota = 10;
    private readonly int scorNotaBuna = 12;
    private readonly int scorNotaPerfecta = 15;

    [SerializeField] private TextMesh scorText;
    [SerializeField] private TextMesh putere;

    private int putereCurenta;
    private int putereTracker;
    private int[] putereNivele;

    [SerializeField] private GameObject highScoreTable;


    private float noteTotal;
    private float noteNormale;
    private float noteBune;
    private float notePerfecte;
    private float noteRatate;

    [SerializeField] private GameObject inputWindow;
    [SerializeField] private GameObject fereastraRezultate;
    [SerializeField] private TextMesh procentajText, nimeritText, bineText, perfectText, pierdutText, calificativText, scorFinalText;
    private bool entered=false;


    public static GameManager getInstanta()
    {
        return instanta;
    }

    // Start is called before the first frame update
    void Start()
    {

        highScoreTable.SetActive(false);
        instanta = this;  // instanta trebuie inalizata, altfel va fi nula si nu poate fi folosita

        scorText.text = "Scor  0";
        putereCurenta = 1;

        noteTotal = FindObjectsOfType<StarUIManager>().Length;

        putereNivele = new int[3];
        putereNivele[0] = 8;
        putereNivele[1] = 16;
        putereNivele[2] = 24;
    }

    public int getScor()
    {
        return scorCurent;
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(1);
        panelCronometru.SetActive(false);
        startPlaying = true;
        theBT.start = true; // notele incep sa cada
        song.Play();
        gameCanvas.transform.Find("Scor&Putere").gameObject.SetActive(true);
        gameCanvas.transform.Find("ButoaneVioara").transform.Find("IndicatiiButoane").gameObject.SetActive(true);
        yield return new WaitForSeconds(9);
        gameCanvas.transform.Find("ButoaneVioara").transform.Find("IndicatiiButoane").gameObject.SetActive(false);
    }

    IEnumerator endGame()
    {
      
        yield return new WaitForSeconds(3);
        fereastraRezultate.SetActive(true);
        gameCanvas.SetActive(false);

        nimeritText.text = "" + noteNormale;
        bineText.text = "" + noteBune;
        perfectText.text = "" + notePerfecte;
        pierdutText.text = "" + noteRatate;

        float totalPrinderi = noteNormale + noteBune + notePerfecte;
        float procentPrinderi = (totalPrinderi / noteTotal) * 100f;

        procentajText.text = procentPrinderi.ToString("F1") + "%"; // pentru a converti valoarea in float cu o singura decimala
        
        string calificativ = "Din pacate muzica nu este punctul tau forte";

        if (procentPrinderi > 35)
        {
            calificativ = "Exista potential dar mai este nevoie de antrenament";
            if (procentPrinderi > 60)
            {
                calificativ = "Daca vei munci suficient te vei descurca cu un instrument real ";
                if (procentPrinderi > 75)
                {
                    calificativ = "Felicitari! Ar trebui sa te apuci de un instrument real";
                   
                }
            }
        }
     
        
        calificativText.text = calificativ;
        scorFinalText.text ="Scor final " +  scorCurent.ToString();

        if (HighscoreUIManager.getInstance().isNewHighscore(scorCurent))
        {
            inputWindow.SetActive(true);
            scorFinalText.text += "\nSCOR RECORD !!!";
        }



    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            
            if(CounterUIManager.instantaa.finished==1 && entered==false)
            {
                entered = true;
                StartCoroutine("startGame");
            }
        }
        else
        {
            if(!song.isPlaying && !fereastraRezultate.activeInHierarchy)
            {

                StartCoroutine("endGame");

            }
        }
    }



    public void NotaNimerita()
    {

        if(putereCurenta-1 < putereNivele.Length) 
        { 

                 putereTracker++;


                 if(putereNivele[putereCurenta-1] <= putereTracker)
                 {
                     putereTracker = 0;
                     putereCurenta++;
                 }
        }

        putere.text = "Putere X" + putereCurenta;

        scorText.text = "Scor  " + scorCurent; 
    }

    public void notaNormala()
    {
        scorCurent += scorPerNota * putereCurenta;
        NotaNimerita();

        noteNormale++;
    }

    public void notaBuna()
    {
        scorCurent += scorNotaBuna * putereCurenta;
        NotaNimerita();

        noteBune++;
    }

    public void notaPerfecta()
    {
        scorCurent += scorNotaPerfecta * putereCurenta;
        NotaNimerita();

        notePerfecte++;
    }

    public void NotaRatata()
    {

        putereCurenta = 1;
        putereTracker = 0;

        putere.text = "Putere X" + putereCurenta;

        noteRatate++;
    }
}
