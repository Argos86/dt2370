#ifndef Controller_Core_H_
#define Controller_Core_H_

#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Game.h"
#include "..\View\GameView.h"
#include "..\View\HudManager.h"
#include "..\View\Camera\Camera.h"
#include "IEvent.h"


class Core
{
protected:
	Ogre::SceneManager *m_scenemgr;
	Player *m_player;
	CameraManager *m_activeCamera;
	GameSettings *m_gameSettings;
	GameView *m_gameView;
	Game *m_game;
	HudManager *m_hudMgr;
	float m_time;


public:	
	Core::Core( Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr);
	virtual bool Core::DoControll( float a_timeSinceLastFrame);
	virtual void Core::UpdateCamera();
	Core::~Core();
};
#endif
