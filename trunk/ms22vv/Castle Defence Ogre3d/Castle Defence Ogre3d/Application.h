#ifndef Main_Controller_H_
#define Main_Controller_H_

#include <Ogre.h>
#include <OIS/OIS.h>
#include <CEGUI/CEGUI.h>
#include <OgreCEGUIRenderer.h>
#include "Model\Player.h"
#include "Model\Level\Level.h"
#include "View\GameView.h"
#include "View\Camera\Camera.h"
#include "Model\GameSettings.h"

#include "Model\GameTimer.h"

using namespace Ogre;

class Application
{
private:
	Ogre::Root *m_root;
    OIS::Keyboard *m_keyboard;
	OIS::Mouse *m_mouse;
    OIS::InputManager *m_inputManager;
    CEGUI::OgreCEGUIRenderer *m_renderer;
    CEGUI::System *m_system;
	Ogre::SceneManager *m_scenemgr;
	RenderWindow *m_window;

	Player *m_player ;
	CameraManager *m_firstPersonCamera;
	CameraManager *m_perspectiveCamera;
	GameView *m_gameView;
	Level *m_level;

	GameTimer *m_timer;
		
	void Application::CreateRoot();    
	void Application::DefineResources();    
	void Application::SetupRenderSystem();    
	void Application::CreateRenderWindow();
	void Application::InitializeResourceGroups();
	void Application::SetupScene();
	void Application::SetupInputSystem();
	void Application::SetupCEGUI();
	void Application::CreateFrameListener();

public:
    void Init();
	void Application::StartRenderLoop();
	Application::~Application();
};




#endif