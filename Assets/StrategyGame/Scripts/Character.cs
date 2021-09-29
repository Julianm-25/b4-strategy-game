using UnityEngine;

/**
 * @author Teddy Dai
 */

/// <summary>
/// base class for a character with abstract methods for attack, as different class (melee, gunner, support) has different attacks
/// </summary>
public abstract class Character : MonoBehaviour
{
    public int maxHealth;
    /// <summary>
    /// character health point
    /// </summary>
    public int currentHealth;
    /// <summary>
    /// character action point
    /// </summary>
    public int actionPoints;
    /// <summary>
    /// maximum AP to begin with
    /// </summary>
    /// <returns></returns>
    public int maxAP;
    /// <summary>
    /// how far can a player move
    /// </summary>
    public int maxMovepoints;
    /// <summary>
    /// how many movements a player has
    /// </summary>
    public int currentMovepoints;
    /// <summary>
    /// how far away can the character execute an action to others
    /// </summary>
    public int attackRange;
    /// <summary>
    /// how strong is the character's attack
    /// </summary>
    public int attack;
    /// <summary>
    /// name of this character class
    /// </summary>
    public string className;
    /// <summary>
    /// which team does this character belong to, to check enemy/friend
    /// </summary>
    public int teamID;

    /// <summary>
    /// increase ap of this character by a number
    /// </summary>
    /// <param name="by">default to add by 1</param>
    /// <returns></returns>
    public int IncreaseAP(int by = 1)
    {
        return actionPoints += by;
    }

    /// <summary>
    /// decrease ap of this character by a number.
    /// calls IncreaseAP passing the negative of this param
    /// </summary>
    /// <param name="by">default to reduce by 1</param>
    /// <returns></returns>
    public int DecreaseAP(int by = 1)
    {
        return IncreaseAP(-by);
    }

    /// <summary>
    /// increase hp of this character by a number
    /// </summary>
    /// <param name="by">default to add by 1</param>
    /// <returns></returns>
    public int IncreaseHP(int by = 1)
    {
        return currentHealth += by;
    }

    /// <summary>
    /// decrease hp of this character by a number.
    /// calls IncreaseHP passing the negative of this param
    /// </summary>
    /// <param name="by">default to reduce by 1</param>
    /// <returns></returns>
    public int DecreaseHP(int by = 1)
    {
        return IncreaseHP(-by);
    }
    
    /// <summary>
    /// move this character to given x z coordinate while keeping the y unchanged.
    /// </summary>
    /// <param name="x">x pos</param>
    /// <param name="z">y pos</param>
    public void MoveToXZ(float x, float z)
    {
        transform.position = new Vector3(x, transform.position.y, z);
    }
    /// <summary>
    /// execute the attack action for this character against the given character
    /// the GameManager would call this, and decide whether or not it the attach is allowed  
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public abstract void DoAttackActionXZ(Character target);
}
