using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBackAniFuc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnEnd3()
    {
        GameRoot.GetInstance().GenerateUIPage(GameRoot.GetInstance().end3);
        Destroy(gameObject);
    }
}
