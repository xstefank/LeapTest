using UnityEngine;
using System.Collections;
using Leap.Unity;
using Leap;

public class GrabObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("index --> " + _indexColliding);
        //Debug.Log("thumb --> " + _thumbColliding);
        if (isGrabbed) {
            HandModel handModel = followObject.GetComponent<HandModel>();
            Vector3 pos = handModel.GetPalmPosition().ToVector().ToVector3();
            Debug.Log("is grabbed by " + handModel.name);
            Debug.Log( "position -> "  + pos.ToString());
            gameObject.transform.position = pos;
        }
    }

    private bool _indexColliding = false;
    private bool _thumbColliding = false;
    private bool isGrabbed = false;
    private GameObject followObject = null;
    private Vector3 offset = Vector3.zero;

    void OnCollisionEnter(Collision other)
    {
        if (!other.transform.parent) return;
        // keep track of the colliding objects
        if (other.transform.parent.name == "index")
            _indexColliding = true;
        else if (other.transform.parent.name == "thumb")
            _thumbColliding = true;

        if (_indexColliding && _thumbColliding)
        {
            //Debug.Log("index thumb colliding");
            isGrabbed = true;
            followObject = other.transform.parent.parent.gameObject;
            //gameObject.transform.Translate(other.transform.position);
        }
    }

    void OnCollisionExit(Collision other)
    {
        // reset the booleans if the objects are no longer colliding
        if (!other.transform.parent) return;
        if (other.transform.parent.name == "index")
            _indexColliding = false;
        else if (other.transform.parent.name == "thumb")
            _thumbColliding = false;

        if (!_indexColliding || !_thumbColliding)
        {
            isGrabbed = false;
            followObject = null;
            offset = Vector3.zero;
        }
    }

    private HandModel GetHand(Collider other)
    {
        if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
            return other.transform.parent.parent.GetComponent<HandModel>();
        else
            return null;
    }

}
