using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruitSlicer : MonoBehaviour
{
    [SerializeField] private GameObject leftHalfPrefab;
    [SerializeField] private GameObject righttHalfPrefab;
    [SerializeField] private float addImpulse = 5f;

    //to make the fruits "squish" on slice
    float time = 0.15f; //bounce dura.
    int vibr = 10; //no. of jiggles in dura.

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Knife"))
        {
            return;
        }
        else
        {
            Slicer(collision);
            fruitSpawner.Instance.scoreGame(10, transform.position);
        }
    }

    private void Slicer(Collision collision)
    {


        float factor = 0.6f; //because all fruits are widely differnt in scale
        Vector3 baseScale = transform.localScale;
        Vector3 punchVec = baseScale * factor;


        //save the fruit's current state
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Rigidbody originalRB = GetComponent<Rigidbody>();

        AudioManager.Ins.PlaySlice(pos);

        //spawn the sliced halves
        GameObject left = Instantiate(leftHalfPrefab, pos, rot);
        GameObject right = Instantiate(righttHalfPrefab, pos, rot);


        //Add the velocity of the original fruit (unsliced to sliced)
        if(originalRB != null)
        {
            Vector3 vel = originalRB.velocity;
            left.GetComponent<Rigidbody>().velocity = vel;
            right.GetComponent<Rigidbody>().velocity = vel;
        }

        //After slice, PUSH away the halves in opposite directions of the contact with knife
        Vector3 norm = collision.contacts[0].normal;
        left.GetComponent<Rigidbody>().AddForce(-norm * addImpulse, ForceMode.Impulse);
        right.GetComponent<Rigidbody>().AddForce(norm * addImpulse, ForceMode.Impulse);

        //squish effect
        left.transform.DOPunchScale(punchVec, time, vibr, 1f);
        right.transform.DOPunchScale(punchVec, time, vibr, 1f);


        Destroy(gameObject);//get rid of og fruit (unsliced)
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
