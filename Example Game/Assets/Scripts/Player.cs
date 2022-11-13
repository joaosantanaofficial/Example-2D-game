using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Animator anim;
    public bool inGround;
    public bool facingRight = true;

    public float jumpForce = 350;
    public float speed = 5;
    public float speedBase = 5;
    
    public float timeBetweenAttacks;
    public float nextAttack;
    public bool isAttack;
	 
    public int life = 3;
    public SpriteRenderer sp;
    public bool takeDamageAgain = true;
    public GameObject death;

    public Slider lifeUI;
    public AudioSource walkA,attackA;
    public GameObject end;

    void Start(){
      end.SetActive(false);
      death.SetActive(false);
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sp = GetComponent<SpriteRenderer>();
    }
      

 void Attack(){
      attackA.Play();
      anim.SetTrigger("Attack");
      nextAttack = Time.time + timeBetweenAttacks;
      isAttack = true;
     }
    void Update(){
      lifeUI.value = life;
       if(life < 1){
        death.SetActive(true);
        StartCoroutine("Death");
       }
    
       if(isAttack){speed = 0;}
       else{speed = speedBase;}
     
    if(Input.GetButtonDown("Fire1") && Time.time > nextAttack){
        Attack();
      }
    
    
    if(Input.GetButtonDown("Jump")){
        if(inGround){
             rb.AddForce(Vector2.up * jumpForce);
            }}
     
     
       }
       void FixedUpdate(){
        inGround = Physics2D.Linecast(transform.position,groundCheck.position,1<<LayerMask.NameToLayer("Ground"));
      if(!inGround){
        anim.SetBool("Jump",true);
      }else{anim.SetBool("Jump",false);}
      
      float move = Input.GetAxisRaw("Horizontal");
        
       	if(move > 0 && !facingRight){Flip();}if(move < 0 && facingRight){Flip();}
       	rb.velocity = new Vector2(move*speed,rb.velocity.y);
      if(move != 0){if(inGround){walkA.Play(0); anim.SetBool("Walk",true);}}else{anim.SetBool("Walk",false);walkA.Pause();}
   
    
     
}
     void EndAttack(){
      isAttack = false;
     }
     


       void Flip(){
          facingRight = !facingRight;
           Vector3 scale = transform.localScale;
           scale.x *= -1;
           transform.localScale = scale;
           }

     private void OnCollisionEnter2D(Collision2D colider) {
      if(colider.gameObject.CompareTag("Enemy")&& takeDamageAgain){
        StartCoroutine("Damage");
      }
       if(colider.gameObject.CompareTag("End")){
        end.SetActive(true);
      }
     if(colider.gameObject.CompareTag("Mortal")){
        death.SetActive(true);
        StartCoroutine("Death");
      }
      
    }
    IEnumerator Damage(){
       takeDamageAgain = false;
      life -= 1;
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.red;
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.white;
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.red;
      yield return new WaitForSeconds(0.2f);
      sp.color = Color.white;
      yield return new WaitForSeconds(0.2f);
      takeDamageAgain = true;
    }
      IEnumerator Death(){
      yield return new WaitForSeconds(1.5f);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
}