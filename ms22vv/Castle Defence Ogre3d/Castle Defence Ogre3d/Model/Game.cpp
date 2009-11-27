#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgrePlane.h>
#include <OgreMeshManager.h>

#include "IModel.h"
#include "..\View\Sound\ISound.h"
#include "Game.h"
#include "Wave\Enemy.h"
#include "LightSource.h"
#include "Castle.h"
#include "Collision.h"
#include "..\Controller\IEvent.h"
#include "Weapon\WeaponBase.h"
#include "Weapon\WeaponFactory.h"
#include "Wave\Wave.h"

Game::Game(Ogre::SceneManager *a_scenemgr, IEvent *a_eventToView, ISound *a_soundEffects)
: IModel()
{
	m_scenemgr = a_scenemgr;
	m_castle = new Castle(m_scenemgr);
	// Belysningen, tillfällingt
	m_light1 = new LightSource(m_scenemgr, Ogre::Vector3(-600, 100, -600), "Light1");
	m_light2 = new LightSource(m_scenemgr, Ogre::Vector3(600, 100, 600), "Light2");

	// Skyboxen, tillfällingt
    m_scenemgr->setSkyBox(true, "Ruins");

	// Plane, tillfällingt
	Ogre::Plane plane(Ogre::Vector3::UNIT_Y, 0);
	Ogre::MeshManager::getSingleton().createPlane("ground",
		Ogre::ResourceGroupManager::DEFAULT_RESOURCE_GROUP_NAME, plane,
	   1500,1500,20,20,true,1,5,5,Ogre::Vector3::UNIT_Z);
	Ogre::Entity *entGround = m_scenemgr->createEntity("GroundEntity", "ground");
	entGround->setMaterialName("grassMaterial");
    entGround->setCastShadows(false);
	m_scenemgr->getRootSceneNode()->createChildSceneNode()->attachObject(entGround);

	for (int x = 0; x < MAX_ENEMIES-1 ; x++)
	{
		m_enemy[x] = NULL;
	}
	m_enemyId = 0;

	// Bara för att hämta boundingboxen..
	m_enemy[0] = new Enemy( m_scenemgr, m_enemyId);
	collisionTest = new Collision(m_enemy[0]->GetAABB());
	delete m_enemy[0];
	m_enemy[0] = NULL;

	m_eventToModel = this;
	m_eventToView = a_eventToView;
	m_soundEffects = a_soundEffects;

	m_weaponFactory = new WeaponFactory(m_eventToView, m_eventToModel, m_soundEffects);
	
	m_positionEastWeapon = Ogre::Vector3(+430,+210,+800) ;
	m_positionCenterWeapon = Ogre::Vector3(0,+25,+600) ;
	m_positionWestWeapon = Ogre::Vector3(-370,+210,+800) ;

	m_eastWeapon = m_weaponFactory->CreateWeapon( m_scenemgr, m_positionEastWeapon, m_weaponFactory->LASER, "EastWeapon");
	m_centerWeapon = m_weaponFactory->CreateWeapon( m_scenemgr, m_positionCenterWeapon, m_weaponFactory->STANDARD, "CenterWeapon");
	m_westWeapon = m_weaponFactory->CreateWeapon( m_scenemgr, m_positionWestWeapon, m_weaponFactory->LASER, "WestWeapon");

	m_wave = new Wave(m_eventToModel);
	m_wave->NewWave();

	m_playerMoney = 0;
}

void Game::SpawnEnemy( )
{
	m_enemy[m_enemyId] = new Enemy( m_scenemgr, m_enemyId);
	m_enemyId += 1;
}

bool Game::Update(float a_timeSinceLastFrame)
{
	m_wave->Update(a_timeSinceLastFrame);
	this->UpdateEnemies(a_timeSinceLastFrame);
	m_eastWeapon->Update(a_timeSinceLastFrame);
	m_centerWeapon->Update(a_timeSinceLastFrame);
	m_westWeapon->Update(a_timeSinceLastFrame);	
	return this->UpdateCastle(a_timeSinceLastFrame);
}

