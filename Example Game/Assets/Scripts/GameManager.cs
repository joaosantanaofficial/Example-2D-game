using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tutorial;
       void Start()
    {
        StartCoroutine("FecharTutorial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CloseTutorial(){
        tutorial.SetActive(false);
    }
    public void End(){
        Application.Quit();
    }
    IEnumerator FecharTutorial(){
        yield return new WaitForSeconds(5f);
        CloseTutorial();
    }
}
