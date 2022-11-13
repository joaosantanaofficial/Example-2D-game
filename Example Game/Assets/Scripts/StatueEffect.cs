using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueEffect : MonoBehaviour
{
    public SpriteRenderer sp;
    bool effecton;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
       if(!effecton){
        StartCoroutine("Effect"); 
        effecton = true;
       }
    }
    IEnumerator Effect(){
        yield return new WaitForSeconds(0.2f);
        sp.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        sp.color = Color.white;
        effecton = false; 
    }

}
