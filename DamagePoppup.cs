using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePoppup : MonoBehaviour
{
    public TextMesh damageText;
    public GameObject me;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(popUp());
    }

    /**
     * popUp is called when the object is created, it will move it up and to the left while displaying the damage taken.
     */
    private IEnumerator popUp()
    {
        float timer = 0f;
        while(timer < 0.25f)
        {
            timer += Time.deltaTime;
            transform.Translate(Vector2.left * 10 * Time.deltaTime);
            transform.Translate(Vector2.up * 10 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.1f);
        Destroy(me);
    }
}
