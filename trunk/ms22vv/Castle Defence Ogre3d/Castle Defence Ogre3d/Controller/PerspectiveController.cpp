#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include <OIS/OIS.h>
#include "Core.h"
#include "PerspectiveController.h"
#include "..\Model\GameSettings.h"
#include "..\Model\Player.h"
#include "..\Model\Game.h"
#include "..\View\GameView.h"
#include "..\View\Camera\Camera.h"
#include "..\View\Effects\MouseAnimation.h"
#include "IEvent.h"


PerspectiveController::PerspectiveController(Player *a_player, GameSettings *a_gameSettings,  GameView *a_gameView, Game *a_game, CameraManager *a_camera, Ogre::SceneManager *a_scenemgr, HudManager *a_hudMgr) 
	: Core(a_player, a_gameSettings, a_gameView, a_game, a_camera, a_scenemgr, a_hudMgr)
{
	m_mouseAnim = new MouseAnimation(m_scenemgr);
}

bool PerspectiveController::DoControll( float a_timeSinceLastFrame)
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
		if ( keyEvent->isKeyDown( OIS::KC_E )) {
			m_game->SpawnEnemy();
		}
		if ( keyEvent->isKeyDown( OIS::KC_R )) {
			m_activeCamera->ResetOrientation();
		}
		if ( m_gameView->MouseLeftPressed()) {
			if (m_game->CollisionAOE(m_mouseAnim->GetPosition(), m_mouseAnim->GetRadius(), true)) {
				m_game->DamageEnemies(5, 0);
			}
			else if (m_game->IsMouseOverWeapon(m_mouseAnim->GetPosition(), m_mouseAnim->GetRadius())) {
				m_player->SetActiveWeapon(m_game->GetSelectedWeaponAt(m_mouseAnim->GetPosition(), m_mouseAnim->GetRadius()));
				m_gameSettings->SetGameView(GameSettings::GAME_VIEW_FIRST_PERSON);
				m_time = 0;
				return false;
			}
		}
	}
	// Flyttar player om transVector inte är "tom"
	if (transVector != Ogre::Vector3::ZERO) {
		m_player->Move( transVector, a_timeSinceLastFrame);
		updateCamera = true;
	}
	// flyttar musen om musen rört på sig
	if ( mouseInput != Ogre::Vector2::ZERO){
		m_mouseAnim->MoveRelative(mouseInput, a_timeSinceLastFrame);
		if (m_game->CollisionAOE(m_mouseAnim->GetPosition(), m_mouseAnim->GetRadius(), false)) {
			m_mouseAnim->SetAboveEnemy();
		}
		else if (m_game->IsMouseOverWeapon(m_mouseAnim->GetPosition(), m_mouseAnim->GetRadius())) {
			m_mouseAnim->SetAboveWeapon();
		}
		else {
			m_mouseAnim->SetNoTarget();
		}
		updateCamera = true;
	}

	// Updaterar kameran..
	if (updateCamera) {
		m_activeCamera->Update( m_player->GetPosition(), m_player->GetWeaponOrientation(), mouseInput );
	}
	m_hudMgr->UpdateMoney(m_game->m_playerMoney);
	m_gameView->Update( a_timeSinceLastFrame);
	if (!m_game->Update( a_timeSinceLastFrame )) {
		m_gameSettings->SetGameState(GameSettings::GAME_STATE_GAMEOVER);
		return false;
	}

	return true;
}

void PerspectiveController::UpdateCamera()
{
	m_activeCamera = m_gameSettings->GetActiveCamera();
	m_activeCamera->Update( m_player->GetPosition(), m_player->GetWeaponOrientation(), Ogre::Vector2::ZERO);
}

PerspectiveController::~PerspectiveController()
{


}
