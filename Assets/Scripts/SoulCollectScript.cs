using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollectScript : MonoBehaviour
{
    public float delay = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
