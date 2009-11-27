#ifndef Game_Controller_H_
#define Game_Controller_H_

#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "Core.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Game.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"
#include "IEvent.h"


class GameController : public Core
{
private:

public:	
	GameController::GameController( Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr);
	bool GameController::DoControll( float a_timeSinceLastFrame);
	void GameController::UpdateCamera();
	GameController::~GameController();
};
#endif
