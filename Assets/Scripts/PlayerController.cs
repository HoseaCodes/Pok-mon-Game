using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float moveSpeed;

    private bool isMoving;
    private Vector2 input;

    private Animator animator;

    private void Awake ()
    {
        //Connects to the animtor in unity
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            // input will always be 1 or -1 w/GetAxisRaw
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //remove diagonal movement
            if (input.x != 0) input.y = 0;

            if(input != Vector2.zero) 
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                // Adding the input into the current position
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));
            }
        }
        // Tracks moving status
        animator.SetBool("isMoving", isMoving);
    }
    //Change from the current position to the target position
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        // check if there is a difference between positions
        while ((targetPos - transform.position).sqrMagnitude >  Mathf.Epsilon)
        {
            // Move the player towards the target position 
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            // Stop the execution
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
}
