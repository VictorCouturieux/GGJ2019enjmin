using UnityEngine;
using System.Collections;


using UnityEditor.Animations;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbody;
    public static Animator anim;
    public AnimatorController NormalAnimControl;
    public AnimatorController KnightAnimControl;

    private bool isAttacking;
    public bool isPickUp { get; private set; } = false;
    private RaycastHit2D hit;

    public Vector2 LookDirection { get; private set; } = Vector2.zero;

    // Use this for initialization
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAttacking){
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false);
            }
        }else if (isPickUp) {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                isPickUp = false;
                anim.SetBool("isPickUp", false);
                GetComponent<Player>().PickUp(hit);
            }
        } else{
            
            Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (movementVector != Vector2.zero)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("input_x", movementVector.x);

                var rotate = transform.rotation;
                if (movementVector.x < 0)
                    rotate.y = 90;
                else if (movementVector.x > 0)
                    rotate.y = 0; 
                transform.rotation = rotate;
            
                LookDirection = movementVector;
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        
            rbody.MovePosition(rbody.position + movementVector * Time.deltaTime * 10f);
        }
        
    }

    public void SetIsAttacking()
    {
        anim.SetBool("isAttacking", true);
        isAttacking = true;
    }

    public void SetIsPickUp(RaycastHit2D h)
    {
        anim.SetBool("isPickUp", true);
        isPickUp = true;
        hit = h;
    }
}