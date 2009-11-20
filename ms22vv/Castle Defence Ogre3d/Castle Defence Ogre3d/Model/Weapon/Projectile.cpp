#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>

#include <OIS/OIS.h>
#include "Weapon.h"
#include "Projectile.h"

Projectile::Projectile(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::SceneManager *a_scenemgr, Ogre::Real a_fireId, Ogre::String a_weaponName)
{
	std::stringstream name;
    name << "Fire" << a_fireId << a_weaponName;
    m_uniqueName = name.str();

	//Vill inte ha instans av SceneManager här..
	m_scenemgr = a_scenemgr;

	// Billboard
	m_projectileBbs = m_scenemgr->createBillboardSet( m_uniqueName );
	m_projectileBbs->setDefaultDimensions(5, 5);
	m_projectileBbs->setMaterialName("Examples/Flare");
	m_projectileBbs->createBillboard(0,0,0, Ogre::ColourValue::White);	


	m_time = 0;
	m_velocity =  0.8f;

	m_projectileNode = m_scenemgr->getRootSceneNode()->createChildSceneNode( m_uniqueName, a_weaponPosition );
	m_projectileNode->setOrientation(a_weaponOrientation);
	//m_projectileNode->pitch(Ogre::Degree(+ 90));

	m_projectileNode->attachObject( m_projectileBbs );
	std::cout << "Projectile position: "<< m_projectileNode->getPosition() << std::endl; 

}

bool Projectile::Update(Ogre::Real a_timeSinceLastFrame)
{
	m_time += a_timeSinceLastFrame;
	//std::cout << "Projectile->m_time = " << m_time << std::endl;
	if (m_time > 2000)
	{
		return false;
	}
	m_projectileNode->setPosition( m_projectileNode->getPosition() + m_projectileNode->getOrientation() * Ogre::Vector3(0.0f,0.0f,-1.0f * m_velocity * a_timeSinceLastFrame) );	
	return true;
}

Projectile::~Projectile()
{
   // KILLCHILD(m_projectileNode);
	m_projectileBbs = NULL;
	m_projectileNode = NULL;
	m_scenemgr->destroyBillboardSet(m_uniqueName);
	m_scenemgr->destroySceneNode(m_uniqueName);
	//Ogre::SceneManager::destroyBillboardSet(m_uniqueName);
	//Ogre::SceneManager::get(m_uniqueName);
}