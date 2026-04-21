using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = transform.Find("Remy@Running (1)").GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        animator.SetBool("isRunning", true);

        if (Keyboard.current != null && 
            (Keyboard.current.spaceKey.wasPressedThisFrame ||
             Keyboard.current.wKey.wasPressedThisFrame ||
             Keyboard.current.upArrowKey.wasPressedThisFrame))
        {
            animator.SetTrigger("Jump");
        }
    }
}