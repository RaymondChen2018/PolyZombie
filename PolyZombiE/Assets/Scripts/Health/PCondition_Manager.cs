using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class PCondition_Manager : MonoBehaviour {
    [SerializeField] private List<Human_Condition> humanAlive = new List<Human_Condition>();
    [SerializeField] private List<Zombie_Condition> zomebieAlive = new List<Zombie_Condition>();

    [SerializeField] private UnityEvent OnHumanDies;
    [SerializeField] private UnityEvent OnZombieDies;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // Check humans condition
        for (int i = 0; i < humanAlive.Count; i++)
        {
            Human_Condition subject = humanAlive[i];

            // Check on subject
            if(subject.GetConditionStatus() == Physical_Condition.STATUS.Dead)
            {
                humanAlive.Remove(subject);
                OnHumanDies.Invoke();
            }
        }
        // Check zombies condition
        for (int i = 0; i < zomebieAlive.Count; i++)
        {
            Zombie_Condition subject = zomebieAlive[i];

            // Check on subject
            if (subject.GetConditionStatus() == Physical_Condition.STATUS.Dead)
            {
                zomebieAlive.Remove(subject);
                OnZombieDies.Invoke();
            }
        }
    }

    //void privIsInfected(Team_Attribute victim)
    //{
    //    // Alter attribute to zombie
    //    victim.SetTeam(Team.Zombie);
    //}

    //void privIsKilled(Team_Attribute victim)
    //{
    //    //Team_Manager.oneLessHuman(victim);
    //}

}
