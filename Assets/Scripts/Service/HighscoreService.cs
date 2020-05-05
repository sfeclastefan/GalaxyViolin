using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreService
{
    [SerializeField] private List<HighscoreEntry> highscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> getHighscores()
    {
        readList();

        sort();
        return highscores;
    }

    private void readList()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (jsonString.Length > 0)
        {
                highscores = JsonConvert.DeserializeObject<List<HighscoreEntry>>(jsonString);
          
        }

    }
    
        private void sort()
        {

            for (int i = 0; i < highscores.Count; i++)
            {
                for (int j = i + 1; j < highscores.Count; j++)
                {
                    if (highscores[j].scor > highscores[i].scor)
                    {

                        HighscoreEntry aux = highscores[i];
                        highscores[i] = highscores[j];
                        highscores[j] = aux;
                    }
                }
            }
    }
    
    private void save()
    {
        //save updated highscores
        string json = JsonConvert.SerializeObject(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }


    public bool isNewHighscore(int scor)
    {
        readList();
        if (highscores.Count > 0)
        {
            
            if (highscores.Count < 5)
                return true;
          
            if (highscores[4].scor < scor)
                return true;
            else
                return false;
        }
       
        return true;
    }
    

    public void addHighscoreEntry(int scor, string nume )
    {
        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry(scor, nume);


        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores =  new List<HighscoreEntry>();
           
        }


        //add new entry to highscores
        if (highscores.Count<5)
        {

            highscores.Add(highscoreEntry);   
       

        }
        else if(highscores[4].scor<scor)
        {

            highscores.RemoveAt(4);
            highscores.Add(highscoreEntry);
        }


        sort();

        save();

    }

    

   

}
