﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary> ##################################
/// 
/// NOTICE :
/// This script is for the audio fx and bgm~!
/// You can add more Audioclips as well as modify the enums as you see fit.
/// 
/// Note that this is just where all the clips are stored. There are various places in other scripts
/// that will reference this script to play the appropriate audio using the defined enum.
/// 
/// </summary> ##################################

public class JSFAudioPlayer : MonoBehaviour {
	
	// the audio source is a gameObject so that you can move it around for various effect.
	// the default listener is attached to the "AudioSource" GameObject.
	public AudioSource player;
	public AudioSource bgmPlayer;

	public bool enableMusic = true;
	public AudioClip[] BackgroundMusic;
	
	public bool enableSoundFX = true;
	public customAudio[] gameOverSoundFx;
	public customAudio[] switchSoundFx;
	public customAudio[] DropSoundFx;
	public customAudio[] matchSoundFx;
	public customAudio[] specialMatchSoundFx;
	public customAudio[] badMoveSoundFx;
	public customAudio arrowSoundFx;
	public customAudio bombSoundFx;
	public customAudio[] matchFiveSoundFx;
	public customAudio[] matchSixSoundFx;
	public customAudio rockPanelHitFx;
	public customAudio shadedPanelHitFx;
	public customAudio icePanelHitFx;
	public customAudio lockedPanelHitFx;
	public customAudio[] convertingSpecialFx; 
	public customAudio[] treasureCollectedFx;
	public customAudio[] treasureLostFx;

	public customAudio[] comboLowFx;
	public customAudio[] comboMidFx;
	public customAudio[] comboHighFx;

    public Slider MusicVolumeSlider;
    public Slider FXVolumeSlider;

    // created a custom class to store a bool as a reference,
    // and to simulate a cooldown function with "x" seconds.
    [System.Serializable]
	public class customAudio{
		public AudioClip audioClip;
		bool canPlay = true;

		public void play(){
			if(audioClip != null && canPlay && JSFUtils.gm.audioScript.enableSoundFX){
				JSFUtils.gm.audioScript.player.PlayOneShot(audioClip);
				JSFUtils.gm.audioScript.StartCoroutine(coolDown(0.1f)); // built-in spam filter
			}
		}

		// causes a delayed state transition
		IEnumerator coolDown(float timer){
			canPlay = !canPlay; // reverse the state
			yield return new WaitForSeconds(timer);
			canPlay = !canPlay; // back to original
		}
	}
	
	// function to play the bgm if enabled
	void loadBGM(){
		if(bgmPlayer == null || BackgroundMusic.Length == 0){
			return; // null player!
		}
		bgmPlayer.GetComponent<AudioSource>().clip = BackgroundMusic[Random.Range(0,BackgroundMusic.Length)]; // set the clip
		if(enableMusic && bgmPlayer.GetComponent<AudioSource>().clip != null){ // if music is enabled
			bgmPlayer.GetComponent<AudioSource>().Play(); // play
		}
	}

	IEnumerator playNextBGM(){
		while(true){
			if(enableMusic && !bgmPlayer.GetComponent<AudioSource>().isPlaying){
				loadBGM();

                MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
                FXVolumeSlider.value = PlayerPrefs.GetFloat("FXVolume", 1);
            }
			yield return new WaitForSeconds(2f);
		}
	}
	
	// function to toggle the bgm on/off
	public void toggleBGM(){
		if(bgmPlayer.GetComponent<AudioSource>().clip != null && bgmPlayer.GetComponent<AudioSource>().isPlaying){ // if music is playing
			bgmPlayer.GetComponent<AudioSource>().Pause(); // pause the music
			enableMusic = false;
		} else {
            bgmPlayer.clip = BackgroundMusic[Random.Range(0, BackgroundMusic.Length)];

            bgmPlayer.GetComponent<AudioSource>().Play(); // play
			enableMusic = true;
		}

        PlayerPrefs.SetString("enableMusic", enableMusic.ToString());
    }
	
	// function to toggle the FX on/off
	public void toggleFX(){
		enableSoundFX = !enableSoundFX;

        PlayerPrefs.SetString("enableSoundFX", enableSoundFX.ToString());
    }

    public void PlayMatchSound(int numberOfSounds)
    {
        StartCoroutine(playMatchSounds(numberOfSounds));
    }

    IEnumerator playMatchSounds(int numberOfSounds)
    {
        for (int x = 0; x < numberOfSounds; x++)
        {
            JSFAudioPlayer.customAudio clip = matchSoundFx[Random.Range(0, matchSoundFx.Length - 1)];
            clip.play();

            //yield return new WaitForSeconds(clip.audioClip.length);
            yield return new WaitForSeconds(.1f);
        }
    }

    void Awake(){
		if(player == null){ // try and get it manually if player forgot to assign an AudioSource
			player = GameObject.Find("AudioSource").GetComponent<AudioSource>();
		}
		if(bgmPlayer == null && player != null){
			bgmPlayer = player;
		}

		if(player == null){
			Debug.LogError("audio source reference is null. Fix the AudioPlayer script reference !");
		}
		if(bgmPlayer == null){
			Debug.LogError("bgm audio source reference is null. Fix the AudioPlayer script reference !");
		}
        
        StartCoroutine(playNextBGM() );
	}

    public void OnFXValueChanged()
    {
        player.volume = FXVolumeSlider.value;

        PlayerPrefs.SetFloat("FXVolume", FXVolumeSlider.value);
    }

    public void OnMusicValueChanged()
    {
        bgmPlayer.volume = MusicVolumeSlider.value;

        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
    }
}
