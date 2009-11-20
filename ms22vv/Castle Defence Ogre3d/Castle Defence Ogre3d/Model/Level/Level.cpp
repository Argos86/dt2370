#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgrePlane.h>
#include <OgreMeshManager.h>

#include "Level.h"
#include "Enemy.h"
#include "LightSource.h"
#include "Castle.h"
#include "..\Collision.h"
#include "..\..\Controller\IEvent.h"

Level::Level(Ogre::SceneManager *a_scenemgr)
: IEvent()
{
	m_castle = new Castle(a_scenemgr);
	// Belysningen, tillfällingt
	m_light1 = new LightSource(a_scenemgr, Ogre::Vector3(-600, 100, -600), "Light1");
	m_light2 = new LightSource(a_scenemgr, Ogre::Vector3(600, 100, 600), "Light2");

	// Skyboxen, tillfällingt
    a_scenemgr->setSkyBox(true, "Ruins");

	// Plane, tillfällingt
	Ogre::Plane plane(Ogre::Vector3::UNIT_Y, 0);
	Ogre::MeshManager::getSingleton().createPlane("ground",
		Ogre::ResourceGroupManager::DEFAULT_RESOURCE_GROUP_NAME, plane,
	   1500,1500,20,20,true,1,5,5,Ogre::Vector3::UNIT_Z);
	Ogre::Entity *entGround = a_scenemgr->createEntity("GroundEntity", "ground");
	entGround->setMaterialName("test1");
    entGround->setCastShadows(false);
	a_scenemgr->getRootSceneNode()->createChildSceneNode()->attachObject(entGround);

	for (int x = 0; x < MAX_ENEMIES-1 ; x++)
	{
		m_enemy[x] = NULL;
	}
	m_enemyId = 0;

	// Bara för att hämta boundingboxen..
	m_enemy[0] = new Enemy( a_scenemgr, m_enemyId);
	collisionTest = new Collision(m_enemy[0]->GetAABB());
	delete m_enemy[0];
	m_enemy[0] = NULL;
}

void Level::SpawnEnemy( Ogre::SceneManager *a_scenemgr, float a_timeSinceLastFrame )
{
	m_enemy[m_enemyId] = new Enemy( a_scenemgr, m_enemyId);
	m_enemyId += 1;
}

void Level::UpdateEnemies(float a_timeSinceLastFrame )
{
	for (int x = 0; x < MAX_ENEMIES -1 ; x++) {
		if(m_enemy[x] != NULL)	{

			m_enemy[x]->Update(a_timeSinceLastFrame);

			if ( m_enemy[x]->GetPosition().z > +800 ) {
				delete m_enemy[x];
				m_enemy[x] = NULL;
			}
		}
	}
	if ( m_enemyId == (MAX_ENEMIES -1) ) {
		m_enemyId = 0;
	}		
}

bool Level::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation)
{
	for (int x = 0; x < MAX_ENEMIES -1 ; x++) {
		if(m_enemy[x] != NULL)	{

			if (collisionTest->CollisionAABB(a_initialPoint, a_orientation, m_enemy[x]->GetPosition())) {
				delete m_enemy[x];
				m_enemy[x] = NULL;
				std::cout << "COLLISIONTEST = TRUE"<< std::endl;
				return true;
			}
		}
	}
	return false;
}

int Level::GetLastCollisionDistance()
{
	return collisionTest->GetCollisionDistance();
}

Level::~Level()
{

}


