using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodoF : MonoBehaviour
{

    public Animator myAnimator;
    public GameObject me;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(F());
    }

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
