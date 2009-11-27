#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include "Castle.h"

Castle::Castle(Ogre::SceneManager *a_scenemgr)
{
	m_castleEntity = a_scenemgr->createEntity( "CastleEntity", "Mesh/castleWall.mesh" );
	m_castleEntity->setMaterialName("stoneWall");


	m_castleEntity->setCastShadows(false);
	m_castleNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( "CastleNode", Ogre::Vector3(0.0f, 0.0f, +800.0f) );
	m_castleNode->setScale(5.0,4.0,4.0);
	m_castleNode->attachObject( m_castleEntity );
	
	Ogre::Entity *m_castleTowerEntity = a_scenemgr->createEntity( "CastleTower01Ent", "Mesh/castleTower.mesh" );
	m_castleTowerEntity->setMaterialName("stoneWall");
	Ogre::SceneNode *m_towerNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( "CastleTowerNode01", Ogre::Vector3(+400.0f, 0.0f, +800.0f));
	m_towerNode->attachObject(m_castleTowerEntity);	
	m_towerNode->setScale(1.5,1.5,1.5);

	Ogre::Entity *m_castleTowerEntity02 = a_scenemgr->createEntity( "CastleTower02Ent", "Mesh/castleTower.mesh" );
	m_castleTowerEntity02->setMaterialName("stoneWall");
	Ogre::SceneNode *m_tower02Node = a_scenemgr->getRootSceneNode()->createChildSceneNode( "CastleTowerNode02", Ogre::Vector3(-400.0f, 0.0f, +800.0f));
	m_tower02Node->attachObject(m_castleTowerEntity02);	
	m_tower02Node->setScale(1.5,1.5,1.5);

	m_hitPointLevel = 0;
	m_hitPoints = INITIAL_HITPOINS;

	//m_castleNode->showBoundingBox(true);
}

bool Castle::Update()
{
	if (m_hitPoints > 0) {
		return true;
	} 
	else return false;
}

void Castle::UpdateWeapon( float a_timeSinceLastFrame )
{
	if (m_hitPoints <= 0)
	{
		//GAMEOVER
	}
	//m_hitpoints += m_hitpointLevel * HITPOINTS_PER_LEVEL;
}

void Castle::UpdateToLevel()
{
	m_hitPoints = INITIAL_HITPOINS + m_hitPointLevel * HITPOINTS_PER_LEVEL;
}

void Castle::UpgradeHitpoints()
{
	m_hitPointLevel += 1;
}

void Castle::TakeDamage(int a_amount)
{
	m_hitPoints -= a_amount;
	std::cout << "Castle hitpoints = " << m_hitPoints << std::endl;
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


