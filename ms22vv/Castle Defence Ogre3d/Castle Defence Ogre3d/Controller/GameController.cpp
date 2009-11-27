#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "Core.h"
#include "GameController.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Game.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"
#include "IEvent.h"


GameController::GameController(Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr)
	: Core(a_player, a_gameSettings, a_gameView, a_game, a_camera, a_scenemgr, a_hudMgr)
{

}

bool GameController::DoControll( float a_timeSinceLastFrame)
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
			m_gameSettings->SetGameState(GameSettings::GAME_STATE_DEBUG);
			return false;
		}
		if ( m_time > 1000 && keyEvent->isKeyDown( OIS::KC_C )) {
			m_time = 0;
			m_gameSettings->SetGameView(GameSettings::GAME_VIEW_PERSPECTIVE);
			return false;
			//m_activeCamera = m_gameSettings->GetActiveCamera();
		}
		if ( keyEvent->isKeyDown( OIS::KC_E )) {
			m_game->SpawnEnemy();
		}
		if ( keyEvent->isKeyDown( OIS::KC_R )) {
			m_player->ResetOrientation();
			m_activeCamera->ResetOrientation();
		}
		if ( m_gameView->MouseLeftPressed()) {
			m_player->FireWeapon();
		}
		/*if ( m_gameView->MouseRightPressed()) {
			m_player->FireRightWeapon();
		}*/
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
		m_activeCamera->Update( m_player->GetWeaponPosition(), m_player->GetWeaponOrientation(), mouseInput);
	}

	m_gameView->Update( a_timeSinceLastFrame);
	m_player->UpdateWeapon( a_timeSinceLastFrame );
	m_hudMgr->UpdateMoney(m_game->m_playerMoney);
	// om slottet är dött returnerar jag false..
	if (!m_game->Update( a_timeSinceLastFrame )) {
		m_gameSettings->SetGameState(GameSettings::GAME_STATE_GAMEOVER);
		return false;
	}

	
	return true;
}

void GameController::UpdateCamera()
{
	m_activeCamera = m_gameSettings->GetActiveCamera();
	m_activeCamera->Update( m_player->GetPosition(),m_player->GetWeaponOrientation(), Ogre::Vector2::ZERO);
}

GameController::~GameController()
{


}
