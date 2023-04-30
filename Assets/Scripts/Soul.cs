using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul
{
    public enum State
    {
        WANDERING = 0,
        PLAYER,
        POT
    }

    public float score = 1000;
    public float decayRate = 1;

    public State currentState = State.WANDERING;
    public Soul()
    {
        this.score = 1000.0f;
        this.decayRate = 10.0f;
        this.currentState = State.WANDERING;
    }
    public Soul(Soul soul)
    {
        this.score = soul.score;
        this.decayRate = soul.decayRate;
        this.currentState = soul.currentState;
    }
    public void DecaySoul()
    {
        switch (currentState)
        {
            case Soul.State.WANDERING:
                score = score - decayRate * Time.deltaTime;
                Debug.Log("Decaying from wandering" + score);
                break;
            case State.PLAYER:
                score = score - (decayRate * 0.5f) * Time.deltaTime;
                Debug.Log("Decaying from player" + score);
                break;
            case State.POT:
                
                Debug.Log("Decaying from Pot, not decaying");
                break;
        }
    }
}
