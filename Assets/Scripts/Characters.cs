using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Characters 
{
    int currentHealth { get; set; }
    int maxHealth { get; set; }
    bool chasing { get; set; }
    bool isWalking { get; set; }
    // Start is called before the first frame update

    void attack();

    void death();

    public void update()
    {
        if (currentHealth == 0)
        {
            death();
        }
    }

}
