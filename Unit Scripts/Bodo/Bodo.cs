using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Bodo is the script that deals with the Bodo instance.
 *
 *  @author ajh6754 (albert2417)
 */
public class Bodo : MonoBehaviour
{
    // Variables to deal with the global aspects of the game
    public Camera camera;
    public GameObject cameraLoc;
    public GameObject cameraKickLoc;
    public GameObject turnThing;
    TurnSystem turnSystem;

    // Bodo player variables
    public GameObject player;
    public Animator myAnimator;

    // Bodo position variables
    public Transform playerStartPos;
    public Transform currentPlayerPos;

    // Attacking position variables
    public GameObject attackPosition;
    public Transform attackPos;

    // Flurry attack
    public GameObject flurry;

    // Dropkick variables
    public GameObject dropKickStart;
    public GameObject dropKickPos;
    public int fallSpeed = 50;

    // Hit effect variables
    public GameObject hitEffect;
    //public Vector2 currPos = ;
    HitEffect HitScript;

    // Explosion variable
    public GameObject explosion;

    // Pointer attack variables
    public GameObject PointerAttack;
    public GameObject PointerLight;

    // Healing variables
    public GameObject BodoHeal; //DON'T FORGET! ADD OTHER HEALS FOR BANANA AND LORENZO
    public GameObject BodoHealText;
    public GameObject BodoSPRestoreText;
    
    /// Start will initialize Bodo's attacking position, turnsystem, and hit 
    /// effect variables.
    /// 
    void Start()
    {
        attackPos = attackPosition.GetComponent<Transform>();
        turnSystem = turnThing.GetComponent<TurnSystem>();
        HitScript = hitEffect.GetComponent<HitEffect>();
    }

    /// BodoIdle simply plays the idle animation
    ///
    public void BodoIdle()
    {
        myAnimator.Play("Bodo Idle");
    }

    /// BodoAttack begins Bodo's attacking routine
    /// 
    public void BodoAttack()
    {
        StartCoroutine(Attack());
    }

    /// Private coroutine for Bodo attacking the enemy.
    /// 
    private IEnumerator Attack()
    {
        // Plays dash anim
        myAnimator.Play("Bodo Dash");

        // Loop for changing the position of Bodo until enemy is reached
        while (player.GetComponent<Transform>().position != attackPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                                      attackPos.position, 25 * Time.deltaTime);
            yield return null;
        }

        // Plays Bodo punching animation
        myAnimator.Play("Bodo Punch No Arm");

        // enables Bodo's arms to start throwing hands
        flurry.SetActive(true);

        // These variables and while loop are used for changing Bodo's position
        // while spawning hit effects
        float timer = 0.0f;
        int count = 0;
        while(timer < 2f)
        {
            // timer updated
            timer += Time.deltaTime;

            // Moves five times right, five times left, repeats

            if (count % 3 == 0)
            {   // Once count is high enough, spawn a hit effect
                HitScript.BodoFlurry();   
            }
            if (count >= 6)
            {   // Resets count for next round of movement
                count = 0;
            }
            if (count < 3)
            {   // Moves to the right
                transform.Translate(Vector2.right * Time.deltaTime);
            }
            if(count >= 3 && count < 6)
            {   // Moves to the left
                transform.Translate(Vector2.left * Time.deltaTime);
            }
            
            // count updated
            count++;
            yield return null;
        }

        // Disables flurry arms
        flurry.SetActive(false);

        // This set of conditionals deals with which character to remove
        // damage from (INCOMPLETE)
        if(turnSystem.round == Round.ROUND1)
        {
            turnSystem.EvilBanUnit.currHP -= (int)Random.Range(80f, 130f);
        }

