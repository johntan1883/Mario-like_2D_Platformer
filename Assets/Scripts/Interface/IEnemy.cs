using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    void MoveTowardPoint();
    void SwitchPoint();
    void Flip();
}
