using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitDamage;
    public int maxHP;
    public int currHP;
    public int maxSP;
    public int currSP;
    
    void Update()
    {
        if(currHP < 0)
        {
            currHP = 0;
        }
        if(currHP > maxHP)
        {
            currHP = maxHP;
        }
        if(currSP > maxSP)
        {
            currSP = maxSP;
        }
    }
    
}
