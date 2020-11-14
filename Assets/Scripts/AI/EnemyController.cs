using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;
using Unity.Profiling;

public class EnemyController :MonoBehaviour
{
    public static EnemyController instance;
    public LayerMask groundmask;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform bombpoint;
    public bool IsStart = false;
    public GameObject currentball;
    public float ballthrowforce = 100f;
    public HealthBar healthBar;
    public Image healthımage;
    public List<Transform> patrolpoints;
    public AnimationListener animationListener;
    public EnemState enemystate;
    public float bombrate = 3f;
    public float bombtimer = 3f;
    public float enemydetectiondistance = 5f;
    
    
  




    void Awake()
    {
        instance = this;
        foreach (Transform item in GameManager.instance.enemypatrolroot)
        {
            patrolpoints.Add(item.transform);
        }


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
            currentball.AddComponent<Rigidbody>().AddForce(force*Random.Range(0.8f,1.5f));

            currentball.GetComponent<Rigidbody>().useGravity = true;
            currentball.transform.SetParent(null);
            currentball = GameManager.instance.CreateBomb(bombpoint);
        }));
    }


    private void Start()
    {
      
        transform.DisabledRagDoll();
     
        GotoPoint();
        anim.SetBool("walking", true);



        healthBar.OnDamaged = () =>
        {
            Debug.Log("Canım azaldı");
            healthımage.fillAmount = healthBar.HealthAmount;
        };
        healthBar.OnDead = () =>
        {
            Debug.Log("Öldü");
            AudioWorker.PlayAudio("dead");
            CharackterController.instance.Happy();
            GameManager.instance.SetplayerScore();
            anim.enabled = false;
            DeathCam.instance.StartFollowing(transform);
            transform.EnabledRagDoll();
          
            StartCoroutine(GameManager.instance.CreateEnemy());
            
          
            transform.AllRagdollForce(Vector3.up * 1000 + (Vector3.right * new float[] { -1, 1 }[Random.Range(0, 1)]) * 500);
            agent.enabled = false;
        };
        currentball = GameManager.instance.CreateBomb(bombpoint);

    }
    private void Update()
    {


        if (healthBar.IsDead) return;         
        if(enemystate==EnemState.patrol)
        {
            PointControl();
            
          
        }
        else if(enemystate==EnemState.attacking)
        {
            bombtimer -= Time.deltaTime;
            if(bombtimer<=0)
            {
                AttackEnemy();
                bombtimer = bombrate;
            }
            TurnToEnemy();


        }
        LookingControlAround();
    }
    public void GotoPoint()
    {
        List<Transform> fakelist = patrolpoints.ToList();
        fakelist.RemoveAll(x => x.position == agent.destination);
        int randpatrolpoints = Random.Range(0, fakelist.Count);
        Vector3 target = fakelist[randpatrolpoints].transform.position;
        agent.SetDestination(target);
    }
    private void TurnToEnemy() 
    {
        Vector3 amount = CharackterController.instance.transform.position - transform.position;
        float amounty = Mathf.Atan2(amount.x, amount.z) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, amounty, 0);
      
      
    }
    public void AttackEnemy()
    {
        if(CharackterController.instance.healthBar.IsDead)
        {
            enemystate = EnemState.patrol;
        }
        else
        {

            anim.SetTrigger("throwing");
        }
    }
    public void PointControl()
    {
        if(Vector3.Distance(transform.position,agent.destination)<0.2f)
        {
            GotoPoint();
        }
    }
    public void LookingControlAround()
    {
        if(CharackterController.instance.healthBar.IsDead)
        {
            enemystate = EnemState.patrol;
            anim.SetBool("walking", true);
            agent.isStopped = false;
            return;
        }
        if(Vector3.Distance(transform.position,CharackterController.instance.transform.position)<enemydetectiondistance )
        {
            enemystate = EnemState.attacking;
            anim.SetBool("walking", false);
            agent.isStopped = true;

        }
        else if(Vector3.Distance(transform.position, CharackterController.instance.transform.position)> enemydetectiondistance*1.2f)
        {
            enemystate = EnemState.patrol;
            anim.SetBool("walking",true);
            agent.isStopped = false;
        }
    }
    public enum EnemState
    {
        patrol=0,attacking=1
    }
    public void Happy()
    {
        anim.SetTrigger("happy");
        AudioWorker.PlayAudio("happy");
    }
}
