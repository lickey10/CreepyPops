﻿using UnityEngine;
using System.Collections;

public class JSFShadedPanel : JSFPanelDefinition {


	// function to check if pieces can fall into this board box
	public override bool allowsGravity(JSFBoardPanel bp){
		return true;
	}
	
	// function to check if pieces can re-appear on this board box
	public override bool allowsAppearReplacement (JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
	}
	
	// if the piece here can be added to the swipe chain
	public override bool isSwippable(JSFBoardPanel bp){
		// your logic here ( if needed )
		return true;
	}
	
	// if the piece here (if any) can be destroyed
	public override bool isDestructible(JSFBoardPanel bp){
		return true;
	}
	
	// function to check if pieces can be stolen from this box by gravity
	public override bool isStealable(JSFBoardPanel bp){
		return true;
	}
	
	// function to check if this board needs to be filled by gravity
	public override bool isFillable(JSFBoardPanel bp){
		return true;
	}
	
	// function to check if this board is a solid panel
	// ( AKA piece, NO ENTRY!! ROADBLOCK~!- IMPORTANT, not the same of allowsGravity()~!
	// this function determines if pieces will landslide it's neighbouring piece to fill bottom blocks)
	public override bool isSolid(JSFBoardPanel bp){
		return false;
	}

	// function to play the audio visuals of this panel
	public override void playAudioVisuals(JSFBoardPanel bp){
		bp.master.gm.audioScript.shadedPanelHitFx.play();
		bp.master.gm.animScript.doAnim(JSFanimType.SHADEHIT, bp.master.arrayRef[0], bp.master.arrayRef[1] );
	}
}