void Game::UpdateEnemies(float a_timeSinceLastFrame )
{
	for (int x = 0; x < MAX_ENEMIES -1 ; x++) {
		if(m_enemy[x] != NULL)	{
			m_enemy[x]->Update(a_timeSinceLastFrame);
			if (m_enemy[x]->IsDead()) {
				m_soundEffects->MakeEnemyDeath();
				m_eventToView->MakeSplatterEffect(m_enemy[x]->GetPosition());
				m_playerMoney += m_enemy[x]->m_moneyForKill;
				m_eventToView->UpdateMoney(m_playerMoney);
				delete m_enemy[x];
				m_enemy[x] = NULL;
			}
			else if ( m_enemy[x]->GetPosition().z > +800 ) {
				m_castle->TakeDamage(1);
				delete m_enemy[x];
				m_enemy[x] = NULL;
			}
		}
	}
	if ( m_enemyId == (MAX_ENEMIES -1) ) {
		m_enemyId = 0;
	}
}

bool Game::UpdateCastle( float a_timeSinceLastFrame )
{
	return m_castle->Update();
}

void Game::DamageEnemies(int a_amount, float a_delay)
{
	// anledningen till loopen är att jag ska kunna skada flera fiender vid AOE-vapen
	for (int x = 0; x < MAX_ENEMIES -1 ; x++) {
		if(m_enemy[x] != NULL) {
			if ( m_enemy[x]->m_collided ) {
				if (a_delay > 0.0) {
					m_enemy[x]->DelayDamage(a_delay);
				}
				m_enemy[x]->TakeDamage(a_amount);
			}
		}
	}	
}

void Game::DamageCastle( int a_amount )
{
	m_castle->TakeDamage(a_amount);
}

bool Game::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation)
{
	for (int x = 0; x < MAX_ENEMIES -1 ; x++) {
		if(m_enemy[x] != NULL)	{
			if (collisionTest->CollisionAABB(a_initialPoint, a_orientation, m_enemy[x]->GetPosition())) {
				m_enemy[x]->m_collided = true;
				return true;
			}
		}
	}
	return false;
}

bool Game::CollisionAOE(Ogre::Vector3 a_mousePosition, float a_radius, bool a_targetAll )
{
	bool collision = false;
	for (int x = 0; x < MAX_ENEMIES -1 ; x++) {
		if(m_enemy[x] != NULL)	{

			if (collisionTest->CollisionAtCoordinates(a_mousePosition, a_radius, m_enemy[x]->GetPosition())) {
				if (!a_targetAll) {
					//m_enemy[x]->m_collided = true;
					return true;
				}
				else {
					m_enemy[x]->m_collided = true;
					collision = true;
				}
			}
		}
	}
	return collision;
}

bool Game::IsMouseOverWeapon(Ogre::Vector3 a_mousePosition, float a_radius )
{
	if (collisionTest->CollisionAtCoordinates(a_mousePosition, a_radius, m_positionEastWeapon) || 
		collisionTest->CollisionAtCoordinates(a_mousePosition, a_radius, m_positionCenterWeapon) ||
		collisionTest->CollisionAtCoordinates(a_mousePosition, a_radius, m_positionWestWeapon) ) {
		return true;
	}
	else return false;
}

WeaponBase* Game::GetSelectedWeaponAt(Ogre::Vector3 a_mousePosition, float a_radius )
{
	if (collisionTest->CollisionAtCoordinates(a_mousePosition, a_radius, m_positionEastWeapon)) {
		return m_eastWeapon;
	}
	else if (collisionTest->CollisionAtCoordinates(a_mousePosition, a_radius, m_positionCenterWeapon)) {
		return m_centerWeapon;
	}
	else if (collisionTest->CollisionAtCoordinates(a_mousePosition, a_radius, m_positionWestWeapon)) {
		return m_westWeapon;
	}
	else return false;

}

int Game::GetLastCollisionDistance()
{
	return collisionTest->GetCollisionDistance();
}

Game::~Game()
{

}


