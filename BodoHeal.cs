using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodoHeal : MonoBehaviour
{

    public Animator myAnimator;
    public GameObject me;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Heal());
    }

    private IEnumerator Heal()
    {
        myAnimator.Play("Heal Aura");
        yield return new WaitForSeconds(1.917f);
        Destroy(me);
    }
}
