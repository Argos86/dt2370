#ifndef Game_H_
#define Game_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgrePlane.h>
#include <OgreMeshManager.h>

#include "IModel.h"
#include "..\View\Sound\ISound.h"
#include "Wave\Enemy.h"
#include "LightSource.h"
#include "Castle.h"
#include "Collision.h"
#include "..\Controller\IEvent.h"
#include "Weapon\WeaponBase.h"
#include "Weapon\WeaponFactory.h"
#include "Wave\Wave.h"

class Game : public IModel
{
private:
	void Game::UpdateEnemies( float a_timeSinceLastFrame );
	bool Game::UpdateCastle( float a_timeSinceLastFrame );

	static const int MAX_ENEMIES = 100;
	Castle *m_castle;
	LightSource *m_light1;
	LightSource *m_light2;

	IModel *m_eventToModel;
	IEvent *m_eventToView;
	ISound *m_soundEffects;

	Ogre::SceneManager *m_scenemgr;

	WeaponFactory *m_weaponFactory;
	Ogre::Vector3 m_positionEastWeapon;
	Ogre::Vector3 m_positionCenterWeapon;
	Ogre::Vector3 m_positionWestWeapon;
	WeaponBase *m_eastWeapon;
	WeaponBase *m_centerWeapon;
	WeaponBase *m_westWeapon;
	Wave *m_wave;
	Enemy *m_enemy[MAX_ENEMIES];
	int m_enemyId;
	Collision *collisionTest;

public:	
	int m_playerMoney;
	enum WeaponPosition {EAST, CENTER, WEST};

	Game::Game( Ogre::SceneManager *a_scenemgr, IEvent *a_eventToView, ISound *a_soundEffects);
	void Game::SpawnEnemy( );
	bool Game::Update( float a_timeSinceLastFrame );

	void Game::DamageEnemies( int a_amount, float a_delay );
	void Game::DamageCastle( int a_amount );

	bool Game::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation);
	bool Game::CollisionAOE(Ogre::Vector3 a_mousePosition, float a_radius, bool a_targetAll );
	bool Game::IsMouseOverWeapon(Ogre::Vector3 a_mousePosition, float a_radius );
	WeaponBase* Game::GetSelectedWeaponAt(Ogre::Vector3 a_mousePosition, float a_radius );
	int Game::GetLastCollisionDistance();
	Game::~Game();
};
#endif
