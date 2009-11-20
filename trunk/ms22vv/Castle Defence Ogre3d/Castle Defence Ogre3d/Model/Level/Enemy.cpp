#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreQuaternion.h>

#include "Enemy.h"
#include <cstdlib> 

Enemy::Enemy(Ogre::SceneManager *a_scenemgr, int a_enemyId)
{
	std::stringstream name;
    name << "Enemy" << a_enemyId;
    m_uniqueName = name.str();
	m_scenemgr = a_scenemgr;

	m_enemyEntity = m_scenemgr->createEntity( m_uniqueName, "Mesh/Box02.mesh" );
	m_enemyEntity->setMaterialName("test1");
	m_enemyEntity->setCastShadows(false);

	m_enemyNode = m_scenemgr->getRootSceneNode()->createChildSceneNode( m_uniqueName, Ogre::Vector3(0.0f, 0.0f, 0.0f) );
	m_enemyNode->setScale(1.0,1.0,1.0);
	m_enemyNode->attachObject( m_enemyEntity );
	m_enemyVelocity = 0.1;

	std::cout << " bounding box : " << m_enemyEntity->getBoundingBox() << std::endl;

	int randomInt = rand() % 1000 - 500;
	m_enemyNode->setPosition(Ogre::Vector3(randomInt,0,0));

	m_movementPossible = true;
}

void Enemy::Update( float a_timeSinceLastFrame )
{
	m_enemyNode->setPosition( m_enemyNode->getPosition() + m_enemyNode->getOrientation() * Ogre::Vector3(0.0f,0.0f, m_enemyVelocity * a_timeSinceLastFrame) );
}

Ogre::AxisAlignedBox Enemy::GetAABB()
{
	return m_enemyEntity->getBoundingBox();
}

void Enemy::MoveTo(Ogre::Vector3 a_destination)
{
	m_enemyNode->setPosition(a_destination);
}

void Enemy::SetMovement(bool a_state)
{
	m_movementPossible = a_state;
}

Ogre::Vector3 Enemy::GetPosition()
{
	if (m_enemyNode) {
		return m_enemyNode->getPosition();
	}
	else {
		return Ogre::Vector3();
	}
}

Ogre::Quaternion Enemy::GetOrientation()
{
	if (m_enemyNode) {
		return m_enemyNode->getOrientation();
	}
	else {
		return Ogre::Quaternion();
	}
}



Enemy::~Enemy()
{
	m_enemyEntity = NULL;
	m_enemyNode = NULL;
	//samma problem här..
	m_scenemgr->destroyEntity(m_uniqueName);
	m_scenemgr->destroySceneNode(m_uniqueName);	
}


