#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreQuaternion.h>
#include <OgreAxisAlignedBox.h>

#include "Collision.h"

Collision::Collision(Ogre::AxisAlignedBox a_enemyAABB)
{
	m_enemyAABB = a_enemyAABB;
}

bool Collision::CollisionAABB(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation, Ogre::Vector3 a_enemyPosition)
{
	Ogre::AxisAlignedBox tempAABB = m_enemyAABB;
	tempAABB.setMinimum(tempAABB.getMinimum() + a_enemyPosition);
	tempAABB.setMaximum(tempAABB.getMaximum() + a_enemyPosition);

	for (int x=0; x<MAX_DISTANCE; x++)
	{
		if (tempAABB.intersects(a_initialPoint + a_orientation * Ogre::Vector3(0,0,-x)))
		{
			m_lastCollisionPoint = a_initialPoint + a_orientation * Ogre::Vector3(0,0,-x);
			m_lastCollisionDistance = x;
			return true;
		}
	}
	return false;
}

int Collision::GetCollisionDistance()
{
	return m_lastCollisionDistance;
}

Ogre::Vector3 Collision::GetCollisionPoint()
{
	return m_lastCollisionPoint;
}

Collision::~Collision()
{

}


