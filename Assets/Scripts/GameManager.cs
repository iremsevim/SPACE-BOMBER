using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject bombprefab;
    public GameObject enemyprefab;
    public List<Transform> enemyspawnpoints;
    public string portal;
    public Transform enemypatrolroot;
    public GameObject charackterprefab;
    public int playerscore = 0;
    public int enemyscore = 0;
    public Text playertext;
    public Text enemytext;
    
    



    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }
    public GameObject CreateBomb(Transform bombpoint)
    {

        GameObject createdbomb = Instantiate(bombprefab, bombpoint.position, Quaternion.identity);
        createdbomb.transform.SetParent(bombpoint);
        return createdbomb;
        
    }


    public IEnumerator CreateEnemy()
    {
        int ransdompoint = Random.Range(0, enemyspawnpoints.Count);
        portal.ShowParticle(enemyspawnpoints[ransdompoint].position);
        yield return new WaitForSeconds(2f);
        GameObject createdenemy = Instantiate(enemyprefab, enemyspawnpoints[ransdompoint].position, Quaternion.identity);
        AudioWorker.PlayAudio("teleport");
        DeathCam.instance.StopFollowing();

    }
    public IEnumerator CreateCharackter()
    {
        int ransdompoint = Random.Range(0, enemyspawnpoints.Count);
        portal.ShowParticle(enemyspawnpoints[ransdompoint].position);
        
        yield return new WaitForSeconds(2f);
        Destroy(CharackterController.instance.gameObject);
        GameObject createdcharackter = Instantiate(charackterprefab, enemyspawnpoints[ransdompoint].position, Quaternion.identity);
        AudioWorker.PlayAudio("teleport");
        DeathCam.instance.StopFollowing();
    }
    public void SetEnemyScore()
    {
        enemyscore++;
       
        enemytext.GetComponent<Text>().text = enemyscore.ToString();
       
    }
    public void SetplayerScore()
    {
        playerscore++;
        playertext.GetComponent<Text>().text = playerscore.ToString();
    }

}
