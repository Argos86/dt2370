#ifndef Enemy_H_
#define Enemy_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreQuaternion.h>
#include <OgreAxisAlignedBox.h>

class Enemy 
{
private:
	Ogre::SceneNode *m_enemyNode;
	Ogre::Entity *m_enemyEntity;
	float m_enemyVelocity;
	Ogre::String m_uniqueName;
	Ogre::SceneManager *m_scenemgr;
	bool m_movementPossible;

public:	
	Enemy::Enemy(Ogre::SceneManager *a_scenemgr, int a_enemyId);
	void Enemy::Update( float a_timeSinceLastFrame);
	Ogre::AxisAlignedBox Enemy::GetAABB();

	Ogre::Vector3 Enemy::GetPosition();
	Ogre::Quaternion Enemy::GetOrientation();

	void Enemy::MoveTo(Ogre::Vector3 a_destination);
	void Enemy::SetMovement(bool a_state);

	Enemy::~Enemy();
};
#endif
