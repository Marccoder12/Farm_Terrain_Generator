using UnityEngine;

public class LightDirectionSetter : MonoBehaviour{
    public bool ShowLightVec;
    public Light light;
    public Material toonLightMat;

    void Update()
    {
        SetLightDir();
    }
    
    public void SetLightDir()
    {
        if(RenderSettings.sun != null){
            Vector3 lightDir = -light.transform.forward;// Light direction * towards* surface
            toonLightMat.SetVector("_MainLightDirection", lightDir);
                Shader.SetGlobalVector("_MainLightDirection",lightDir);
            Debug.Log(lightDir);
        }
    }
}