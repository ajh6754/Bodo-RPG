using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  BodoHeal is the script that deals with Sanatio.
 *
 *  @author ajh6754 (albert2417)
 */
public class BodoHeal : MonoBehaviour
{
    // Initializes animator and object
    public Animator myAnimator;
    public GameObject me;

    // Start is called before the first frame update, starts healing coroutine
    void Start()
    {
        StartCoroutine(Heal());
    }

    /// Heal plays the healing animation.
    /// 
    private IEnumerator Heal()
    {
        myAnimator.Play("Heal Aura");
        yield return new WaitForSeconds(1.917f);
        Destroy(me);
    }
}
