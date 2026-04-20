using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        animator.SetBool("isRunning", true);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }
}