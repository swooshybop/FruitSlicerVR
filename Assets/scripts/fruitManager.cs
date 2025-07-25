using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1f)
        {
            Destroy(this.gameObject);
        }
    }
}
