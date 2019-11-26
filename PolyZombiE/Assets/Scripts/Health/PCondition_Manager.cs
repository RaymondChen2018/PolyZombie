using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PCondition_Manager : MonoBehaviour {
    private Human_Condition[] allHumanCondition;
    private Zombie_Condition[] allZombieCondition;
    // Use this for initialization
    void Start () {
        allHumanCondition = FindObjectsOfType<Human_Condition>();
        allZombieCondition = FindObjectsOfType<Zombie_Condition>();
    }
	
	// Update is called once per frame
	void Update () {
        // Check humans condition
        for (int i = 0; i < allHumanCondition.Length; i++)
        {
            Human_Condition subject = allHumanCondition[i];

            // Check on subject

        }
        // Check zombies condition
        for (int i = 0; i < allZombieCondition.Length; i++)
        {
            Zombie_Condition subject = allZombieCondition[i];

            // Check on subject

        }
    }

    void privInfect(Team_Attribute victim)
    {
        // Alter attribute to zombie
        victim.SetTeam(Team.Zombie);

        Team_Manager.oneLessHuman(victim);
    }

    void privKill(Team_Attribute victim)
    {
        Team_Manager.oneLessHuman(victim);
    }
}
