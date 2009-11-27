#ifndef Collision_H_
#define Collision_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreQuaternion.h>
#include <OgreAxisAlignedBox.h>

class Collision 
{
private:
	Ogre::AxisAlignedBox m_enemyAABB;
	static const int MAX_DISTANCE = 1400;
	int m_lastCollisionDistance;
	Ogre::Vector3 m_lastCollisionPoint;

public:	
	Collision::Collision(Ogre::AxisAlignedBox a_enemyAABB);
	
	bool Collision::CollisionAABB(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation, Ogre::Vector3 a_enemyPosition);
	bool Collision::CollisionAtCoordinates(Ogre::Vector3 a_mousePosition, float a_radius , Ogre::Vector3 a_enemyPosition);

	int Collision::GetCollisionDistance();
	Ogre::Vector3 Collision::GetCollisionPoint();
	Collision::~Collision();
};
#endif
