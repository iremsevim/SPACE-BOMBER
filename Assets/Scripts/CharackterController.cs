using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBar))]
public class CharackterController : MonoBehaviour
{
    public static CharackterController instance;
    public LayerMask groundmask;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform bombpoint;
    public bool IsStart = false;
    public GameObject currentball;
    public float ballthrowforce = 100f;
    public HealthBar healthBar;
    public Image healthımage;
  
   
 
    public AnimationListener animationListener;

    void Awake()
    {
        instance = this;
        healthBar = GetComponent<HealthBar>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        animationListener.animprofils.Add(new AnimationListener.Animprofil(AnimationListener.AnimType.throwing, () =>
         {
             Debug.Log("Top Fırladı");
             currentball.GetComponent<SphereCollider>().enabled = true;
             float x = ballthrowforce * 2f / 3f;
             float y = ballthrowforce - x;
             Vector3 force = (transform.up * x) + (transform.forward * y);
             currentball.AddComponent<Rigidbody>().AddForce(force);
            
             currentball.GetComponent<Rigidbody>().useGravity = true;
             currentball.transform.SetParent(null);
             currentball = GameManager.instance.CreateBomb(bombpoint);
         }));
    }
    
    private void Start()
    {
        transform.DisabledRagDoll();
        



        healthBar.OnDamaged=()=>
        {
            Debug.Log("Canım azaldı");
            healthımage.fillAmount = healthBar.HealthAmount;
        };
        healthBar.OnDead=()=>
         {
             Debug.Log("Öldü");
             EnemyController.instance.Happy();
             AudioWorker.PlayAudio("dead");
          
             anim.enabled = false;
             transform.EnabledRagDoll();
             DeathCam.instance.StartFollowing(transform);
             StartCoroutine(GameManager.instance.CreateCharackter());
             GameManager.instance.SetEnemyScore();

            


             
         
             transform.AllRagdollForce(Vector3.up*1000 + (Vector3.right *new float[] {-1,1 }[Random.Range(0,1)])*500);
             agent.enabled = false;
         };
       currentball= GameManager.instance.CreateBomb(bombpoint);
        
    }
    void Update()
    {
        if (healthBar.IsDead) return;
           CharackterControl();
    }
   

    
  
    public void CharackterControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayhit;
            if (Physics.Raycast(ray.origin, ray.direction, out rayhit, 200f, groundmask))
            {
                agent.SetDestination(rayhit.point);
                anim.SetBool("walking", true);
            }
        }
        if (Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance)
        {
            anim.SetBool("walking", false);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("throwing");

        }
    }
    public void Happy()
    {
        anim.SetTrigger("happy");
        AudioWorker.PlayAudio("happy");
    }
}

