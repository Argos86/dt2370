#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>

#include "Weapon.h"
#include "Projectile.h"

Projectile::Projectile(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::SceneManager *a_scenemgr, Ogre::Real a_fireId, Ogre::String a_weaponName)
{
	std::stringstream name;
    name << "Fire" << a_fireId << a_weaponName;
    m_uniqueName = name.str();
	m_scenemgr = a_scenemgr;

	// Billboard
	m_projectileBbs = m_scenemgr->createBillboardSet( m_uniqueName );
	m_projectileBbs->setDefaultDimensions(5, 5);
	m_projectileBbs->setMaterialName("flare");
	m_projectileBbs->createBillboard(0,0,0, Ogre::ColourValue::White);


	m_time = 0;
	m_velocity =  3.8f;

	m_projectileNode = m_scenemgr->getRootSceneNode()->createChildSceneNode( m_uniqueName, a_weaponPosition );
	m_projectileNode->setOrientation(a_weaponOrientation);

	m_projectileNode->attachObject( m_projectileBbs );
}

bool Projectile::Update(float a_timeSinceLastFrame)
{
	m_time += a_timeSinceLastFrame;
	if (m_time > 1500)
	{
		return false;
	}
	m_projectileNode->setPosition( m_projectileNode->getPosition() + m_projectileNode->getOrientation() * Ogre::Vector3(0.0f,0.0f,-1.0f * m_velocity * a_timeSinceLastFrame) );	
	return true;
}

Projectile::~Projectile()
{
	m_projectileBbs = NULL;
	m_projectileNode = NULL;
	m_scenemgr->destroyBillboardSet(m_uniqueName);
	m_scenemgr->destroySceneNode(m_uniqueName);
	m_scenemgr = NULL;
}