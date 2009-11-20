#ifndef Level_H_
#define Level_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgrePlane.h>
#include <OgreMeshManager.h>

#include "Enemy.h"
#include "LightSource.h"
#include "Castle.h"
#include "..\Collision.h"
#include "..\..\Controller\IEvent.h"

class Level : public IEvent
{
private:
	Castle *m_castle;
	LightSource *m_light1;
	LightSource *m_light2;
	static const int MAX_ENEMIES = 100;
	Enemy *m_enemy[MAX_ENEMIES];
	int m_enemyId;
	Collision *collisionTest;

public:	
	Level::Level(Ogre::SceneManager *a_scenemgr);
	void Level::SpawnEnemy( Ogre::SceneManager *a_scenemgr, float a_timeSinceLastFrame);
	void Level::UpdateEnemies(float a_timeSinceLastFrame );
	bool Level::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation);
	int Level::GetLastCollisionDistance();

	Level::~Level();
};
#endif
