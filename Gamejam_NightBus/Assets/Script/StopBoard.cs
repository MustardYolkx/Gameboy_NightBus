using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBoard : MonoBehaviour
{

    public Sprite normal;
    public Sprite bad;

    public SpriteRenderer render;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if (GameRoot.GetInstance().playerHP < 3)
        {
            render.sprite = bad;
        }
        else
        {
            render.sprite = normal;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
