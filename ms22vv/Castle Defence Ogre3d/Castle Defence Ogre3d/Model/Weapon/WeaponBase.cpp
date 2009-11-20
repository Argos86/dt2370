#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include <OIS/OIS.h>
#include "WeaponBase.h"
#include "Projectile.h"
#include "WeaponSight.h"

WeaponBase::WeaponBase(Ogre::SceneNode *a_playerNode, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name)
{
	std::stringstream name;
	name << "Weapon:" << a_name;
    m_uniqueName = name.str();

	m_relativePosition = a_relativePosition;
	m_scenemgr = a_scenemgr;
	m_weaponEntity = m_scenemgr->createEntity( m_uniqueName, "Mesh/Box02.mesh" );
	m_weaponEntity->setMaterialName("test1");
	m_weaponNode = a_playerNode->createChildSceneNode( m_uniqueName, Ogre::Vector3(0,0,0));
	m_weaponNode->setScale(1.0,1.0,1.0);
	m_weaponNode->attachObject( m_weaponEntity );
	m_weaponNode->setPosition(m_relativePosition);
	
	m_fireId = 0;


	m_sight = new WeaponSight(m_weaponNode, m_scenemgr, m_uniqueName);
} 


void WeaponBase::Update( Ogre::Real a_timeSinceLastFrame)
{

}

void WeaponBase::Fire()
{

}

void WeaponBase::MakeRecoil(float a_force)
{
	m_recoil += a_force;
}



Ogre::Vector3 WeaponBase::GetPosition()
{
	if (m_weaponNode) {
		return m_weaponNode->getPosition();
	}
	else {
		return Ogre::Vector3();
	}
}

Ogre::Quaternion WeaponBase::GetOrientation()
{
	if (m_weaponNode) {
		return m_weaponNode->getOrientation();
	}
	else {
		return Ogre::Quaternion();
	}
}

void WeaponBase::ResetOrientation()
{
	std::cout << "Weapon::Quaternion = " << this->GetOrientation()  << std::endl; 
	m_weaponNode->setOrientation(Ogre::Quaternion::IDENTITY);
}


WeaponBase::~WeaponBase()
{
	m_weaponEntity = NULL;
	m_weaponNode = NULL;
	//samma problem här..
	m_scenemgr->destroyEntity(m_uniqueName);
	m_scenemgr->destroySceneNode(m_uniqueName);	
	m_scenemgr = NULL;

	delete m_sight;
	m_sight = NULL;

}


