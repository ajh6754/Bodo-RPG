using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Unit file's purpose is to update and keep track of a character's 
 * HP, SP, and whether or not they are dead. It ensures no overload or
 * underload of variables.
 * 
 * @author ajh6754 (albert2417)
 */
public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitDamage;
    public int maxHP;
    public int currHP;
    public int maxSP;
    public int currSP;
    
    /// Update will continuously check to make sure that a unit's SP or HP
    /// is what they should be. It may be necessary to create a call method to
    /// reduce overhead, because calling this every frame seems unnecessary.
    /// 
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
