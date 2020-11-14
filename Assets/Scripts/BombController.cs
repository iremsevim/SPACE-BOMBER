using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombController : MonoBehaviour
{
    public string expbomb;
    public float expbombdelay = 2f;
    public bool IsOneCollison = true;
    public string ExpAudio;
    public float shakeduration = 0.2f;
    public float expradius = 2f;
    public int maxbombdamaage = 100;




    public void OnCollisionEnter(Collision collision)
    {

        Debug.Log("dIŞARI");
        if (collision.gameObject.layer == 8)
        {
            if(IsOneCollison)
            {
                StartCoroutine(WaitingBomb());
                Debug.Log("İçeri");
                IsOneCollison = false;
            }
           
            
        }
    }
    private IEnumerator WaitingBomb()
    {
        yield return new WaitForSeconds(expbombdelay);
        expbomb.ShowParticle(new Vector3(transform.position.x,transform.position.y+1f,transform.position.z));
        AudioWorker.PlayAudio(ExpAudio);
        Camera.main.DOShakePosition(shakeduration);
        BombAttackDamage();
        Destroy(gameObject);
    }
    public void BombAttackDamage()
    {
        Collider[] allhitted = Physics.OverlapSphere(transform.position, expradius);
        foreach (var item in allhitted)
        {
            if(item.GetComponent<HealthBar>())
            {
                float damagex = maxbombdamaage / expradius;//20
                float distance = Vector3.Distance(transform.position, item.transform.position);//1
                int damageamont =maxbombdamaage-(int)(distance * damagex);
                item.GetComponent<HealthBar>().HealthDamage(damageamont);
            }
        }
    }
}
