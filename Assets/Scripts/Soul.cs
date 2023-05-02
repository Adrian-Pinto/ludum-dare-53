using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul
{
    public enum State
    {
        WANDERING,
        PLAYER,
        POT
    }

    public float score = 1000;
    public float decayRate = 1;
    public State currentState = State.WANDERING;

    public Soul()
    {
        score = 1000.0f;
        decayRate = 10.0f;
        currentState = State.WANDERING;
    }

    public Soul(Soul soul)
    {
        score = soul.score;
        decayRate = soul.decayRate;
        currentState = soul.currentState;
    }

    public void DecaySoul()
    {
        if (score < 1)
        {
            score = 0;
            return;
        }

        switch (currentState)
        {
            case State.WANDERING:
                score = score - decayRate * Time.deltaTime;
                break;
            case State.PLAYER:
                score = score - (decayRate * 0.5f) * Time.deltaTime;
                break;
            case State.POT:
                
                break;
        }
    }
}
