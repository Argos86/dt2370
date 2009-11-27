#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "Core.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Game.h"
#include "..\View\GameView.h"
#include "..\View\HudManager.h"
#include "..\View\Camera\Camera.h"

#include "IEvent.h"


Core::Core(Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr)
{
	m_scenemgr = a_scenemgr;
	m_player = a_player;
	m_gameSettings = a_gameSettings;
	m_activeCamera = a_camera;
	m_gameView = a_gameView;
	m_game = a_game;
	m_hudMgr = a_hudMgr;
	m_time = 0;	
}

bool Core::DoControll( float a_timeSinceLastFrame)
{
	return true;
}

void Core::UpdateCamera()
{
	m_activeCamera = m_gameSettings->GetActiveCamera();
	m_activeCamera->Update( m_player->GetPosition(),m_player->GetWeaponOrientation(), Ogre::Vector2::ZERO);
}

Core::~Core()
{

}
