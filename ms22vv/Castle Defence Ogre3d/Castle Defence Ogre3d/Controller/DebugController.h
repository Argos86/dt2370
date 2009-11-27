#ifndef Debug_Controller_H_
#define Debug_Controller_H_

#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "Core.h"
#include "..\Model\Player.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Game.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"

class DebugController : public Core
{
private:

public:	
	DebugController::DebugController( Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr );
	bool DebugController::DoControll( float a_timeSinceLastFrame);
	void DebugController::UpdateCamera();
	DebugController::~DebugController();
};
#endif
