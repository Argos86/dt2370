#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "GameController.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Level\Level.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"
#include "IEvent.h"


GameController::GameController(Player *a_player, GameSettings *a_gameSettings, GameView *a_gameView, Level *a_level )
{
	m_player = a_player;	
	m_gameSettings = a_gameSettings;
	m_activeCamera = m_gameSettings->GetActiveCamera();
	m_gameView = a_gameView;
	m_level = a_level;
	m_time = 0;	
}

bool GameController::DoControll(Ogre::SceneManager *a_scenemgr, float a_timeSinceLastFrame)
{
	Ogre::Vector3 transVector = Ogre::Vector3::ZERO;

	m_gameView->UpdateInput();
	OIS::Keyboard *keyEvent = m_gameView->GetKeyEvent();
	Ogre::Vector2 mouseInput = m_gameView->GetMouseMovement();
	bool updateCamera = false; 
	m_time += a_timeSinceLastFrame;

	if (keyEvent != NULL) {
		if (m_time > 1000 && keyEvent->isKeyDown( OIS::KC_F1 )) {
			m_time = 0;
			m_gameSettings->ChangeGameState(GameSettings::GAMESTATE_DEBUG);
			return false;
		}
		if ( m_time > 1000 && keyEvent->isKeyDown( OIS::KC_C )) {
			m_time = 0;
			m_gameSettings->ToggleCamera();
			m_activeCamera = m_gameSettings->GetActiveCamera();
		}
		if ( keyEvent->isKeyDown( OIS::KC_E )) {
			m_level->SpawnEnemy(a_scenemgr,a_timeSinceLastFrame);
		}
		if ( keyEvent->isKeyDown( OIS::KC_R )) {
			m_player->ResetOrientation();
			m_activeCamera->ResetOrientation();
		}
		if ( m_gameView->MouseLeftPressed()) {
			m_player->FireLeftWeapon();
		}
		if ( m_gameView->MouseRightPressed()) {
			m_player->FireRightWeapon();
		}
		if ( keyEvent->isKeyDown( OIS::KC_A )) {
			transVector += Ogre::Vector3(-0.3f,0,0);
		}
		if (keyEvent->isKeyDown( OIS::KC_D )) {
			transVector += Ogre::Vector3(+0.3f,0,0);
		}
	}
	// Flyttar player om transVector inte är "tom"
	if (transVector != Ogre::Vector3::ZERO) {
		m_player->Move( transVector, a_timeSinceLastFrame);
		updateCamera = true;
	}
	// Roterar player om musen rört på sig
	if ( mouseInput != Ogre::Vector2::ZERO){
		m_player->Rotate( mouseInput );
		updateCamera = true;
	}
	// Updaterar cameran..
	if (updateCamera) {
		m_activeCamera->Update( m_player->GetPosition(), mouseInput );
	}

	m_gameView->UpdateSplines( a_timeSinceLastFrame);
	m_player->UpdateWeapon( a_timeSinceLastFrame );
	m_level->UpdateEnemies( a_timeSinceLastFrame );
	
	return true;
}

void GameController::UpdateCamera()
{
	m_activeCamera = m_gameSettings->GetActiveCamera();
	m_activeCamera->Update( m_player->GetPosition(), Ogre::Vector2::ZERO);
}

GameController::~GameController()
{


}
