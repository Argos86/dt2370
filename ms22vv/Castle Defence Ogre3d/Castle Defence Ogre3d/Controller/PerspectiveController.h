#ifndef Perspective_Controller_H_
#define Perspective_Controller_H_

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
#include "..\View\Effects\MouseAnimation.h"
#include "IEvent.h"


class PerspectiveController : public Core
{
private:
	MouseAnimation *m_mouseAnim;

public:	
	PerspectiveController::PerspectiveController( Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr);
	bool PerspectiveController::DoControll( float a_timeSinceLastFrame);
	void PerspectiveController::UpdateCamera();
	PerspectiveController::~PerspectiveController();
};
#endif
