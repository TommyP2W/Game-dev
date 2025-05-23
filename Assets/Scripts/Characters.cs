using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Characters
{

    int currentHealth { get; set; }
    int maxHealth { get; set; }
    bool chasing { get; set; }
    bool isWalking { get; set; }
    bool attackAction { get; set; }

    int armour_class { get; set; }
    GameObject requestedEnemy {  get; set; }
    Attacksvulnerablities.attackTypes vulnerability { get; set; }
    Attacksvulnerablities.attackTypes attackType { get; set; }
    // Start is called before the first frame update

    void attack();

    void death();

    void actionSelector();
    public void update()
    {
        if (currentHealth == 0)
        {
            death();
        }
    }

}
