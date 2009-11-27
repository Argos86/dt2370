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

bool Collision::CollisionAtCoordinates(Ogre::Vector3 a_mousePosition, float a_radius , Ogre::Vector3 a_enemyPosition)
{
	if (a_enemyPosition.x > a_mousePosition.x - a_radius  && a_enemyPosition.x < a_mousePosition.x + a_radius) {
		if (a_enemyPosition.z > a_mousePosition.z - a_radius  && a_enemyPosition.z < a_mousePosition.z + a_radius ) {
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


