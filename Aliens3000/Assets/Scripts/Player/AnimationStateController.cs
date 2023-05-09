using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    public bool isCharacterDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d");
        bool backwardPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKeyDown("space");
        bool jumpFlipPressed = Input.GetKey("c") && Input.GetKey("c");
        bool jumpBackwardPressed = Input.GetKey("x") && Input.GetKey("x");
        ;
        bool shootingPressed = Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
        bool dancingPressed = Input.GetKey("t") && Input.GetKey("y");

        animator.SetBool("isWalking", forwardPressed);
        animator.SetBool("isGoingBackward", backwardPressed);
        animator.SetBool("isRunning", forwardPressed && runPressed);
        animator.SetBool("isShooting", shootingPressed);
        animator.SetBool("isJumping", jumpPressed);
        animator.SetBool("isDancing", dancingPressed);
        animator.SetBool("isDead", isCharacterDead);

        
        if (!animator.GetBool("isJumpFlipping") && jumpFlipPressed)
        {
            animator.SetBool("isJumpFlipping", true);
        }
        else if (animator.GetBool("isJumpFlipping") && !jumpFlipPressed)
        {
            animator.SetBool("isJumpFlipping", false);
        }

        if (!animator.GetBool("isJumpingBackward") && jumpBackwardPressed)
        {
            animator.SetBool("isJumpingBackward", true);
        }
        else if (animator.GetBool("isJumpingBackward") && !jumpBackwardPressed)
        {
            animator.SetBool("isJumpingBackward", false);
        }
    }
}

