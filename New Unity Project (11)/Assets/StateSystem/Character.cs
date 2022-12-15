using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rb;
    public Vector3 animDirection;
    public Vector3 moveDirection;
    Quaternion targetRotation = Quaternion.identity;
    [Tooltip("Not yet implemented")] public float jumpForce = 300;
    private float lastJumpTime;
    public float angleSpeedReduction;
    public float runSpeedMultiplier = 3;
    public int staminaDrainFac = 10;

    [Header("Combat")]
    public Sword sword;

    [Header("Sound")]
    public AudioSource sound;
    public AudioClip footstepSound;
    public AudioClip attackSound;    
    
    [Header("Stats")]
    public float speed = 1.2f;
    private float baseSpeed=2;
    public float stamina;
    private float baseStamina=100;
    public float health;
    private float baseHealth=100;
    private bool isDead = false;
    public Animator anim;

    [Header("Ground check")]
    public float groundOffset;
    public float sphereRadius;
    Vector3 spherePosition;

    //check slope angle
    RaycastHit hit;
    
    State currentState;
    [HideInInspector] public State walkState = new CharWalkState();
    [HideInInspector] public State runState = new CharRunState();
    [HideInInspector] public State blockState= new CharBlockState();
    [HideInInspector] public State jumpState = new CharJumpState();
    [HideInInspector] public State battleState = new CharBattleState();
    [HideInInspector] public State deathState = new CharDeathState();

    [Header("Stats")]
    public int strength; //ќтвечает за здоровье персонажа и наносимый физический урон.
    public int agility; //ќтвечает за скорость передвижени€, атак, блокировани€ ударов.
    public int endurance; //ќтвечает за величину параметра выносливости и сопротивлени€ урону.
    [Header("UI")]
    public TMP_Text deathText;
    public Image deathImage;

    float timer = 0;
    void Awake()
    {
        baseHealth += strength * 10;
        health = baseHealth;

        baseSpeed +=agility * 0.02f;
        speed= baseSpeed;

        baseStamina += endurance * 10;
        stamina= baseStamina;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        currentState = walkState;
        currentState.Enter(this);
    }

    void Update()
    {
        if (health < 0) 
        {
            if(!isDead) 
            {
                isDead = true;
                SwitchState(deathState);
            }
            StartCoroutine(Restart());
        }
       // Debug.Log(currentState.ToString());
        
        CameraControl();
        MovementInput();
        animDirection.x = -Vector3.Dot(moveDirection, transform.right);
        animDirection.z = Vector3.Dot(moveDirection, transform.forward);
        anim.SetFloat("Y", animDirection.z, 0.1f, Time.deltaTime);
        anim.SetFloat("X", animDirection.x, 0.1f, Time.deltaTime);
        
        moveDirection = ProjectMoveDir(moveDirection);
        currentState.Update(this);
        stamina += 10 * Time.deltaTime;
        //Debug.Log(currentState);
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }
    public void SwitchState(State state) 
    {
        currentState.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }
    void CameraControl()
    {
        if(!isDead) 
        {
            targetRotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 3, Vector3.up);

            rb.MoveRotation(rb.rotation * targetRotation);
        }
        
    }
    void MovementInput()
    {
        moveDirection =(-Input.GetAxis("Vertical") * transform.right + Input.GetAxis("Horizontal") * transform.forward).normalized;
    }
    public bool isGrounded() 
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y-groundOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, sphereRadius, LayerMask.GetMask("Ground"));
    }
    public void TakeDamage(float damage) 
    {
        if (currentState == blockState)
        {
            if (stamina - 80 < 0)
            {
                anim.SetTrigger("blockBreak");
                stamina -= 80;
            }
            else
            {
                anim.SetTrigger("blockHit");
                stamina -= 80;
            }

        }
        else 
        {
            anim.SetTrigger("Damage");
            health -= damage;
        }
        
    }
    private Vector3 ProjectMoveDir(Vector3 MoveDir) 
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.9f + 0.3f)) 
        {
            if (90 - (Vector3.Angle(-moveDirection, hit.normal)) > 50) angleSpeedReduction = 0; //«амедление при ходьбе под углом
            else angleSpeedReduction = 1 - (90 - Vector3.Angle(-moveDirection, hit.normal)) / 135;
            return Vector3.ProjectOnPlane(MoveDir, hit.normal).normalized;
        }
        else return Vector3.zero;
    }
    public IEnumerator Restart() 
    {
        float fadeTime = 2f;
        
        while(timer < fadeTime)
        {
            yield return null;
            timer += Time.deltaTime;
            deathImage.color = new Color(deathImage.color.r, deathImage.color.g, deathImage.color.b, timer/2);
            deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, timer/2);
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
    public void PlaySoundDelayed(AudioClip clip, float volume, float delay) 
    {
        StartCoroutine(SoundDelay(clip, volume, delay));
    }
    IEnumerator SoundDelay(AudioClip clip, float volume, float delay) 
    {
        yield return new WaitForSeconds(delay);
        sound.PlayOneShot(clip, volume);
    }
}
