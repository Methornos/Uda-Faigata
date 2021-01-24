using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<Skill> ActiveSkills = new List<Skill>(4);
    [HideInInspector]
    public List<Skill> AllSkills;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) &&
            Player.Aim.IsAimed)
        {
            ActiveSkills[0].UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ActiveSkills[1].UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ActiveSkills[2].UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ActiveSkills[3].UseSkill();
        }
    }
}
