using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    public int life;
    public GameObject heart1,heart2,heart3;
    public GameObject lifeUIActive;
    public SpriteRenderer sp;
    void Start(){
        sp = GetComponent<SpriteRenderer>();
        lifeUIActive.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(life < 3){
           Destroy(heart3.gameObject);
        }
        if(life < 2){
            Destroy(heart2.gameObject);
        }
        
        if(life < 1){
            Destroy(gameObject);
        }
          transform.Translate(Time.deltaTime * speed * transform.right);

          if(!Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)){
            Flip();
          }
    }
        void Flip(){
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        speed *= -1;
    }
    private void OnTriggerEnter2D(Collider2D colider) {
        if(colider.gameObject.CompareTag("PlayerAttack")){
             StartCoroutine("Damage");
        }
    }
      IEnumerator Damage(){
      life -= 1;
      lifeUIActive.SetActive(true);
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.red;
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.white;
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.red;
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.white;
      yield return new WaitForSeconds(0.2f);
      lifeUIActive.SetActive(false);
    }
}
