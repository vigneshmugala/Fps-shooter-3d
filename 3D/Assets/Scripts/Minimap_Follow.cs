using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Follow : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
       
    }
}
