#include <Ogre.h>
#include <OIS/OIS.h>
#include <CEGUI/CEGUI.h>
#include <OgreCEGUIRenderer.h>

#include "Application.h"
#include "Model\Player.h"
#include "Model\Level\Level.h"
#include "View\GameView.h"
#include "View\Camera\Camera.h"
#include "View\Camera\FirstPerson.h"
#include "View\Camera\Perspective.h"


#include "Model\GameTimer.h"
#include "Model\GameSettings.h"
#include "Controller\GameController.h"
#include "Controller\DebugController.h"
#include "Controller\IEvent.h"
#include <iostream>

using namespace Ogre;


void Application::Init()
{
    CreateRoot();
    DefineResources();
    SetupRenderSystem();
    CreateRenderWindow();
    InitializeResourceGroups();
    SetupScene();
    SetupInputSystem();
    SetupCEGUI();
}

Application::~Application()
{
    OIS::InputManager::destroyInputSystem(m_inputManager);

    //delete m_renderer;
    //delete m_system;

    delete m_root;

	//m_timer = NULL;
}


void Application::CreateRoot()
{
    m_root = new Root();
}

void Application::DefineResources()
{
    Ogre::String secName, typeName, archName;
	Ogre::ConfigFile cf;
    cf.load("resources.cfg");

    ConfigFile::SectionIterator seci = cf.getSectionIterator();
    while (seci.hasMoreElements())
    {
        secName = seci.peekNextKey();
        ConfigFile::SettingsMultiMap *settings = seci.getNext();
        ConfigFile::SettingsMultiMap::iterator i;
        for (i = settings->begin(); i != settings->end(); ++i)
        {
            typeName = i->first;
            archName = i->second;
            ResourceGroupManager::getSingleton().addResourceLocation(archName, typeName, secName);
        }
    }
}

void Application::SetupRenderSystem()
{
    if (!m_root->restoreConfig() && !m_root->showConfigDialog())
        throw Exception(52, "User canceled the config dialog!", "Application::setupRenderSystem()");

	Ogre::RenderSystem *rs = m_root->getRenderSystemByName("OpenGL Rendering Subsystem");
	m_root->setRenderSystem(rs);
    rs->setConfigOption("Full Screen", "No");
    rs->setConfigOption("Video Mode", "1280 x 720 @ 32-bit colour");
}

void Application::CreateRenderWindow()
{
    m_root->initialise(true, "Castle Defence");

    //mRoot->initialise(false);
    //HWND hWnd = 0;  // Get the hWnd of the application!
    //NameValuePairList misc;
    //misc["externalWindowHandle"] = StringConverter::toString((int)hWnd);
    //RenderWindow *win = mRoot->createRenderWindow("Main RenderWindow", 800, 600, false, &misc);
}

void Application::InitializeResourceGroups()
{
    TextureManager::getSingleton().setDefaultNumMipmaps(5);
    ResourceGroupManager::getSingleton().initialiseAllResourceGroups();
}

void Application::SetupScene()
{
    m_scenemgr = m_root->createSceneManager(ST_GENERIC, "Default SceneManager");
	//m_scenemgr->setShadowTechnique(SHADOWTYPE_TEXTURE_MODULATIVE);
}

void Application::SetupInputSystem()
{
    size_t windowHnd = 0;
    std::ostringstream windowHndStr;
    OIS::ParamList pl;
    m_window = m_root->getAutoCreatedWindow();

    m_window->getCustomAttribute("WINDOW", &windowHnd);
    windowHndStr << windowHnd;
    pl.insert(std::make_pair(std::string("WINDOW"), windowHndStr.str()));
    m_inputManager = OIS::InputManager::createInputSystem(pl);
}

void Application::SetupCEGUI()
{
	// CEGUI setup
	m_renderer = new CEGUI::OgreCEGUIRenderer(m_window, Ogre::RENDER_QUEUE_OVERLAY, false, 3000, m_scenemgr);
	m_system = new CEGUI::System(m_renderer);
}



void Application::StartRenderLoop()
{	
	m_perspectiveCamera = new Perspective(m_scenemgr, m_window, "PerspectiveCamera");
	m_firstPersonCamera = new FirstPerson(m_scenemgr, m_window, "FirstPersonCamera");

	GameSettings *m_gameSettings = new GameSettings(m_perspectiveCamera, m_firstPersonCamera);
	m_gameView = new GameView(m_inputManager, m_root, m_scenemgr);
	m_level = new Level(m_scenemgr);

	IEvent *m_eventToView = m_gameView;
	IEvent *m_eventToModel = m_level;
	
	m_player = new Player(m_scenemgr, m_eventToView, m_eventToModel );


	int gamestate = GameSettings::GAMESTATE_RUNNING;
	bool cameraUpdated = true;
	m_timer = new GameTimer;

	DebugController *m_debugController = new DebugController(m_player, m_gameSettings, m_gameView, m_level);
	GameController *m_gameController = new GameController(m_player, m_gameSettings, m_gameView, m_level);

	float time = 0;
	//Main loop..
    while (m_root->renderOneFrame())
    {
		if (gamestate == GameSettings::GAMESTATE_RUNNING) {
			if (!m_gameController->DoControll(m_scenemgr, m_timer->GetTimeSinceLastFrame())) {
				std::cout << "Cangeing gamestate to DEBUG " << std::endl; 
				// om m_gameController->DoControll returnerar falskt så uppdaterar jag state...
				gamestate = m_gameSettings->GetGameState();
				m_debugController->UpdateCamera();
			}
		}
		else if (gamestate == GameSettings::GAMESTATE_DEBUG){
			if (!m_debugController->DoControll(m_scenemgr, m_timer->GetTimeSinceLastFrame())) {
				std::cout << "Cangeing gamestate to RUNNING " << std::endl; 
				gamestate = m_gameSettings->GetGameState();
				m_gameController->UpdateCamera();
			}
		}

		time += m_timer->GetTimeSinceLastFrame();
		if (time > 1.0) {
			std::cout << "Fps: "  << m_window->getAverageFPS() << std::endl; 
			time = 0.0;
		}
    }
}