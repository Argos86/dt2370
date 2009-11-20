#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreBillboardSet.h>

#include "WeaponSight.h"

WeaponSight::WeaponSight(Ogre::SceneNode *a_weaponNode, Ogre::SceneManager *a_scenemgr, Ogre::String a_weaponName)
{
	std::stringstream name;
	name << "Sight:" << a_weaponName;
	m_sightName = name.str();
	m_scenemgr = a_scenemgr;

	m_sightBbs = m_scenemgr->createBillboardSet( m_sightName );
	m_sightBbs->setDefaultDimensions(5, 5);
	m_sightBbs->setMaterialName("Examples/Flare");
	m_sightBbs->createBillboard(0,0,0, Ogre::ColourValue::Red);
	
	m_sightNode = a_weaponNode->createChildSceneNode( m_sightName, Ogre::Vector3::ZERO);
	m_sightNode->attachObject( m_sightBbs );
	m_sightNode->setPosition( m_sightNode->getPosition() + m_sightNode->getOrientation() * Ogre::Vector3(0,0,-200));
} 



WeaponSight::~WeaponSight()
{
	m_sightNode = NULL;
	m_sightBbs = NULL;

	//TODO: Måste komma på något bra sätt än o ha SceneManager...
	m_scenemgr->destroyBillboardSet(m_sightName);
	m_scenemgr->destroySceneNode(m_sightName);
	m_scenemgr = NULL;
}
