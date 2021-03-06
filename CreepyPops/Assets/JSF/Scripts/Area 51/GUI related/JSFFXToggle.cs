﻿using UnityEngine;
using System.Collections;

/// <summary> ################################
/// 
/// NOTICE :
/// This script is the control script for the music and sound.
/// This script will set the properties based on feedback from FXTracker and GameManager.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ################################

// public enums to be used by FXTracker script
public enum JSFsoundButtonType{FX_ON, FX_OFF, MUSIC_ON, MUSIC_OFF};

public class JSFFXToggle : MonoBehaviour {
	
	GameObject FxOn;
	GameObject FxOff;
	GameObject MusicOn;
	GameObject MusicOff;
	
	JSFAudioPlayer ap;
	bool isScriptBroken = false;
	
	// called by FXTracker script located on the individual toggle buttons
	public void slaveClick(JSFsoundButtonType IamA){
		if(!isScriptBroken){
			switch(IamA){
			case JSFsoundButtonType.FX_OFF :
                TurnFXOn();
                break;
			case JSFsoundButtonType.FX_ON :
                TurnFXOff();
                break;
			case JSFsoundButtonType.MUSIC_OFF :
                TurnBGMusicOn();
				break;
			case JSFsoundButtonType.MUSIC_ON :
                TurnBGMusicOff();
				break;
			}
		}
	}

    public void TurnFXOn()
    {
        FxOn.SetActive(true);
        FxOff.SetActive(false);
        ap.toggleFX(); // set the fx on
    }

    public void TurnFXOff()
    {
        FxOn.SetActive(false);
        FxOff.SetActive(true);
        ap.toggleFX(); // set the fx off
    }

    public void TurnBGMusicOn()
    {
        MusicOn.SetActive(true);
        MusicOff.SetActive(false);
        ap.toggleBGM(); // toggle the bgm on/off (defined in AudioPlayer.cs)
    }

    public void TurnBGMusicOff()
    {
        MusicOn.SetActive(false);
        MusicOff.SetActive(true);
        ap.toggleBGM(); // toggle the bgm on/off (defined in AudioPlayer.cs)
    }
	
	void initMe(){
		// if you renamed game objects, revise the changes below...
		ap = JSFUtils.gm.audioScript;
		FxOn = GameObject.Find("FX Button on");
		FxOff = GameObject.Find("FX Button off");
		MusicOn = GameObject.Find("Music Button on");
		MusicOff = GameObject.Find("Music Button off");
		
		// warning msgs for game producers
		if(ap == null ){
			Debug.Log("Cannot find game manager script! revise the FXtoggle script!");
			isScriptBroken = true;
		}		
		if(FxOn == null || FxOff == null || MusicOn == null || MusicOff == null ){
			Debug.Log("You changed the \"Sound Button\" game object! revise the FXtoggle script!");
			isScriptBroken = true;
		}
		
		setDefaultOptions();
	}
	
	void setDefaultOptions(){
		if(!isScriptBroken){
            ap.enableSoundFX = bool.Parse(PlayerPrefs.GetString("enableSoundFX", "true"));
            ap.enableMusic = bool.Parse(PlayerPrefs.GetString("enableMusic", "true"));

            if (ap.enableSoundFX){  // default fx is on
				FxOn.SetActive(true);
				FxOff.SetActive(false);
			} else { // default fx is off
				FxOn.SetActive(false);
				FxOff.SetActive(true);
			}
			
			if(ap.BackgroundMusic != null){ // bgm provided, show icon
				if(ap.enableMusic){ // default is on
					MusicOn.SetActive(true);
					MusicOff.SetActive(false);
				} else { // default is off
					MusicOn.SetActive(false);
					MusicOff.SetActive(true);
				}
			} else { // no bgm provided... don't show icon
				MusicOn.SetActive(false);
				MusicOff.SetActive(false);
			}
		}
	}
	
	void Awake(){
		initMe();
	}
}
