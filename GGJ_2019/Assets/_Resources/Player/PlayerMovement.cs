using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

//using UnityEditor.Animations;
//using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public AnimationClip idleKnight;
    public AnimationClip runKnight;
//    public AnimationClip attackKnight;
    
    public AnimationClip idleNormal;
    public AnimationClip runNormal;
//    public AnimationClip pickupNormal;
    
    private Rigidbody2D rbody;
    protected Animator anim;
    public AnimatorOverrideController animatorOverrideController { get; private set; }

//    private bool isNormal { get; set; } = true;
//    public AnimatorController animatorOverrideController;
//    public AnimatorController KnightAnimControl;

    private bool isAttacking;
    public bool isPickUp { get; private set; } = false;
    private RaycastHit2D hit;
    
    private GameManager gameManager;

    public Vector2 LookDirection { get; private set; } = Vector2.zero;

    // Use this for initialization
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAttacking){
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                if (gameManager.CurrentWorldState == GameManager.WorldState.Knight)
                {
                    isAttacking = false;
                    anim.SetBool("isAttacking", false);
                }
            }
        }else if (isPickUp) {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
//                if (gameManager.CurrentWorldState == GameManager.WorldState.Normal)
//                {
                    isPickUp = false;
                    anim.SetBool("isPickUp", false);
                    GetComponent<Player>().PickUp(hit);
//                }
            }
        } else{
            
            Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (movementVector != Vector2.zero)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("input_x", movementVector.x * Time.deltaTime);

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
        
            rbody.MovePosition(rbody.position + movementVector * Time.deltaTime * 15f);
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