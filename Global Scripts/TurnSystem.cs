using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Round { ROUND1, ROUND2, ROUND3 }
public enum BattleState { START, BODOTURN, BODOSPECIAL, BODOSELECT, BANANATURN, BANANASPECIAL, BANANASELECT, LORENZOTURN, LORENZOSPECIAL, LORENZOSELECT, BODOATTACK, BANANAATTACK, LORENZOATTACK, ENEMYTURN, WON, LOST }
public enum BodoAction { ATTACK, COMBINATION1, COMBINATION2, IMPETUM, SANATIO, POINTER, GUARD }
public enum BananaAction { ATTACK, TORNADO, FOCUS, HEAL, ASSAULT, GUARD }
public enum LorenzoAction { ATTACK, HAZELNUT, BLACK, RUINED, DIO, GUARD }
public enum EvilBananaAction { ATTACK, TORNADO, FOCUS, ASSAULT }
public enum EvilLorenzoAction { ATTACK, BLACK, RUINED, DIO }
public enum EvilBodoAction { ATTACK, POWERUP, EXPLOSION, CHARGE, SUPEREXPLOSION }

public class TurnSystem : MonoBehaviour
{
    public Round round;
    public BattleState state;
    public BodoAction bodo;
    public BananaAction banana;
    public LorenzoAction lorenzo;
    public EvilBananaAction evilBan;
    public EvilLorenzoAction evilLor;
    public EvilBodoAction evilBodo;

    //Character Objects
    public GameObject Bodo;
    public GameObject Banana;
    public GameObject Lorenzo;
    public GameObject EnemyBan;
    public GameObject EnemyLorenzo;
    public GameObject EnemyBodo;


    //public GameObject fade;

    //positions
    public Transform BodoPos;
    public Transform EnemyPos;
    public Transform BananaPos;
    public Transform LorenzoPos;

    //display of health
    public Text BodoHealth;
    public Text BodoSP;
    public Text BananaHealth;
    public Text BananaSP;
    public Text LorenzoHealth;
    public Text LorenzoSP;

    // Dialogue variables
    public Button dialogue;
    public Text dialogueText;

    //Units and Character Scripts

    public Unit BodoUnit;
    public Unit BananaUnit;
    public Unit LorenzoUnit;
    public Unit EvilBanUnit;
    public Unit EvilLorenzoUnit;
    public Unit EvilBodoUnit;

    Bodo BodoScript;
    Banana BananaScript;
    Lorenzo LorenzoScript;
    EvilBanana EvilBananaScript;
    EvilLorenzo EvilLorenzoScript;
    EvilBodo EvilBodoScript;

    

    //Cursors 

    public GameObject BodoCursor;
    //public GameObject BananaCursor;
    //public GameObject LorenzoCursor;

    //Damage

    public GameObject BodoDamagePoints;
    DamagePoppup BodoDamagePopUp;
    public GameObject EnemyDamagePoints;
    EnemyDamagePoints EnemyDamagePopUp;
    public GameObject EnemyDamagePointsDropkick;
    EnemyDamagePoints EnemyDamagePopUpDropKick;

    //REGULAR BUTTONS
    public Button attack;
    public Button special;
    public Button guard;
    public Button back;

    //BODO SPECIAL BUTTONS
    public Button combination;
    public Button impetum;
    public Button sanatio;
    public Button pointer;

