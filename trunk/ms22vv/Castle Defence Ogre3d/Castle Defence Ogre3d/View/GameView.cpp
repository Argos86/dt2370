#include <Ogre.h>
#include <OIS/OIS.h>

#include "GameView.h"
#include "KeyboardListener.h"
#include "Effects\CatmullRomProjectile.h"
#include "Sound\SoundManager.h"
#include "..\Controller\IEvent.h"


GameView::GameView(OIS::InputManager *a_inputManager, Ogre::Root *a_root, Ogre::SceneManager *a_scenemgr)
	: IEvent()
{
	m_scenemgr = a_scenemgr;

	try
        {
            m_keyboard = static_cast<OIS::Keyboard*>(a_inputManager->createInputObject(OIS::OISKeyboard, false));
            m_mouse = static_cast<OIS::Mouse*>(a_inputManager->createInputObject(OIS::OISMouse, false));
            //mJoy = static_cast<OIS::JoyStick*>(mInputManager->createInputObject(OIS::OISJoyStick, false));
        }
        catch (const OIS::Exception &e)
        {
			throw new Ogre::Exception(42, e.eText, "Application::setupInputSystem");
        }

	m_listener = new KeyboardListener(m_keyboard);
	a_root->addFrameListener(m_listener);

	m_splineId = 0;
	for (int x = 0; x < MAX_SPLINES-1 ; x++)
	{
		m_splines[x] = NULL;
	}


	m_splatterSystem = m_scenemgr->createParticleSystem("SplatterSystem", "Splatter");
	m_splatterNode = m_scenemgr->getRootSceneNode()->createChildSceneNode("SplatterNode");
	m_splatterNode->attachObject(m_splatterSystem);

	// Flyttar partiklarna sist i renderkön eftersom dom renderades efter marken :P, tillfälligt...
	m_splatterSystem->setRenderQueueGroup(Ogre::RENDER_QUEUE_OVERLAY);
	m_splatterEmitter = m_splatterSystem->getEmitter(0);
	m_splatterEmitter->setEnabled(false);

	//renderSystem->clearFrameBuffer(FBT_DEPTH);
}

void GameView::UpdateInput()
{
	m_keyboard->capture();
	m_mouse->capture();
}

void GameView::MakeSpline(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance)
{
	m_splines[m_splineId] = new CatmullRomProjectile( a_weaponPosition, a_weaponOrientation, m_scenemgr, m_splineId, a_weaponName, 1, a_distance);
	m_splineId += 1;
	if (m_splineId == MAX_SPLINES-1) {
		m_splineId = 0;
	}
}

void GameView::MakeSplineHit(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance)
{
	for (int x = 0; x <= 3; x++) {
		Ogre::Quaternion tempQ(Ogre::Degree(90 * (x+1)), Ogre::Vector3::UNIT_Z);
		m_splines[m_splineId] = new CatmullRomProjectile( a_weaponPosition, a_weaponOrientation * tempQ, m_scenemgr, m_splineId, a_weaponName, 6 , a_distance);
		m_splineId += 1;
		if (m_splineId == MAX_SPLINES-1) {
			m_splineId = 0;
		}
	}
}

void GameView::UpdateMouseAnimation(Ogre::Vector2 a_movement, float a_timeSinceLastFrame)
{/////////////////////////// ---------------------------------------------------------------ska bort
	//m_mouseAnim->MoveRelative(a_movement, a_timeSinceLastFrame);
}

void GameView::Update(float a_timeSinceLastFrame)
{
	for (int x = 0; x < MAX_SPLINES-1 ; x++) {
		if(m_splines[x] != NULL) {
			if (!m_splines[x]->Update(a_timeSinceLastFrame)) {
				delete m_splines[x];
				m_splines[x] = NULL;
			}
		}
	}
	if ( m_splineId == (MAX_SPLINES -1) ) {
		m_splineId = 0;
	}

	if (m_particlesActive ){
		m_splatterTimer-= a_timeSinceLastFrame;
		if (m_splatterTimer <= 0) {
			m_splatterEmitter->setEnabled(false);
		}
	}
}

void GameView::MakeSplatterEffect( Ogre::Vector3 a_enemyPosition)
{
	m_splatterNode->setPosition(a_enemyPosition);
	m_splatterEmitter->setEnabled(true);
	m_splatterTimer = (float)MAX_PARTICLE_LIFETIME;
	m_particlesActive = true;
}

Ogre::Vector2 GameView::GetMouseMovement()
{
	// 0.1 ska ersättas med någon "sense"
	return Ogre::Vector2(m_mouse->getMouseState().X.rel * 0.1, m_mouse->getMouseState().Y.rel * 0.1);
}

bool GameView::MouseLeftPressed()
{
	return m_mouse->getMouseState().buttonDown(OIS::MB_Left);
}
bool GameView::MouseRightPressed()
{
	 return m_mouse->getMouseState().buttonDown(OIS::MB_Right);
}

OIS::Keyboard *GameView::GetKeyEvent()
{
	return m_keyboard;
}

GameView::~GameView()
{
	delete m_listener;
	m_listener = NULL;
}


