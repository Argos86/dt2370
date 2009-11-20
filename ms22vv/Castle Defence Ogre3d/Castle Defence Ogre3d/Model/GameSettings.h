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

public:
	static const int GAMESTATE_PAUSED = 0;
	static const int GAMESTATE_RUNNING = 1;
	static const int GAMESTATE_DEBUG = 2;

	static const int CAMERA_FIRST_PERSON = 3;
	static const int CAMERA_PERSPECTIVE = 4;

	GameSettings::GameSettings(CameraManager *a_perspectiveCamera, CameraManager *a_firstPersonCamera);
	void GameSettings::ChangeGameState(int a_state);
	int GameSettings::GetGameState();
	void GameSettings::ToggleCamera();
	CameraManager* GameSettings::GetActiveCamera();
	GameSettings::~GameSettings();
};
#endif