    // Start is called before the first frame update
    void Start()
    {
        // Initializes Battle Scene
        state = BattleState.START;
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "First Battle")
        {
            round = Round.ROUND1;
        }
        if(scene.name == "Second Battle")
        {
            round = Round.ROUND2;
        }
        if(scene.name == "Final Battle")
        {
            round = Round.ROUND3;
        }
        setupBattle();
    }

    /**
     * Creates the instances of everything in the battle scene.
     */
    void setupBattle() 
    {
        //start fade. NOT DONE

        //Grabbing every character's starting position
        BodoPos = Bodo.transform;
        BodoUnit = Bodo.GetComponent<Unit>();
        BodoScript = Bodo.GetComponent<Bodo>();
        BodoHealth.text = "HP: " + BodoUnit.currHP + "/" + BodoUnit.maxHP;
        BodoSP.text = "SP: " + BodoUnit.currSP + "/" + BodoUnit.maxSP;
        BodoDamagePopUp = BodoDamagePoints.GetComponent<DamagePoppup>();
        EnemyDamagePopUp = EnemyDamagePoints.GetComponent<EnemyDamagePoints>();
        EnemyDamagePopUpDropKick = EnemyDamagePointsDropkick.GetComponent<EnemyDamagePoints>();
        if (round == Round.ROUND1)
        {
            EnemyPos = EnemyBan.transform;
            EvilBanUnit = EnemyBan.GetComponent<Unit>();
            EvilBananaScript = EnemyBan.GetComponent<EvilBanana>();
            state = BattleState.BODOTURN;
            BodoTurn();
        }
        if(round == Round.ROUND2)
        {
            BananaPos = Banana.transform;
            BananaUnit = Banana.GetComponent<Unit>();
            EnemyPos = EnemyLorenzo.transform;
            EvilLorenzoUnit = EnemyLorenzo.GetComponent<Unit>();
            state = BattleState.BANANATURN;
            BananaTurn();
        }
        if(round == Round.ROUND3)
        {
            BananaPos = Banana.transform;
            BananaUnit = Banana.GetComponent<Unit>();
            LorenzoPos = Lorenzo.transform;
            LorenzoUnit = Lorenzo.GetComponent<Unit>();
            EnemyPos = EnemyBodo.transform;
            EvilBodoUnit = EnemyBodo.GetComponent<Unit>();
            state = BattleState.BANANATURN;
            BananaTurn();
        }

    }

    void Update()
    {   
        //ig put things here to make sure it doesn't do shit with the wrong rounds
        BodoHealth.text = "HP: " + BodoUnit.currHP + "/" + BodoUnit.maxHP;
        BodoSP.text = "SP: " + BodoUnit.currSP + "/" + BodoUnit.maxSP;
    }

    public void BodoTurn()
    {
        if (state != BattleState.BODOTURN || BodoUnit.currHP == 0)
        {
            if((round == Round.ROUND1 && BodoUnit.currHP == 0) 
                || (round == Round.ROUND2 && BodoUnit.currHP == 0 && BananaUnit.currHP == 0) 
                || (round == Round.ROUND3 && BodoUnit.currHP == 0 && BananaUnit.currHP == 0 && LorenzoUnit.currHP == 0))
            {   // If all party members have no HP, the game is lost.
                state = BattleState.LOST;
                GameOver();
            }
            if(round == Round.ROUND2 && BodoUnit.currHP == 0)
            {
                state = BattleState.BANANAATTACK;
                //add stuff, incomplete
            }
            if(round == Round.ROUND3 && BodoUnit.currHP == 0)
            {
                state = BattleState.LORENZOTURN;
                //add stuff, incomplete
            }
            return;
        }
        else
        {
            BodoScript.BodoIdle();
            BodoCursor.SetActive(true);
            attack.gameObject.SetActive(true);
            special.gameObject.SetActive(true);
            guard.gameObject.SetActive(true);
            if(round == Round.ROUND1)
            {
                back.gameObject.SetActive(false);
            }
            //put an else for when it's round2 or 3
        }
        
    }

    public void BananaTurn()
    {

    }

    public void LorenzoTurn()
    {

    }

    public void EnemyBananaTurn()
    {
        if(EvilBanUnit.currHP > 0)
        {
            state = BattleState.ENEMYTURN;
            EvilBananaScript.EvilBananaIdle();
            StartCoroutine(EvilBananaWait());
        }
        else
        {
            //make music fade out, then explode the enemy putting the enemy into it's death state, and then have Bodo do his idle animation.
            state = BattleState.WON;
        }
    }

    private IEnumerator EvilBananaWait() //does RNG to determine the Banana's next move
    {
        yield return new WaitForSeconds(2);
        int random = (int) Random.Range(0f, 2.99f);
        random = 0;
        if(random == 0)
        {
            evilBan = EvilBananaAction.TORNADO;
            EvilBananaScript.EvilBananaAttack();
        }
        if(random == 1)
        {
            EvilBananaScript.EvilBananaFocus();
        }
        if(random == 2)
        {
            EvilBananaScript.EvilBananaAssault();
        }
    }

    public void EnemyLorenzoTurn()
    {

    }

    public void EnemyBodoTurn()
    {

    }

    public void OnAttackButton()
    {
        
        attack.gameObject.SetActive(false);
        special.gameObject.SetActive(false);
        guard.gameObject.SetActive(false);
        if (state == BattleState.BODOTURN)
        {
            BodoCursor.SetActive(false);
            bodo = BodoAction.ATTACK;
            if(round == Round.ROUND1)
            {
                state = BattleState.BODOATTACK;
                BodoScript.BodoAttack();
            }
            //set an attack, and then do a check to see if the enemy is death
        }
        //else
    }

    public void OnSpecialButton()
    {
        attack.gameObject.SetActive(false);
        special.gameObject.SetActive(false);
        guard.gameObject.SetActive(false);
        if(state == BattleState.BODOTURN)
        {
            state = BattleState.BODOSPECIAL;
            combination.gameObject.SetActive(true);
            impetum.gameObject.SetActive(true);
            sanatio.gameObject.SetActive(true);
            pointer.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
        }
    }

    public void OnGuardButton()
    {
        attack.gameObject.SetActive(false);
        special.gameObject.SetActive(false);
        guard.gameObject.SetActive(false);
        if(state == BattleState.BODOTURN)
        {
            BodoCursor.SetActive(false);
            bodo = BodoAction.GUARD;
            BodoScript.BodoGuard();
            if(round == Round.ROUND1)
            {
                EnemyBananaTurn();
            }
        }
    }

    public void OnBackButton()
    {
        if(round == Round.ROUND1)
        {
            if(state == BattleState.BODOSPECIAL)
            {
                state = BattleState.BODOTURN;
                combination.gameObject.SetActive(false);
                impetum.gameObject.SetActive(false);
                sanatio.gameObject.SetActive(false);
                pointer.gameObject.SetActive(false);
                BodoTurn();
            }
            if(state == BattleState.BODOSELECT)
            {
                state = BattleState.BODOSPECIAL;
                OnSpecialButton();
            }
        }
    }

    public void CheckDeath()
    {

    }

    //BODO BUTTONS

    public void OnCombinationButton()
    {
        BodoCursor.SetActive(false);
        combination.gameObject.SetActive(false);
        impetum.gameObject.SetActive(false);
        sanatio.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        if(round == Round.ROUND1)
        {
            state = BattleState.BODOATTACK;
            bodo = BodoAction.COMBINATION1;
            back.gameObject.SetActive(false);
            BodoScript.BodoCombination();
        }
    }

    public void OnImpetumButton()
    {
        BodoCursor.SetActive(false);
        combination.gameObject.SetActive(false);
        impetum.gameObject.SetActive(false);
        sanatio.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        if (round == Round.ROUND1)
        {
            state = BattleState.BODOATTACK;
            bodo = BodoAction.IMPETUM;
            back.gameObject.SetActive(false);
            BodoScript.BodoImpetum();
        }
    }

    public void OnSanatioButton()
    {
        BodoCursor.SetActive(false);
        combination.gameObject.SetActive(false);
        impetum.gameObject.SetActive(false);
        sanatio.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        if (round == Round.ROUND1)
        {
            state = BattleState.BODOATTACK;
            bodo = BodoAction.SANATIO;
            back.gameObject.SetActive(false);
            BodoScript.BodoSanatio();
        }
    }

    public void OnPointerButton()
    {
        BodoCursor.SetActive(false);
        combination.gameObject.SetActive(false);
        impetum.gameObject.SetActive(false);
        sanatio.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        if (round == Round.ROUND1)
        {
            state = BattleState.BODOATTACK;
            bodo = BodoAction.POINTER;
            back.gameObject.SetActive(false);
            BodoScript.BodoPointer();
        }
    }

    //BANANA BUTTONS



    //LORENZO BUTTONS



    //DAMAGE

    public void BodoDamage(int damage)
    {
        BodoDamagePopUp.damageText.text = damage.ToString();
        Instantiate(BodoDamagePoints);
    }

    public void EnemyDamage(int damage)
    {
        EnemyDamagePopUp.damageText.text = damage.ToString();
        Instantiate(EnemyDamagePoints);
    }

    public void EnemyDamageDropkick(int damage)
    {
        EnemyDamagePopUpDropKick.damageText.text = damage.ToString();
        Instantiate(EnemyDamagePointsDropkick);
    }



    //SELECTIONS

    public void Select()
    {

    }

    public void GameOver()
    {

    }
}
