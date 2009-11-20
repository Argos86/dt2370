#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include "Castle.h"

Castle::Castle(Ogre::SceneManager *a_scenemgr)
{
	m_castleEntity = a_scenemgr->createEntity( "CastleEntity", "Mesh/Box02.mesh" );
	m_castleEntity->setMaterialName("test1");
	m_castleEntity->setCastShadows(true);

	m_castleNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( "CastleNode", Ogre::Vector3(0.0f, 0.0f, +800.0f) );
	m_castleNode->setScale(100.0,4.0,4.0);
	m_castleNode->attachObject( m_castleEntity );

	m_hitpointLevel = 0;
	m_hitpoints = INITIAL_HITPOINS;
}

void Castle::UpdateWeapon( float a_timeSinceLastFrame )
{
	if (m_hitpoints <= 0)
	{
		//GAMEOVER
	}
	//m_hitpoints += m_hitpointLevel * HITPOINTS_PER_LEVEL;
}

void Castle::UdpateToLevel()
{
	m_hitpoints = INITIAL_HITPOINS + m_hitpointLevel * HITPOINTS_PER_LEVEL;
}

void Castle::UpgradeHitpoints()
{
	m_hitpointLevel += 1;
}

void Castle::TakeDamage(int a_quantity)
{
	m_hitpoints -= a_quantity;
}
	
void Castle::UpgradeWeapon()
{
	
}

void Castle::NewWeapon()
{

}

Castle::~Castle()
{

}


