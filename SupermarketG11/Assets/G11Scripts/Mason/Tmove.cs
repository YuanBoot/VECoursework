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
        print("  " + other.gameObject.name + "    碰撞了    " + gameObject.name);
        if (other.gameObject.GetComponent<ProductCode>())
        {
            print(" it is a food, we can still move!!! ");
        }
        else
        {
            //player 碰撞也要去掉
            touch = 1;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        print("  " + other.gameObject.name + "  离开了  " + gameObject.name);
        touch = 0;
        Debug.Log("现在的t状态应该可以动 " + touch);
        debug = 0;
    }

    void OnCollisionEnter(Collision other)
    {
       // print("  " + other.gameObject.name + " 非triger/有效果碰撞      " + gameObject.name);
    }

    void OnCollisionExit(Collision other)
    {
      //  print("  " + other.gameObject.name + "   有效果碰撞后离开  " + gameObject.name);
    }
}
