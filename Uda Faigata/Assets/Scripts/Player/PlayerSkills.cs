using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<Skill> ActiveSkills = new List<Skill>(2);
    [HideInInspector]
    public List<Skill> AllSkills;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) &&
            Player.Aim.EnemyTarget)
        {
            ActiveSkills[0].UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ActiveSkills[1].UseSkill();
        }
        else if(Input.GetKeyUp(KeyCode.V))
        {
            ActiveSkills[1].DeactivateSkill();
        }
    }
}
