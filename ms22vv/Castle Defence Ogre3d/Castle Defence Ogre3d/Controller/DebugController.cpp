#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "DebugController.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Level\Level.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"

DebugController::DebugController(Player *a_player,  GameSettings *a_gameSettings, GameView *a_gameView, Level *a_level )
{
	m_player = a_player;
	m_gameSettings = a_gameSettings;
	m_activeCamera = m_gameSettings->GetActiveCamera();
	m_gameView = a_gameView;
	m_level = a_level;
	m_time = 0.0;
}

bool DebugController::DoControll(Ogre::SceneManager *a_scenemgr, float a_timeSinceLastFrame)
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
			m_gameSettings->ChangeGameState(GameSettings::GAMESTATE_RUNNING);
			return false;
		}
		if ( keyEvent->isKeyDown( OIS::KC_C )) {
			m_gameSettings->ToggleCamera();
			m_activeCamera = m_gameSettings->GetActiveCamera();
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
