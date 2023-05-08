using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodoExplosionScript : MonoBehaviour
{

    public Animator myAnimator;
    public GameObject me;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());
        myAnimator.Play("Explosion");
    }

    private IEnumerator Explode()
    {
        myAnimator.Play("Explosion");
        yield return new WaitForSeconds(2f);
        Destroy(me);
    }

    
}
