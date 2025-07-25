using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSlicer : MonoBehaviour
{
    [SerializeField] private GameObject leftHalfPrefab;
    [SerializeField] private GameObject righttHalfPrefab;
    [SerializeField] private float addImpulse = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Knife"))
        {
            return;
        }
        else
        {
            Slicer(collision);
            fruitSpawner.Instance.scoreGame(10);
        }
    }

    private void Slicer(Collision collision)
    {
        //save the fruit's current state
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Rigidbody originalRB = GetComponent<Rigidbody>();

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
