#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreQuaternion.h>
#include <OgreAxisAlignedBox.h>
#include "MouseAnimation.h"

MouseAnimation::MouseAnimation(Ogre::SceneManager *a_scenemgr)
{
	m_scenemgr = a_scenemgr;

	m_mouseEntity = m_scenemgr->createEntity( "MouseRing", "Mesh/Ring01.mesh" );
	m_mouseEntity->setMaterialName("test1");
	m_mouseEntity->setCastShadows(false);

	m_mouseNode = m_scenemgr->getRootSceneNode()->createChildSceneNode( "MouseRingNode", Ogre::Vector3(0.0f, 0.0f, 0.0f) );
	m_mouseNode->setScale((float)SCALE_AT_NORMAL,(float)SCALE_AT_NORMAL,(float)SCALE_AT_NORMAL);
	m_mouseNode->attachObject( m_mouseEntity );

	m_mouseNode->setPosition(Ogre::Vector3(0,+20,0));

	// Roterar för att rörelserna ska bli relativa kameravinkeln, tillfällig. 
	m_mouseNode->setOrientation(m_mouseNode->getOrientation() * Ogre::Quaternion(Ogre::Degree(-45) , Ogre::Vector3::UNIT_Y));

	m_sense = 12.0;
	m_radius = 50.0;
}

void MouseAnimation::Update( float a_timeSinceLastFrame)
{

}

void MouseAnimation::MoveRelative(Ogre::Vector2 a_movement, float a_timeSinceLastFrame)
{
	m_mouseNode->setPosition(m_mouseNode->getPosition() + m_mouseNode->getOrientation() * Ogre::Vector3(+a_movement.x, +0, +a_movement.y) * m_sense);
}

void MouseAnimation::SetVisibility(bool a_state)
{
	
}

void MouseAnimation::SetAboveEnemy()
{
	m_mouseNode->setScale((float)SCALE_AT_ABOVE,(float)SCALE_AT_ABOVE,(float)SCALE_AT_ABOVE);
	m_mouseEntity->setMaterialName("test3");
}

void MouseAnimation::SetAboveWeapon()
{
	m_mouseNode->setScale((float)SCALE_AT_ABOVE,(float)SCALE_AT_ABOVE,(float)SCALE_AT_ABOVE);
	m_mouseEntity->setMaterialName("test5");
}

void MouseAnimation::SetNoTarget()
{
	m_mouseNode->setScale((float)SCALE_AT_NORMAL,(float)SCALE_AT_NORMAL,(float)SCALE_AT_NORMAL);
	m_mouseEntity->setMaterialName("test1");
}


Ogre::Vector3 MouseAnimation::GetPosition()
{
	if (m_mouseNode) {
		return m_mouseNode->getPosition();
	}
	else {
		return Ogre::Vector3();
	}
}

float MouseAnimation::GetRadius()
{
	return m_radius;
}

MouseAnimation::~MouseAnimation()
{

}