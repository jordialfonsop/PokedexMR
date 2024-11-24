using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;


public class CentrelineToolbox : MonoBehaviour
{

    [SerializeField] private GameObject button;
    [SerializeField] private bool isToggle = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //GenerateButtons(); 
    }

    void GenerateButtonsLessons()
    {

        /*int row = 0;

        //Canviar a Pokemon JSON
        for (int i = 0; i < PokedexManager.Instance.lessonList.transform.childCount; i++)
        {
            GameObject pokemonButton = Instantiate(button);
            pokemonButton.name = PokedexManager.Instance.lessonList.transform.GetChild(i).name;
            pokemonButton.GetComponent<CentrelineButton>().SetIsToggle(false);
            pokemonButton.GetComponent<CentrelineButton>().lessonNumber = i;
            pokemonButton.transform.parent = this.transform;
            pokemonButton.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = pokemonButton.name;
            pokemonButton.transform.localPosition = new Vector3(0.41f - (0.06f * row), 0, -0.0351f);
            pokemonButton.transform.rotation = new Quaternion(0, 0, 0, 0);
            pokemonButton.transform.localScale = new Vector3(1, 1, 1);

            row++;

            if (PokedexManager.Instance.currentLesson)
            {
                if (i == PokedexManager.Instance.currentLessonNumber)
                {
                    pokemonButton.GetComponent<CentrelineButton>().SetButtonColor(new Color(0f, 0.55f, 1f));  
                }
            }

        }
        */
        

    }

    void DeleteButtons()
    {
        for(int i = transform.childCount-1; i > 1; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void DeleteButtonsLessons()
    {
        for (int i = transform.childCount - 1; i > -1; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void RegenerateButtons()
    {
        DeleteButtons();
        //GenerateButtons();
    }

    public void RegenerateButtonsLessons()
    {
        DeleteButtonsLessons();
        GenerateButtonsLessons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (!isToggle)
        {
            RegenerateButtonsLessons();
        }   
    }
}
