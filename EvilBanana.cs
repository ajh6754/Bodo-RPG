using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBanana : MonoBehaviour
{
    public GameObject camera;
    public GameObject turnThing;
    TurnSystem turnSystem;
    private BoxCollider2D myBoxCollider;
    public GameObject me;
    public Animator myAnimator;
    public GameObject hitEffect;
    HitEffect HitScript;
    public Vector2 hitEffectPos;
    public GameObject tornadoPosition;
    public Transform tornadoPos;
    public GameObject tornadoEndPosition;
    public Transform tornadoEndPos;
    public Transform startPos;
    public GameObject BodoAttackPos;
    public GameObject flurry;
    public GameObject BodoDropkickPos;
    public bool BodoSecondDone;

    // Start is called before the first frame update
    void Start()
    {
        HitScript = hitEffect.GetComponent<HitEffect>();
        myBoxCollider = me.GetComponent<BoxCollider2D>();
        turnSystem = turnThing.GetComponent<TurnSystem>();
    }

    public void EvilBananaIdle()
    {
        myAnimator.Play("Banana Idle");
        BodoSecondDone = false;
    }

    public void EvilBananaAttack()
    {
        StartCoroutine(tornadoAttack());
    }

    private IEnumerator tornadoAttack()
    {
        myAnimator.Play("Tornado Startup");
        yield return new WaitForSeconds(.333f);
        myAnimator.Play("Tornado Loop");
        while(me.GetComponent<Transform>().position != tornadoPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, tornadoPos.position, 15 * Time.deltaTime);
            yield return null;
        }
        float timer = 0f;
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        while (me.GetComponent<Transform>().position != tornadoEndPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, tornadoEndPos.position, 2 * Time.deltaTime);
            yield return null;
        }
        myAnimator.Play("Tornado End");
        yield return new WaitForSeconds(.333f);

        myAnimator.Play("Banana Idle");
        yield return new WaitForSeconds(.1f);
        myAnimator.Play("Banana Backdash");
        while (me.GetComponent<Transform>().position != startPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, 20 * Time.deltaTime);
            yield return null;
        }
        myAnimator.Play("Banana Idle");
        turnSystem.state = BattleState.BODOTURN;
        turnSystem.BodoTurn();
    }

    public void EvilBananaFocus()
    {
        myAnimator.Play("Tornado Loop");
    }

    public void EvilBananaAssault()
    {
        myAnimator.Play("Banana Summon");
    }

    void OnTriggerEnter2D(Collider2D target) //FIGURE OUT HOW TO FIND POINT OF COLLISION
    {
        if(target.tag == "Flurry")
        {
            myAnimator.Play("Banana Damage");
            int damage = (int)Random.Range(15f, 25f);
            turnSystem.EnemyDamage(damage);
            turnSystem.EvilBanUnit.currHP -= damage;
            StartCoroutine(Hit());
        }
        if(target.tag != "Flurry" && turnSystem.state == BattleState.BODOATTACK && turnSystem.bodo == BodoAction.COMBINATION1)
        {
            StopCoroutine(Hit());
            me.transform.position = startPos.transform.position;
            HitScript.BodoCombinationFirst();
            int damage = (int)Random.Range(45f, 60f);
            turnSystem.EnemyDamage(damage);
            turnSystem.EvilBanUnit.currHP -= damage;
            StartCoroutine(HitUp());
            
        }
        if (target.tag != "Flurry" && turnSystem.state == BattleState.BODOATTACK && turnSystem.bodo == BodoAction.COMBINATION2)
        {
            BodoSecondDone = true;
            StopCoroutine(HitUp());
            StopCoroutine(Hit());
            HitScript.BodoCombinationSecond();
            int damage = (int)Random.Range(70f, 80f);
            turnSystem.EnemyDamageDropkick(damage);
            turnSystem.EvilBanUnit.currHP -= damage;
            StartCoroutine(HitDown());
        }
        if(target.tag == "Explosion")
        {
            myAnimator.Play("Banana Damage");
            int damage = (int)Random.Range(300f, 450f);
            turnSystem.EnemyDamage(damage);
            turnSystem.EvilBanUnit.currHP -= damage;
        }
        if(target.tag == "The F")
        {
            StartCoroutine(Hit());
            myAnimator.Play("Banana Damage");
            int damage = (int)Random.Range(100f, 150f);
            turnSystem.EnemyDamage(damage);
            turnSystem.EvilBanUnit.currHP -= damage;
        }
    }

    private IEnumerator Hit()
    {
        me.transform.position = startPos.transform.position;
        me.transform.position = new Vector2(7.25f, .65f);
        yield return new WaitForSeconds(.05f);
        me.transform.position = new Vector2(6.75f, .65f);
        yield return new WaitForSeconds(.05f);
        me.transform.position = new Vector2(7.2f, .65f);
        yield return new WaitForSeconds(.05f);
        me.transform.position = new Vector2(6.8f, .65f);
        yield return new WaitForSeconds(.05f);
        me.transform.position = new Vector2(7.15f, .65f);
        yield return new WaitForSeconds(.05f);
        me.transform.position = new Vector2(6.85f, .65f);
        yield return new WaitForSeconds(.05f);
        me.transform.position = startPos.transform.position;
    }

    private IEnumerator HitUp()
    {
        while (me.GetComponent<Transform>().position != BodoDropkickPos.transform.position && BodoSecondDone == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, BodoDropkickPos.transform.position, 20 * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator HitDown()
    {
        while (me.GetComponent<Transform>().position != startPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, 30 * Time.deltaTime);
            yield return null;
        }
        int damage = (int)Random.Range(50f, 60f);
        turnSystem.EnemyDamage(damage);
        turnSystem.EvilBanUnit.currHP -= damage;
        camera.transform.position = new Vector2(-0.06f, 1.19f);
        yield return new WaitForSeconds(.05f);
        camera.transform.position = new Vector2(-1.06f, .19f);
        yield return new WaitForSeconds(.05f);
        camera.transform.position = new Vector2(-.31f, .94f);
        yield return new WaitForSeconds(.05f);
        camera.transform.position = new Vector2(-.81f, .44f);
        yield return new WaitForSeconds(.05f);
        camera.transform.position = new Vector2(-.66f, .79f);
        yield return new WaitForSeconds(.05f);
        camera.transform.position = new Vector2(-.46f, .59f);
        yield return new WaitForSeconds(.05f);
        camera.transform.position = new Vector2(-.56f, .69f);
        yield return null;

    }



}
