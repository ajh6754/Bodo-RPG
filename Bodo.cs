using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 *  Bodo is the script that deals with the Bodo instance.
 */
public class Bodo : MonoBehaviour
{
    public Camera camera;
    public GameObject cameraLoc;
    public GameObject cameraKickLoc;
    public GameObject turnThing;
    TurnSystem turnSystem;

    public GameObject player;
    public Animator myAnimator;
    public Transform playerStartPos;
    public Transform currentPlayerPos;


    public GameObject attackPosition;
    public Transform attackPos;
    public GameObject flurry;

    public GameObject dropKickStart;
    public GameObject dropKickPos;
    public int fallSpeed = 50;

    public GameObject hitEffect;
    //public Vector2 currPos = ;
    HitEffect HitScript;

    public GameObject explosion;
    public GameObject PointerAttack;
    public GameObject PointerLight;
    public GameObject BodoHeal; //DON'T FORGET! ADD OTHER HEALS FOR BANANA AND LORENZO
    public GameObject BodoHealText;
    public GameObject BodoSPRestoreText;
    
    //boxcolliders

    void Start()
    {
        attackPos = attackPosition.GetComponent<Transform>();
        turnSystem = turnThing.GetComponent<TurnSystem>();
        HitScript = hitEffect.GetComponent<HitEffect>();
    }

    public void BodoIdle()
    {
        myAnimator.Play("Bodo Idle");
    }

