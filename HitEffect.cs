using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public int count = 0;
    public int maxCount = 3;

    public Animator myAnimator;
    public GameObject flurry;
    public GameObject BodoAttackPos;
    public GameObject EvilTornadoPos;
    public GameObject turnThing;
    public GameObject BodoComboFirst;
    public GameObject BodoComboSecond;
    TurnSystem turnSystem;

    // Start is called before the first frame update
    void Start()
    {
        turnSystem = turnThing.GetComponent<TurnSystem>();
        myAnimator.Play("hit effect");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BodoFlurry()
    {
        StartCoroutine(Flurry());
    }

    private IEnumerator Flurry()
    {

            float randomY = Random.Range(0.499f, 1.384f);
            float randomX = Random.Range(6.73f, 7.531f);
            //BodoAttackPos.transform.position = new Vector2(6.73f, 1.384f);
            
            Object me = Resources.Load("hit effect_0");
            if (me)
            {
                GameObject hit = (GameObject)Instantiate(me);
                hit.transform.position = new Vector2(randomX, randomY);
                yield return new WaitForSeconds(.267f);
                Destroy(hit);
            }
            yield return new WaitForSeconds(.2f);
        
    }

    public void EvilBananaTornado()
    {
        StartCoroutine(EvilBananaTornadoHits());
    }

    private IEnumerator EvilBananaTornadoHits()
    {
        Object me = Resources.Load("hit effect_0");
        if (me)
        {
            GameObject hit = (GameObject)Instantiate(me);
            hit.transform.position = EvilTornadoPos.transform.position;
            yield return new WaitForSeconds(.267f);
            Destroy(hit);
        }
    }

    public void BodoCombinationFirst()
    {
        StartCoroutine(BodoComboOne());
    }

    private IEnumerator BodoComboOne()
    {
        Object me = Resources.Load("hit effect_0");
        if (me)
        {
            GameObject hit = (GameObject)Instantiate(me);
            hit.transform.position = BodoComboFirst.transform.position;
            yield return new WaitForSeconds(.267f);
            Destroy(hit);
        }
    }

    public void BodoCombinationSecond()
    {
        StartCoroutine(BodoComboTwo());
    }

    private IEnumerator BodoComboTwo()
    {
        Object me = Resources.Load("hit effect_0");
        if (me)
        {
            GameObject hit = (GameObject)Instantiate(me);
            hit.transform.position = BodoComboSecond.transform.position;
            yield return new WaitForSeconds(.267f);
            Destroy(hit);
        }
    }


}
