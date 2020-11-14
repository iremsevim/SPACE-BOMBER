using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCam : MonoBehaviour
{
    public static DeathCam instance;
    public Transform DeathEnemy;
    public Vector3 offset;
    public GameObject maincam;


    private void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);
    }



    public void Update()
    {
        if (DeathEnemy == null) return;
        transform.position = DeathEnemy.position + offset;
        FocusCam();
    }

    public void StopFollowing()
    {
        DeathEnemy = null;
        DeathCam.instance.gameObject.SetActive(false);

    }
    public void FocusCam()
    {
        /*
        Vector3 amount = DeathEnemy.transform.position - maincam.transform.position;
        float amounty = Mathf.Atan2(amount.x, amount.z) * Mathf.Rad2Deg;
        maincam.transform.localEulerAngles = new Vector3(0, amounty, 0);
        */

        maincam.transform.LookAt(DeathEnemy);

    }

    public void StartFollowing(Transform x)
    {
        DeathCam.instance.gameObject.SetActive(true);
        DeathCam.instance.DeathEnemy = x;
       
        

    }


}
