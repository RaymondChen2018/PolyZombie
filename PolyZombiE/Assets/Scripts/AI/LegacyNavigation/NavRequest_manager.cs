using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class NavRequest_manager : MonoBehaviour {

    public Queue<NavRequest> pathRequestQueue = new Queue<NavRequest>();
    public int count;
    //PathRequest currentPathRequest;
    private void Update()
    {
        count = pathRequestQueue.Count;
    }
    public void RequestPath(NavRequest navRequest)//, GameObject player, float z_size, float p_size)
    {   
        if (!pathRequestQueue.Contains(navRequest))
        {
            pathRequestQueue.Enqueue(navRequest);
        }
    }
}
