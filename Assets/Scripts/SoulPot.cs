using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulPot : MonoBehaviour
{ 
    public List<Soul> soulsStored;

    private void Start()
    {
        soulsStored = new List<Soul>();
    }

    public void DepositSouls(List<Soul> souls)
    {
        soulsStored.Clear();
        soulsStored = new List<Soul>(souls);
        souls.Clear();
    }

    public int RetrieveSouls(List<Soul> soulsToFill)
    {
        soulsToFill = new List<Soul>(soulsStored);
        soulsStored.Clear();
        return soulsStored.Count;
    }
}
