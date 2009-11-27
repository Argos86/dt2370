#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "Core.h"
#include "DebugController.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Game.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"

DebugController::DebugController(Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr)
	: Core(a_player, a_gameSettings, a_gameView, a_game, a_camera, a_scenemgr, a_hudMgr)
{
	m_activeCamera = m_gameSettings->GetActiveCamera();
}

bool DebugController::DoControll( float a_timeSinceLastFrame)
{	
	Ogre::Vector3 transVector = Ogre::Vector3::ZERO;

	m_gameView->UpdateInput();
	OIS::Keyboard *keyEvent = m_gameView->GetKeyEvent();

	m_time += a_timeSinceLastFrame;

	if (keyEvent != NULL) {
		if (m_time > 1000 && keyEvent->isKeyDown( OIS::KC_F1 )) {
			m_activeCamera->ResetOrientation();
			m_player->ResetOrientation();
			m_time = 0;
			m_gameSettings->SetGameState(GameSettings::GAME_STATE_RUNNING);
			return false;
		}
		if ( keyEvent->isKeyDown( OIS::KC_F )) {
			m_gameView->Update( 0.2f);
			m_game->Update(0.2f);
		}

		if ( keyEvent->isKeyDown( OIS::KC_R )) {
			m_player->ResetOrientation();
			m_activeCamera->ResetOrientation();
		}
		if ( keyEvent->isKeyDown( OIS::KC_A )) {
			transVector += Ogre::Vector3(-0.4f,0,0);
		}
		if (keyEvent->isKeyDown( OIS::KC_D )) {
			transVector += Ogre::Vector3(+0.4f,0,0);
		}
		if (keyEvent->isKeyDown( OIS::KC_W )) {
			transVector += Ogre::Vector3(0,0,-0.4f);
		}
		if (keyEvent->isKeyDown( OIS::KC_S )) {
			transVector += Ogre::Vector3(0,0,+0.4f);
		}
	}

	if (transVector != Ogre::Vector3::ZERO) {
		m_activeCamera->Move( transVector, a_timeSinceLastFrame);
	}

	m_activeCamera->Rotate( m_gameView->GetMouseMovement());
	return true;
}

void DebugController::UpdateCamera()
{
	m_activeCamera = m_gameSettings->GetActiveCamera();
}


DebugController::~DebugController()
{


}
