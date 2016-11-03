using UnityEngine;
using System.Collections;
using Leap.Unity;

public class GrabObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("index --> " + _indexColliding);
        //Debug.Log("thumb --> " + _thumbColliding);
    }

    private bool _indexColliding = false;
    private bool _thumbColliding = false;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        // keep track of the colliding objects
        if (other.gameObject.tag == "index")
            _indexColliding = true;
        else if (other.gameObject.tag == "thumb")
            _thumbColliding = true;

        if (_indexColliding && _thumbColliding)
        {
            Debug.Log("index thumb colliding");
            gameObject.transform.Translate(other.transform.position);
        }
    }

    void OnCollisionExit(Collision other)
    {
        // reset the booleans if the objects are no longer colliding
        if (other.gameObject.tag == "index")
            _indexColliding = false;
        else if (other.gameObject.tag == "thumb")
            _thumbColliding = false;
    }

    private HandModel GetHand(Collider other)
    {
        if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
            return other.transform.parent.parent.GetComponent<HandModel>();
        else
            return null;
    }

}
