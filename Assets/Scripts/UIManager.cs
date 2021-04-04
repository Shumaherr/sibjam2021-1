using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI oxygenText;
    public TextMeshProUGUI energyText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.playerInfo.OnEnergyChange += ChangeEnergy;
        GameManager.Instance.playerInfo.OnOxygenChange += ChangeOxygen;

    }

    private void ChangeEnergy(float value)
    {
        energyText.text = "Energy: " + ((int) (value)).ToString();
    }


    private void ChangeOxygen(float value)
    {
        oxygenText.text = "Oxygen: " + ((int) (value)).ToString();
    }
    
}
