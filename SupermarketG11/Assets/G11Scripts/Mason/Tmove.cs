using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tmove : MonoBehaviour {

    // Use this for initialization
    public int touch;
    public int debug;
	void Start () {
        touch = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        //print("  " + other.gameObject.name + "    碰撞了    " + gameObject.name);
        if (other.gameObject.GetComponent<ProductCode>())
        {
            print(" it is a food.");
            other.gameObject.transform.parent = this.transform;
        }
        else
        {
            //player 碰撞也要去掉
            touch = 1;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        //print("  " + other.gameObject.name + "  离开了  " + gameObject.name);
        touch = 0;
        //other.gameObject.transform.parent = null;
    }

    void OnCollisionEnter(Collision other)
    {
      
        print("  " + other.gameObject.name + "    碰撞了    " + gameObject.name);
        if (other.gameObject.GetComponent<ProductCode>())
        {
            print(" it is a food.");
            other.gameObject.transform.parent = this.transform;
        }

    }

    void OnCollisionExit(Collision other)
    {
     
        print("  " + other.gameObject.name + "  离开了  " + gameObject.name);
        touch = 0;
        other.gameObject.transform.parent =null;
    }
}
