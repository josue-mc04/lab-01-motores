using UnityEditor.AnimatedValues;
using UnityEngine;

public class playermove : MonoBehaviour{
    [SerializeField] private float Horizontal;
    [SerializeField] private float Vertical;
    [SerializeField] private float Velocity;
    [SerializeField] private float Distance;
    [SerializeField] private int IDAnimation;
    [SerializeField] private Animator animator;
    [SerializeField] private bool animationtrue;
    [SerializeField]private SpriteRenderer sprite;

    private void Awake(){
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        Horizontal = Input.GetAxis("Horizontal") * Velocity*Time.deltaTime;
        Vertical = Input.GetAxis("Vertical") * Velocity*Time.deltaTime;
        if (Horizontal == 0 && Vertical == 0) {
            animationtrue = false;
        }
        else { 
            animationtrue= true;    
        }
        if (Horizontal > 0){
            sprite.flipX = false;
        }
        else if (Horizontal < 0) { 
            sprite.flipX = true;
        }
        transform.position = new Vector2(Horizontal + transform.position.x, Vertical + transform.position.y);

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position,new Vector2(Horizontal,Vertical),Distance);

        Debug.DrawRay(transform.position, new Vector2(Horizontal, Vertical).normalized*hit2D.distance,Color.red);

        animator.SetInteger("id", IDAnimation);

        animator.SetBool("walcking", animationtrue);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) {
            if (collision.CompareTag("monk"))
            {
                IDAnimation = 1;
                sprite.sprite = spriteRenderer.sprite;
            }
            else if (collision.CompareTag("archer"))
            {
                IDAnimation = 2;
                sprite.sprite = spriteRenderer.sprite;
            }
            else if (collision.CompareTag("lancer"))
            {
                IDAnimation = 3;
                sprite.sprite = spriteRenderer.sprite;
            }
        }
       
    }
}
