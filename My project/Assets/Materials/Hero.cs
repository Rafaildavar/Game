using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private int scene = 0;
    public Transform Finishcheck;
    private float finrad = 0.2f;
    public LayerMask Fin;
    public LayerMask Bed;
    public LayerMask Spikes;


    private bool isGrounded = false;
    private bool isFinish = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Finish();
        Jump_2();
        Death();
  
    }

    private void Update()
    {
        if (isGrounded) State = States.idle;
        if (Input.GetButton("Horizontal"))
            Run();

        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();


    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
        if (isGrounded) State = States.run;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded) State = States.jump;
    }

    private void Finish()
    {    
        isFinish = Physics2D.OverlapCircle(Finishcheck.position, finrad, Fin);
        if(isFinish)
        {
            SceneManager.LoadScene(scene);
        }
    }

    private void Death()
    {
        if (Physics2D.OverlapCircle(Finishcheck.position, finrad, Spikes))
        {
            SceneManager.LoadScene(scene-1);
        }
    }

    private void Jump_2()
    {
        if (Physics2D.OverlapCircle(Finishcheck.position, finrad, Bed))
        {
            rb.AddForce(transform.up * 30f, ForceMode2D.Impulse);
        }
    }



}
public enum States
{
    idle,
    run,
    jump
}
