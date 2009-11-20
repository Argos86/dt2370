#ifndef Debug_Controller_H_
#define Debug_Controller_H_

#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "..\Model\Player.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Level\Level.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"

class DebugController
{
private:
	Player *m_player ;
	CameraManager *m_activeCamera;
	GameSettings *m_gameSettings;
	GameView *m_gameView;
	Level *m_level;
	float m_time;

public:	
	DebugController::DebugController( Player *a_player,  GameSettings *a_gameSettings, GameView *a_gameView, Level *a_level );
	bool DebugController::DoControll(Ogre::SceneManager *a_scenemgr, float a_timeSinceLastFrame);
	void DebugController::UpdateCamera();
	DebugController::~DebugController();
};
#endif
