using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager brain;
    protected virtual void Awake()
    {
        brain = GetComponent<GameManager>();
    }

    public virtual void Construct() { }
    public virtual void Destruct() { }
    public virtual void UpdateState() { }

}
