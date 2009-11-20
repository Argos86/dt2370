#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>

#include "..\View\Camera\Camera.h"
#include "..\View\Camera\Perspective.h"
#include "..\View\Camera\FirstPerson.h"
#include "GameSettings.h"


GameSettings::GameSettings(CameraManager *a_perspectiveCamera, CameraManager *a_firstPersonCamera)
{
	m_perspectiveCamera = a_perspectiveCamera;
	m_firstPersonCamera = a_firstPersonCamera;
	m_activeCamera = m_firstPersonCamera;
	m_activeCamera->EnableViewport();
}

void GameSettings::ChangeGameState(int a_state)
{
	m_gameState = a_state;
}

int GameSettings::GetGameState()
{
	return m_gameState;
}

void GameSettings::ToggleCamera()
{
	if (m_activeCamera == m_perspectiveCamera)
	{
		m_activeCamera->DisableViewport();
		m_activeCamera = m_firstPersonCamera;
		m_activeCamera->EnableViewport();
	}
	else 
	{
		m_activeCamera->DisableViewport();
		m_activeCamera = m_perspectiveCamera;
		m_activeCamera->EnableViewport();
	}
}

CameraManager* GameSettings::GetActiveCamera()
{
	return m_activeCamera;
}

GameSettings::~GameSettings()
{
	m_activeCamera = NULL;
	m_perspectiveCamera = NULL;
	m_firstPersonCamera = NULL;

}