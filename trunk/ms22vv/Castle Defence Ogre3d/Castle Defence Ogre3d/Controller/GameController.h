#ifndef Game_Controller_H_
#define Game_Controller_H_

#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Level\Level.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"
#include "IEvent.h"


class GameController
{
private:
	Player *m_player;
	CameraManager *m_activeCamera;
	GameSettings *m_gameSettings;
	GameView *m_gameView;
	Level *m_level;
	float m_time;

public:	
	GameController::GameController( Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Level *a_level );
	bool GameController::DoControll(Ogre::SceneManager *a_scenemgr, float a_timeSinceLastFrame);
	void GameController::UpdateCamera();
	GameController::~GameController();
};
#endif
