using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AnimationListener : MonoBehaviour
{

    public List<Animprofil> animprofils = new List<Animprofil>();
  
    public void ThrowingBall(AnimType type)
    {
        Animprofil findedanim=animprofils.Find(x => x.type == type);
        findedanim.animevent?.Invoke();
    }
    
   
    public class Animprofil
    {
        public AnimType type;
        public System.Action animevent;
        public Animprofil(AnimType _type,System.Action _animevent)
        {
            type = _type;
            animevent = _animevent;
        }
    }
    public enum AnimType
    {
        none=0,throwing=1
    }
}

