#include <Ogre.h>
#include <OIS/OIS.h>
#include <CEGUI/CEGUI.h>
#include <OgreCEGUIRenderer.h>

#include "Application.h"
#include "Model\Player.h"
#include "Model\Game.h"
#include "View\GameView.h"
#include "View\Camera\Camera.h"
#include "View\Camera\FirstPerson.h"
#include "View\Camera\Perspective.h"
#include "View\Sound\SoundEffect.h"
#include "View\HudManager.h"

#include "Model\GameTimer.h"
#include "Model\GameSettings.h"
#include "Controller\GameController.h"
#include "Controller\DebugController.h"
#include "Controller\PerspectiveController.h"
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

    delete m_GUIrenderer;
    //delete m_GUIsystem;

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

	// För loggningen, tillfällig.
	Ogre::LogManager::getSingleton().setLogDetail(Ogre::LL_BOREME);
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
	m_GUIrenderer = new CEGUI::OgreCEGUIRenderer(m_window, Ogre::RENDER_QUEUE_OVERLAY, false, 3000, m_scenemgr);
	m_GUIsystem = new CEGUI::System(m_GUIrenderer);
	CEGUI::Logger::getSingleton().setLoggingLevel(CEGUI::Insane);

    //CEGUI::SchemeManager::getSingleton().loadScheme((CEGUI::utf8*)"TaharezLook.scheme");
   // CEGUI::System::getSingleton().setDefaultMouseCursor((CEGUI::utf8*)"TaharezLook", (CEGUI::utf8*)"MouseArrow");
    //CEGUI::MouseCursor::getSingleton().setImage("TaharezLook", "MouseMoveCursor");

	//CEGUI::Font *f = CEGUI::FontManager::getSingleton().createFont("Advent.font");
	//m_GUIsystem->setDefaultFont(f);

/*
	m_GUISystem->setDefaultFont((CEGUI::utf8*)"BlueHighway-12");

	if(! CEGUI::FontManager::getSingleton().isFontPresent( "Advent" ) )
	CEGUI::FontManager::getSingleton().createFont( "Advent.ttf" );*/
}

void Application::StartRenderLoop()
{	
	m_perspectiveCamera = new Perspective(m_scenemgr, m_window, "PerspectiveCamera");
	m_firstPersonCamera = new FirstPerson(m_scenemgr, m_window, "FirstPersonCamera");

	GameSettings *m_gameSettings = new GameSettings(m_perspectiveCamera, m_firstPersonCamera);
	m_gameView = new GameView(m_inputManager, m_root, m_scenemgr);
	SoundEffect *m_soundEffects = new SoundEffect(m_scenemgr);
	m_game = new Game(m_scenemgr, m_gameView, m_soundEffects);	
	m_player = new Player(m_scenemgr );

	
	m_gameSettings->SetGameState(GameSettings::GAME_STATE_RUNNING);
	m_gameSettings->SetGameView(GameSettings::GAME_VIEW_PERSPECTIVE);
	int gameState = m_gameSettings->GetGameState();   //GameSettings::GAME_STATE_RUNNING;
	int gameView = m_gameSettings->GetGameView();   
	m_timer = new GameTimer;
	HudManager *m_hudMgr = new HudManager();

	DebugController *m_debugController = new DebugController(m_player, m_gameSettings, m_gameView, m_game, m_firstPersonCamera, m_scenemgr, m_hudMgr);
	GameController *m_gameController = new GameController(m_player, m_gameSettings, m_gameView, m_game, m_firstPersonCamera, m_scenemgr, m_hudMgr);
	PerspectiveController *m_perspectiveContr = new PerspectiveController(m_player, m_gameSettings, m_gameView, m_game, m_perspectiveCamera, m_scenemgr, m_hudMgr);

	
	float time = 0;
	//Main loop..
    while (m_root->renderOneFrame())
    {
		if (gameState == GameSettings::GAME_STATE_RUNNING) {
			if ( gameView == GameSettings::GAME_VIEW_PERSPECTIVE) {
				if (!m_perspectiveContr->DoControll( m_timer->GetTimeSinceLastFrame())) {
					std::cout << "Cangeing gamestate to DEBUG " << std::endl; 
					// om m_gameController->DoControll returnerar falskt så uppdaterar jag state...
					
					if (gameView != m_gameSettings->GetGameView())	{
						gameView = m_gameSettings->GetGameView();
						m_gameSettings->ToggleCamera();
					}
					else {
						gameState = m_gameSettings->GetGameState();
						m_debugController->UpdateCamera();
					}
				}
			}
			if ( gameView == GameSettings::GAME_VIEW_FIRST_PERSON) {
				if (!m_gameController->DoControll( m_timer->GetTimeSinceLastFrame())) {
					std::cout << "Cangeing gamestate to DEBUG " << std::endl; 
					// om m_gameController->DoControll returnerar falskt så uppdaterar jag state...
					if (gameView != m_gameSettings->GetGameView())	{
						gameView = m_gameSettings->GetGameView();
						m_gameSettings->ToggleCamera();
					}
					else {
						gameState = m_gameSettings->GetGameState();
						m_debugController->UpdateCamera();
					}
				}
			}
		}
		else if (gameState == GameSettings::GAME_STATE_DEBUG){
			if (!m_debugController->DoControll( m_timer->GetTimeSinceLastFrame())) {
				std::cout << "Cangeing gamestate to RUNNING " << std::endl; 
				gameState = m_gameSettings->GetGameState();
				m_perspectiveContr->UpdateCamera();
			}
		}
		else if (gameState == GameSettings::GAME_STATE_GAMEOVER) 
		{
			break;
		}

		time += m_timer->GetTimeSinceLastFrame();
		if (time > 10.0) {
			std::cout << "Fps: "  << m_window->getAverageFPS() << std::endl; 
			time = 0.0;
		}		
    }
}