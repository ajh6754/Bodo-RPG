using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  BodoExplosionScript is the script that deals with the Impetum attack.
 *  It is the script for the xplosion that ensues.
 *
 *  @author ajh6754 (albert2417)
 */
public class BodoExplosionScript : MonoBehaviour
{
    // Gets Animator and current object
    public Animator myAnimator;
    public GameObject me;

    // Start is called before the first frame update, starts coroutine
    void Start()
    {
        StartCoroutine(Explode());
    }

    /// Explode plays the animation and destroys itself.
    /// 
    private IEnumerator Explode()
    {
        myAnimator.Play("Explosion");
        yield return new WaitForSeconds(2f); // perhaps change to when animation ends
        Destroy(me);
    }
}
