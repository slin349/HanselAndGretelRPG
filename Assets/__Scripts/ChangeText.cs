using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    public TextMesh text;
    public bool currentWeapon;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            if(currentWeapon){
                text.text = "Weapon In Pack";
                currentWeapon = false;
            }
            else{
                text.text = "Current Weapon";
                currentWeapon = true;
            }
        }
    }
    
}
