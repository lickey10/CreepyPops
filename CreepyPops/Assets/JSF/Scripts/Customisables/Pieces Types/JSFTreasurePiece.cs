﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSFTreasurePiece : JSFPieceDefinition {

	JSFWinningConditions wc {get {return JSFUtils.wc;}}

	public override JSFPieceDefinition chanceToSpawnThis (int x, int y)
	{
		if(wc.canSpawnTreasure() ){
			return this;
		}
		return null;
	}

	public override void onPieceCreated (JSFGamePiece gp)
	{
		wc.treasureList.Add(gp);
	}

	public override void onPieceDestroyed (JSFGamePiece gp)
	{
		// play audio visuals
        if(gm.audioScript.treasureCollectedFx.Length > 0)
		    gm.audioScript.treasureCollectedFx[Random.Range(0,gm.audioScript.treasureCollectedFx.Length-1)].play(); // play this sound fx

		gm.animScript.doAnim(JSFanimType.TREASURECOLLECTED, gp.master.arrayRef[0], gp.master.arrayRef[1]); // instantiate this anim
	}
}
