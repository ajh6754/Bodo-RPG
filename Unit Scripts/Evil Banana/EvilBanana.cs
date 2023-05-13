using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  EvilBanana is the script that deals with the EvilBanana instance.
 *
 *  @author ajh6754 (albert2417)
 */
public class EvilBanana : MonoBehaviour
{
    // Variables to deal with global aspects of the game
    public GameObject camera;
    public GameObject turnThing;
    TurnSystem turnSystem;

    // EvilBanana player variables
    private BoxCollider2D myBoxCollider;
    public GameObject me;
    public Animator myAnimator;

    // Hit effect variables
    public GameObject hitEffect;
    HitEffect HitScript;
    public Vector2 hitEffectPos;

    // Tornado attack variables
    public GameObject tornadoPosition;
    public Transform tornadoPos;
    public GameObject tornadoEndPosition;
    public Transform tornadoEndPos;

    // Positioning variables
    public Transform startPos;
    public GameObject BodoAttackPos;
    public GameObject flurry;
    public GameObject BodoDropkickPos;

    // Variable for Bodo combination attack
    public bool BodoSecondDone;

    /// Start is called before the first frame update, initializes hitscript,
    /// box collider, and turnsystem for instance.
    /// 
    void Start()
    {
        HitScript = hitEffect.GetComponent<HitEffect>();
        myBoxCollider = me.GetComponent<BoxCollider2D>();
        turnSystem = turnThing.GetComponent<TurnSystem>();
    }

    /// EvilBananaIdle will play the idle animation of EvilBanana
    /// 
    public void EvilBananaIdle()
    {
        myAnimator.Play("Banana Idle");
        BodoSecondDone = false;
    }

    /// EvilBananaAttack will pick a random attack and then begin that attack's
    /// specified coroutine.
    /// 
    public void EvilBananaAttack()
    {
        // MUST PICK RANDOM NUMBER, THEN CHOOSES ATTACK BASED ON THAT
        StartCoroutine(TornadoAttack());
    }

    /// TornadoAttack will play the Tornado animation and deal with the
    /// damage and all other things.
    /// 
    private IEnumerator TornadoAttack()
    {
        // Plays animation, moves Banana to the enemy
        myAnimator.Play("Tornado Startup");
        yield return new WaitForSeconds(.333f);
        myAnimator.Play("Tornado Loop");
        while(me.GetComponent<Transform>().position != tornadoPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, tornadoPos.position, 15 * Time.deltaTime);
            yield return null;
        }

        // timer variable for keeping track of time
        float timer = 0f;

        // Keeps banana attacking for 2 seconds
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        // Moves banana into ending animation position, and ends animation
        while (me.GetComponent<Transform>().position != tornadoEndPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, tornadoEndPos.position, 2 * Time.deltaTime);
            yield return null;
        }
        myAnimator.Play("Tornado End");
        yield return new WaitForSeconds(.333f);

        // Moves banana back into neutral position
        myAnimator.Play("Banana Idle");
        yield return new WaitForSeconds(.1f);
        myAnimator.Play("Banana Backdash");
        while (me.GetComponent<Transform>().position != startPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, 20 * Time.deltaTime);
            yield return null;
        }

        // Plays idle animation and updates turnsystem
        myAnimator.Play("Banana Idle");
        turnSystem.state = BattleState.BODOTURN;
        turnSystem.BodoTurn();
    }

    /// EvilBananaFocus will deal with the Focus move, raises ATK and DEF?
    /// 
    public void EvilBananaFocus()
    {
        myAnimator.Play("Tornado Loop");
    }

    /// EvilBananaAssault will deal with the Banana barrage attack.
    /// 
    public void EvilBananaAssault()
    {
        myAnimator.Play("Banana Summon");
    }

    /// OnTriggerEnter2D deals with what to do when colliding with something.
    /// 
    void OnTriggerEnter2D(Collider2D target) //FIGURE OUT HOW TO FIND POINT OF COLLISION
    {
        // damage variable created for updating HP
        int damage = 0;

        // This set of conditionals is for very specific circumstances

        if(target.tag == "Flurry")
        {   // deals with getting flurry attacked
            myAnimator.Play("Banana Damage");
            damage = (int)Random.Range(15f, 25f);
            turnSystem.EnemyDamage(damage);
            StartCoroutine(Hit());
        }
        if(target.tag != "Flurry" && turnSystem.state == BattleState.BODOATTACK && turnSystem.bodo == BodoAction.COMBINATION1)
        {   // Deals with initial kick of Bodo Combinaison
            
            // stops previous hit coroutine
            StopCoroutine(Hit());
            me.transform.position = startPos.transform.position;
            HitScript.BodoCombinationFirst();
            damage = (int)Random.Range(45f, 60f);
            turnSystem.EnemyDamage(damage);
            StartCoroutine(HitUp());
        }
        if (target.tag != "Flurry" && turnSystem.state == BattleState.BODOATTACK && turnSystem.bodo == BodoAction.COMBINATION2)
        {   // deals with dropkick of Bodo Combinaison

            // stops hitting coroutines
            BodoSecondDone = true;
            StopCoroutine(HitUp());
            StopCoroutine(Hit());
            HitScript.BodoCombinationSecond();
            damage = (int)Random.Range(70f, 80f);
            turnSystem.EnemyDamageDropkick(damage);
            StartCoroutine(HitDown());
        }
        if(target.tag == "Explosion")
        {   // deals with Impetum
            myAnimator.Play("Banana Damage");
            damage = (int)Random.Range(300f, 450f);
            turnSystem.EnemyDamage(damage);
        }
        if(target.tag == "The F")
        {   // deals with pointer attack
            StartCoroutine(Hit());
            myAnimator.Play("Banana Damage");
            damage = (int)Random.Range(100f, 150f);
            turnSystem.EnemyDamage(damage);
        }

        // updates HP
        turnSystem.EvilBanUnit.currHP -= damage;
    }

    /// Hit will deal with the animation done while being hit
    /// 
    private IEnumerator Hit() // MUST UPDATE METHOD, VERY FLAWED
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

    /// HitUp is the coroutine that deals with the hitting up portion of Bodo
    /// Combinaison attack.
    /// 
    private IEnumerator HitUp()
    {
        // Moves Banana into position
        while (me.GetComponent<Transform>().position != BodoDropkickPos.transform.position && BodoSecondDone == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, BodoDropkickPos.transform.position, 20 * Time.deltaTime);
            yield return null;
        }
    }

    /// HitDown is the coroutine that deals with Bodo's dropkick attack.
    /// 
    private IEnumerator HitDown()
    {
        // Moves banana back into neutral position
        while (me.GetComponent<Transform>().position != startPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, 30 * Time.deltaTime);
            yield return null;
        }

        // fall damage
        int damage = (int)Random.Range(50f, 60f);
        turnSystem.EnemyDamage(damage);
        turnSystem.EvilBanUnit.currHP -= damage;

        // Camera shake
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