    public void BodoAttack()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        myAnimator.Play("Bodo Dash");
        while (player.GetComponent<Transform>().position != attackPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackPos.position, 25 * Time.deltaTime);
            yield return null;
        }
        myAnimator.Play("Bodo Punch No Arm");
        flurry.SetActive(true);
        float timer = 0.0f;
        int count = 0;
        while(timer < 2f)
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
            if(count >= 25 && count < 50)
            {
                transform.Translate(Vector2.left * Time.deltaTime);
            }
            
            count++;
            yield return null;
        }
        flurry.SetActive(false);
        if(turnSystem.round == Round.ROUND1)
        {
            turnSystem.EvilBanUnit.currHP -= (int)Random.Range(80f, 130f);
        }
        myAnimator.Play("Bodo Backdash");
        while (player.GetComponent<Transform>().position != playerStartPos.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerStartPos.position, 25 * Time.deltaTime);
            yield return null;
        }
        myAnimator.Play("Bodo Idle");
        if(turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
        yield return null;
    }

    public void BodoCombination()
    {
        StartCoroutine(Combination());
    }

    private IEnumerator Combination()
    {
            turnSystem.BodoUnit.currSP -= 30;
            myAnimator.Play("Bodo Dash");
            while (player.GetComponent<Transform>().position != attackPos.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, attackPos.position, 25 * Time.deltaTime);
                yield return null;
            }
            myAnimator.Play("Bodo Punch No Arm");
            flurry.SetActive(true);
            float timer = 0.0f;
            int count = 0;
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
            flurry.SetActive(false);
            yield return new WaitForSeconds(.1f);
            player.transform.Translate(Vector2.right * 1);
            myAnimator.Play("Bodo Kick Up");
            yield return new WaitForSeconds(.7f);
            turnSystem.bodo = BodoAction.COMBINATION2;
            myAnimator.Play("Bodo Dropkick");
            camera.transform.position = cameraKickLoc.transform.position;
            player.GetComponent<Transform>().position = dropKickStart.transform.position;
            while (player.GetComponent<Transform>().position != dropKickPos.transform.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, dropKickPos.transform.position, 35 * Time.deltaTime);
                yield return null;
            }
            timer = 0f;
            yield return new WaitForSeconds(.25f);
            camera.transform.position = cameraLoc.transform.position;
            yield return new WaitForSeconds(2.25f);
            myAnimator.Play("Bodo Backdash");
            while (player.GetComponent<Transform>().position != playerStartPos.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerStartPos.position, 25 * Time.deltaTime);
                yield return null;
            }
            myAnimator.Play("Bodo Idle");
            if (turnSystem.round == Round.ROUND1)
            {
                turnSystem.EnemyBananaTurn();
            }
            yield return null;
    }

    public void BodoImpetum()
    {
        StartCoroutine(Impetum());
    }

    private IEnumerator Impetum()
    {
        turnSystem.BodoUnit.currSP -= 40;
        myAnimator.Play("Bodo Explode");
        yield return new WaitForSeconds(1.75f);
        Instantiate(explosion);
        yield return new WaitForSeconds(2.2f);
        myAnimator.Play("Bodo Idle");
        if (turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
    }

    public void BodoSanatio()
    {
        StartCoroutine(Sanatio());
    }

    private IEnumerator Sanatio()
    {
        turnSystem.BodoUnit.currSP -= 25;
        myAnimator.Play("Bodo Heal"); 
        yield return new WaitForSeconds(1.75f);
        Instantiate(BodoHeal);
        yield return new WaitForSeconds(2.2f);
        Instantiate(BodoHealText);
        Instantiate(BodoSPRestoreText);
        turnSystem.BodoUnit.currHP += 500;
        turnSystem.BodoUnit.currSP += 125;
        myAnimator.Play("Bodo Idle");
        if (turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
    }

    public void BodoPointer()
    {
        StartCoroutine(Pointer());
    }

    private IEnumerator Pointer()
    {
        turnSystem.BodoUnit.currSP -= 40;
        yield return new WaitForSeconds(.5f);
        myAnimator.Play("Bodo Pointer Startup");
        yield return new WaitForSeconds(0.5f);
        //PointerLight.SetActive(true);
        Instantiate(PointerAttack);
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
        myAnimator.Play("Bodo Idle");
        if (turnSystem.round == Round.ROUND1)
        {
            turnSystem.EnemyBananaTurn();
        }
    }

    public void BodoGuard()
    {
        myAnimator.Play("Bodo Guard");
    }

    public void BodoDeath()
    {
        myAnimator.Play("Bodo Death");
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(turnSystem.state == BattleState.ENEMYTURN && turnSystem.round == Round.ROUND1 && turnSystem.evilBan == EvilBananaAction.TORNADO && turnSystem.bodo != BodoAction.GUARD) //Banana Tornado
        {
            myAnimator.Play("Bodo Damage");
            HitScript.EvilBananaTornado();
            if (turnSystem.bodo == BodoAction.GUARD)
            {
                int damage = (int)Random.Range(15f, 25f);
                turnSystem.BodoDamage(damage);
                turnSystem.BodoUnit.currHP -= damage;
            }
            else
            {
                int damage = (int)Random.Range(7f, 10f);
                turnSystem.BodoDamage(damage);
                turnSystem.BodoUnit.currHP -= damage;
            }
            StartCoroutine(Hit());
        }
        else if(turnSystem.state == BattleState.ENEMYTURN && turnSystem.round == Round.ROUND1 && turnSystem.evilBan == EvilBananaAction.TORNADO && turnSystem.bodo == BodoAction.GUARD)
        {
            HitScript.EvilBananaTornado();
            if (turnSystem.bodo == BodoAction.GUARD)
            {
                int damage = (int)Random.Range(15f, 25f);
                turnSystem.BodoDamage(damage);
                turnSystem.BodoUnit.currHP -= damage;
            }
            else
            {
                int damage = (int)Random.Range(7f, 10f);
                turnSystem.BodoDamage(damage);
                turnSystem.BodoUnit.currHP -= damage;
            }
            StartCoroutine(Hit());
        }

        if(turnSystem.bodo == BodoAction.COMBINATION2 && turnSystem.state == BattleState.BODOATTACK)
        {
            //fallSpeed = 5;
        }
    }

    private IEnumerator Hit() //later, test with the transform.position.translate shit
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
