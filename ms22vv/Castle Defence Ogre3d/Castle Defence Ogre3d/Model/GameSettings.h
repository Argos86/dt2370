#ifndef Game_Settings_H_
#define Game_Settings_H_

#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include "..\View\Camera\Camera.h"
#include "..\View\Camera\Perspective.h"
#include "..\View\Camera\FirstPerson.h"


class GameSettings
{
private:
	CameraManager *m_activeCamera;
	CameraManager *m_perspectiveCamera;
	CameraManager *m_firstPersonCamera;
	int m_gameState;
	int m_gameView;

public:
	static const int GAME_STATE_PAUSED = 0;
	static const int GAME_STATE_RUNNING = 1;
	static const int GAME_STATE_DEBUG = 2;
	static const int GAME_STATE_GAMEOVER = 3;

	static const int CAMERA_FIRST_PERSON = 4;
	static const int CAMERA_PERSPECTIVE = 5;

	static const int GAME_VIEW_FIRST_PERSON = 6;
	static const int GAME_VIEW_PERSPECTIVE = 7;

	GameSettings::GameSettings(CameraManager *a_perspectiveCamera, CameraManager *a_firstPersonCamera);
	void GameSettings::SetGameState(int a_state);
	int GameSettings::GetGameState();
	void GameSettings::ToggleCamera();
	void GameSettings::SetGameView(int a_state);
	int GameSettings::GetGameView();
	CameraManager* GameSettings::GetActiveCamera();
	GameSettings::~GameSettings();
};
#endif
