using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    public TextMesh text1;
    public TextMesh text2;
    public static bool currentWeapon;
    // Update is called once per frame
    void Start(){
        if(currentWeapon){
            text1.text = "Weapon In Pack";
            text2.text = "Current Weapon";
        }
        else{
            text1.text = "Current Weapon";
            text2.text = "Weapon In Pack";
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            if(currentWeapon){
                text1.text = "Current Weapon";
                text2.text = "Weapon In Pack";
                currentWeapon = false;
            }
            else{
                text1.text = "Weapon In Pack";
                text2.text = "Current Weapon";
                currentWeapon = true;
            }
        }
    }
    
}
