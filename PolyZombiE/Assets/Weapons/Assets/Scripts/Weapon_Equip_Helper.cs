using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon_Equip_Helper : MonoBehaviour {
    private Abstract_Identity user;
    [SerializeField] private UnityEvent OnEquiped = new UnityEvent();
    [SerializeField] private UnityEvent OnDequiped = new UnityEvent();

    bool prevEquiped = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (user != null && !prevEquiped)
        {
            OnEquiped.Invoke();
            prevEquiped = true;
        }
        else if(user == null && prevEquiped)
        {
            OnDequiped.Invoke();
            prevEquiped = false;
            user = null;
        }
    }

    public void setUser(Abstract_Identity newUser)
    {
        user = newUser;
    }

    public Abstract_Identity getUser()
    {
        return user;
    }

    
}
