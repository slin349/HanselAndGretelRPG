using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public GameObject ThirdCam;
    public GameObject FirstCam;
    public static int CamMode;
    public GameObject Player;
    public Vector3 euler;
    // Update is called once per frame
    void Start(){
        if(CamMode == 0 ){
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
        }
        if(CamMode == 1){
            ThirdCam.SetActive(false);
            FirstCam.SetActive(true);
        }
    }
    void Update()
    {
        if(Input.GetButtonDown("Camera")){
            
            if(CamMode == 1){
                CamMode = 0;
            }
            else{
                CamMode += 1;
            }
            euler = Player.transform.rotation.eulerAngles;
            StartCoroutine (CamChange());
            Player.transform.rotation = Quaternion.Euler(new Vector3(0, euler.y,0));
        }
        
    }
   
    IEnumerator CamChange(){
        yield return new WaitForSeconds(0.01f);
        if(CamMode == 0 ){
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
            
        }
        if(CamMode == 1){
            ThirdCam.SetActive(false);
            FirstCam.SetActive(true);
            
        }
    }
}