        // Plays backdash and moves Bodo back to Neutral position
        myAnimator.Play("Bodo Backdash");
        while (player.GetComponent<Transform>().position != playerStartPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                            playerStartPos.position, 25 * Time.deltaTime);
            yield return null;
        }

        // Plays default idle animation and ends turn
        myAnimator.Play("Bodo Idle");

        // This set of conditionals deals with whose turn is next (INCOMPLETE)
        if(turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
        yield return null;
    }

    /// BodoCombination will begin the Combination coroutine attack
    /// 
    public void BodoCombination()
    {
        StartCoroutine(Combination());
    }

    /// Combination will run the Combination attack
    /// 
    private IEnumerator Combination()
    {
        // updates SP
        turnSystem.BodoUnit.currSP -= 30;

        // Moves character to enemy
        myAnimator.Play("Bodo Dash");
        while (player.GetComponent<Transform>().position != attackPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackPos.position, 25 * Time.deltaTime);
            yield return null;
        }

        // Begins flurry attack
        myAnimator.Play("Bodo Punch No Arm");
        flurry.SetActive(true);
        float timer = 0.0f;
        int count = 0;

        // could use another method for this while loop, reused elsewhere
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            //go five times right, five times left
            if (count % 25 == 0)
            {
                HitScript.BodoFlurry();
            }
            if (count >= 50)
            {
                count = 0;
            }
            if (count < 25)
            {
                transform.Translate(Vector2.right * Time.deltaTime);
            }
            if (count >= 25 && count < 50)
            {
                transform.Translate(Vector2.left * Time.deltaTime);
            }

            count++;
            yield return null;
        }

        // clears the flurry of arms for next attack
        flurry.SetActive(false);
        yield return new WaitForSeconds(.1f);

        // Positions Bodo for upward kick
        player.transform.Translate(Vector2.right * 1);
        myAnimator.Play("Bodo Kick Up");
        yield return new WaitForSeconds(.7f);

        // Updates turnsystem and changes backdrop, dropkick attack plays
        turnSystem.bodo = BodoAction.COMBINATION2;
        myAnimator.Play("Bodo Dropkick");
        camera.transform.position = cameraKickLoc.transform.position;
        player.GetComponent<Transform>().position = dropKickStart.transform.position;

        // Moves Bodo to dropkick position
        while (player.GetComponent<Transform>().position != dropKickPos.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, dropKickPos.transform.position, 35 * Time.deltaTime);
            yield return null;
        }
        timer = 0f;

        // updates camera view back to battleground
        yield return new WaitForSeconds(.25f);
        camera.transform.position = cameraLoc.transform.position;

        // Bodo moves back into neutral position
        yield return new WaitForSeconds(1.8f);
        myAnimator.Play("Bodo Backdash");
        while (player.GetComponent<Transform>().position != playerStartPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerStartPos.position, 25 * Time.deltaTime);
            yield return null;
        }

        // Idle animation plays and turnsystem updated
        myAnimator.Play("Bodo Idle");
        if (turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
        yield return null;
    }

    /// BodoImpetum begins the Impetum attack coroutine
    /// 
    public void BodoImpetum()
    {
        StartCoroutine(Impetum());
    }

    /// Impetum will play out the impetum attack
    /// 
    private IEnumerator Impetum()
    {
        // updates SP, plays animation
        turnSystem.BodoUnit.currSP -= 40;
        myAnimator.Play("Bodo Explode");
        yield return new WaitForSeconds(1.75f);

        // creates explosion
        Instantiate(explosion);
        yield return new WaitForSeconds(2.2f);

        // Sets animation to idle and updates turnsystem
        myAnimator.Play("Bodo Idle");
        if (turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
    }

    /// BodoSanatio will start the Sanatio coroutine
    ///
    public void BodoSanatio()
    {
        StartCoroutine(Sanatio());
    }

    /// Sanatio will perform the healing move
    /// 
    private IEnumerator Sanatio()
    {
        // updates SP, plays animation
        turnSystem.BodoUnit.currSP -= 25;
        myAnimator.Play("Bodo Heal"); 
        yield return new WaitForSeconds(1.75f);

        // Instantiates healing aura
        Instantiate(BodoHeal);
        yield return new WaitForSeconds(2.2f);

        // Instantiates the text for Health Up
        Instantiate(BodoHealText);
        Instantiate(BodoSPRestoreText); // consider removing
        turnSystem.BodoUnit.currHP += 500;
        turnSystem.BodoUnit.currSP += 125; // consider removing

        // Plays idle animation and updates turnsystem
        myAnimator.Play("Bodo Idle");
        if (turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
    }

    /// BodoPointer begins the Pointer coroutine
    /// 
    public void BodoPointer()
    {
        StartCoroutine(Pointer());
    }

    /// Pointer will play the pointer animation
    /// 
    private IEnumerator Pointer()
    {
        // Updates SP
        turnSystem.BodoUnit.currSP -= 40;
        yield return new WaitForSeconds(.5f);

        // Plays animation, need to update this portion for efficiency
        myAnimator.Play("Bodo Pointer Startup");
        yield return new WaitForSeconds(0.5f);
        //PointerLight.SetActive(true);
        Instantiate(PointerAttack);
        yield return new WaitForSeconds(0.1f);
        myAnimator.Play("Bodo Pointer Slash 1");
        yield return new WaitForSeconds(.0835f);
        //PointerLight.SetActive(false);
        yield return new WaitForSeconds(.0835f);
        myAnimator.Play("Bodo Pointer Slash 2");
        //PointerLight.SetActive(true);
        yield return new WaitForSeconds(.0835f);
        //PointerLight.SetActive(false);
        yield return new WaitForSeconds(.0835f);
        myAnimator.Play("Bodo POinter Slash 3");
        //PointerLight.SetActive(true);
        yield return new WaitForSeconds(.0835f);
        //PointerLight.SetActive(false);
        yield return new WaitForSeconds(1f);

        // Plays idle animation, updates turnsystem
        myAnimator.Play("Bodo Idle");
        if (turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
    }

    /// BodoGuard plays the guarding animation
    /// 
    public void BodoGuard()
    {
        myAnimator.Play("Bodo Guard");
    }

    /// BodoDeath plays the death animation
    public void BodoDeath()
    {
        myAnimator.Play("Bodo Death");
    }

    /// OnTriggerEnter2D will deal with various collisions (typically damage)
    /// 
    void OnTriggerEnter2D(Collider2D target)
    {
        // This conditional is for the BananaTornado attack.
        if(turnSystem.state == BattleState.ENEMYTURN && 
                           turnSystem.round == Round.ROUND1 && 
                                turnSystem.evilBan == EvilBananaAction.TORNADO)
        {
            // spawns hit effect
            HitScript.EvilBananaTornado();

            // temp variable to hold damage
            int damage = 0;

            // This set of conditionals deals with Bodo's guard
            if (turnSystem.bodo != BodoAction.GUARD)
            {
                myAnimator.Play("Bodo Damage");
                damage = (int)Random.Range(15f, 25f);
            }
            else
                damage = (int)Random.Range(7f, 10f);

            // Updates damage and turnsystem
            turnSystem.BodoDamage(damage);
            turnSystem.BodoUnit.currHP -= damage;

            // Starts coroutine for the little jiggle, may be moved
            StartCoroutine(Hit());
        }

        // i'm pretty sure this was trying to test how fast he moved but it might be useless
        if(turnSystem.bodo == BodoAction.COMBINATION2 && turnSystem.state == BattleState.BODOATTACK)
        {
            //fallSpeed = 5;
        }
    }

    /// Hit deals with changing the position when Bodo is hit
    /// 
    private IEnumerator Hit() // MUST UPDATE TRANSFORMATION SEQUENCE, VERY FLAWED
    {
        player.transform.position = playerStartPos.transform.position;
        player.transform.position = new Vector2(-6.37f, .65f);
        yield return new WaitForSeconds(.05f);
        player.transform.position = new Vector2(-6.87f, .65f);
        yield return new WaitForSeconds(.05f);
        player.transform.position = new Vector2(-6.42f, .65f);
        yield return new WaitForSeconds(.05f);
        player.transform.position = new Vector2(-6.82f, .65f);
        yield return new WaitForSeconds(.05f);
        player.transform.position = new Vector2(-6.47f, .65f);
        yield return new WaitForSeconds(.05f);
        player.transform.position = new Vector2(-6.77f, .65f);
        yield return new WaitForSeconds(.05f);
        player.transform.position = playerStartPos.transform.position;
    }
}
