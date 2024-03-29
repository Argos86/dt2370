#ifndef Game_View_H_
#define Game_View_H_

#include <Ogre.h>
#include <OIS/OIS.h>
#include "KeyboardListener.h"
#include "Effects\CatmullRomProjectile.h"
#include "..\Controller\IEvent.h"

class GameView : public IEvent
{
private:
	OIS::Keyboard *m_keyboard;
	OIS::Mouse *m_mouse;
	KeyboardListener *m_listener;
	Ogre::SceneManager *m_scenemgr;

	static const int MAX_SPLINES = 50;
	CatmullRomProjectile *m_splines[MAX_SPLINES];
	int m_splineId;

	Ogre::ParticleSystem *m_splatterSystem;
	Ogre::SceneNode *m_splatterNode;
	Ogre::ParticleEmitter *m_splatterEmitter;
	static const int MAX_PARTICLE_LIFETIME = 600;
	float m_splatterTimer;
	bool m_particlesActive;


public:	
	enum ViewType {FIRSTPERSON, PERSPECTIVE};

	GameView::GameView(OIS::InputManager *a_inputManager, Ogre::Root *a_root, Ogre::SceneManager *a_scenemgr);
	bool GameView::MouseLeftPressed();
	bool GameView::MouseRightPressed();
	void GameView::UpdateInput( );

	void GameView::MakeSpline( Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance);
	void GameView::MakeSplineHit( Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance);
	void GameView::Update(float a_timeSinceLastFrame);

	void GameView::MakeSplatterEffect( Ogre::Vector3 a_enemyPosition);

	void GameView::UpdateMouseAnimation(Ogre::Vector2 a_movement, float a_timeSinceLastFrame);

	Ogre::Vector2 GameView::GetMouseMovement();
	OIS::Keyboard *GameView::GetKeyEvent();

	GameView::~GameView();
};

#endif
