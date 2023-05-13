using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  BodoF is the script that deals with the pointer attack.
 *
 *  @author ajh6754 (albert2417)
 */
public class BodoF : MonoBehaviour
{
    // Initializes animator and current game object
    public Animator myAnimator;
    public GameObject me;

    // Start is called before the first frame update, starts coroutine, may need positioning?
    void Start()
    {
        StartCoroutine(F());
    }

    /// F will play the F animation. MAY NEED UPDATING, METHOD IS FLAWED
    /// 
    private IEnumerator F()
    {
        myAnimator.Play("F Phase 1");
        yield return new WaitForSeconds(.167f);
        myAnimator.Play("F Phase 2");
        yield return new WaitForSeconds(.167f);
        myAnimator.Play("F Phase 3");
        yield return new WaitForSeconds(.95f);
        Destroy(me);
    }
}
