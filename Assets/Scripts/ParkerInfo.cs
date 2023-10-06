using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkerInfo : MonoBehaviour
{
    public Car yellowCar;
    public Car redCar;
    public SpriteAlphaChanger changerFail;
    public SpriteAlphaChanger changerFinalLogo;
    public SpriteAlphaChanger changerFinalButton;
    public SpriteAlphaChanger changerBGButton;

    private YieldInstruction fadeInstruction = new YieldInstruction();

    [SerializeField]
    int IdleTimeSetting = 20;

    float LastIdleTime;
    bool failBool;

    void Awake() {

        LastIdleTime = Time.time;
    }
    
    private void Update() 
    {
        if(Input.anyKey){
            LastIdleTime = Time.time;
        }

        if(yellowCar.conect == true && redCar.conect == true)
        {
            yellowCar.startMovement = true;
            redCar.startMovement = true;
        }

        if(yellowCar.isHitCar == true || redCar.isHitCar == true)
        {
            yellowCar.isHitCarStop = yellowCar.isHitCar;
            redCar.isHitCarStop = redCar.isHitCar;

            if(!failBool)
                changerFail.UpdateAlpha(true);
            
            if(changerFail._sprite.color.a == 1)
            {
                Debug.Log("Fail is 1");
                failBool = true;
                if(failBool)
                {
                    changerFail.UpdateAlpha(false);
                    yellowCar.isHitCarStop = false;
                    redCar.isHitCarStop = false;
                }
            }
            else{
                failBool = false;
            }

            if(IdleCheck())
            {
                changerFail.UpdateAlpha(false);
            }
        }

        if(IdleCheck())
        {
            changerFinalLogo.UpdateAlpha(true);
            changerFinalButton.UpdateAlpha(true);
            changerBGButton.UpdateAlpha(true);
        }
    }
    
    public bool IdleCheck(){
            return Time.time - LastIdleTime > IdleTimeSetting;
    }

    public void OpenSite()
    {
        Application.OpenURL("https://roasup.com");
    }

    IEnumerator FadeOut(Image image, float fadeTime)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime ;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }

    IEnumerator FadeIn(Image image, float fadeTime)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime ;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }
}
