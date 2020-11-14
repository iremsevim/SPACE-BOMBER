using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWorker : MonoBehaviour
{
    public static ParticleWorker instance; 
    public List<ParticleProfil> particles;


    private void Awake()
    {
        instance = this;
    }
}
public static class Particles
{

    public static void ShowParticle(this string Particle,Vector3 pos)
    {

       ParticleProfil findedparticle =ParticleWorker.instance.particles.Find(x => x.ParticleID == Particle);
        GameObject createparticle = ParticleWorker.Instantiate(findedparticle.particle, pos, Quaternion.identity);
        ParticleWorker.Destroy(createparticle.gameObject, 2f);
    }
   
}
[System.Serializable]
public class ParticleProfil
{
    public string ParticleID;
    public GameObject particle;
}



