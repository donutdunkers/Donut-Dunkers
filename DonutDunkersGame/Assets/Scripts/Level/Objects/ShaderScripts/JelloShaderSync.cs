using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloShaderSync : MonoBehaviour
{
    Material[] materials;

    Vector3 ballWiggleDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        materials = GetComponent<Renderer>().materials;
        float WiggleInt = materials[0].GetFloat("_WiggleIntensity");
        float WiggleSpeed = materials[0].GetFloat("_WiggleSpeed");
        Vector3 WiggleDir = materials[0].GetVector("_WiggleDir");

        materials[1].SetFloat("_WiggleIntensity", WiggleInt);
        materials[1].SetFloat("_WiggleSpeed", WiggleSpeed);
      

       
        UpdateWiggleDir();
        materials[0].SetVector("_WiggleDir", ballWiggleDir);
        materials[1].SetVector("_WiggleDir", ballWiggleDir);

        

        

    }
    // UpdateWiggleDir will update the wiggle direction variable in the shader depending on the position of the DonutHole (Ball)
    void UpdateWiggleDir(){
        GameObject donutHole = GameObject.Find("Ball Controller");
        if (!donutHole){
            Debug.Log("DonutHole Object Not Found!");
            return;
        }
        Vector3 wiggleDir = Vector3.Normalize (donutHole.transform.position - this.transform.position); 
        if (wiggleDir.magnitude < 0.02){
            return;
        }
        //Debug.Log(wiggleDir);
        ballWiggleDir =  wiggleDir;
    }
}
