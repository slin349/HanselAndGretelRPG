using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacter : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public GameObject parent;
    public GameObject gameObject;
    private int distance = 3;
    private void Awake()
    {
        
        if(ChangeText.currentWeapon == true){
            gameObject = Instantiate(character1, parent.transform.forward*distance+parent.transform.position  ,Quaternion.Euler(0f,0f,0f) );
            gameObject.transform.parent = parent.transform;
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        else{
            gameObject = Instantiate(character2, parent.transform.forward*distance+parent.transform.position  ,Quaternion.Euler(0f,0f,0f) );
            gameObject.transform.parent = parent.transform;
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
            
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
