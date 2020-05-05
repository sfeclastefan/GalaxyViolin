using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUIManager : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entry;

    [SerializeField] private TMP_Text InputText;
    private List<Transform> highscoreEntryTransformList;

    private HighscoreService service;
    private static HighscoreUIManager _instance;

    public void addScore()
    {
        string username = InputText.text;
        int scor = GameManager.getInstanta().getScor();
        AddHighscoreEntry(scor, username);
    }

    public static HighscoreUIManager getInstance()
    {
          return _instance; 
    }

    public bool isNewHighscore(int scor)
    {
        return service.isNewHighscore(scor);
    }

    public void Awake()
    {

        if (_instance == null)
            _instance = this;
        if (service == null)
            service = new HighscoreService();
        
        entryContainer = transform.Find("ContainerIntregistrari");
        entry = transform.Find("TemplateIntregistrare");

        entry.gameObject.SetActive(false); 
        refresh();
        
    }

    private void refresh()
    {
            highscoreEntryTransformList = new List<Transform>();
            foreach(Transform child in entryContainer)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (HighscoreEntry highscoreEntry in service.getHighscores())
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            }

        }
    


    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList )
    {
        float templateHeight = 3f;
        Transform entryTransform = Instantiate(entry, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int scor = highscoreEntry.scor;
        entryTransform.Find("Scor").GetComponent<Text>().text = scor.ToString();

        string nume = highscoreEntry.nume;
        entryTransform.Find("Nume").GetComponent<Text>().text = nume;

        transformList.Add(entryTransform);


    }


    public void AddHighscoreEntry(int scor, string nume )
    {
        service.addHighscoreEntry(scor, nume);
        refresh();

    }

}
