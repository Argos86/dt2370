#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include "WeaponBase.h"
#include "Projectile.h"
#include "WeaponSight.h"
#include "..\IModel.h"
#include "..\..\Controller\IEvent.h"
#include "..\..\View\Sound\ISound.h"

WeaponBase::WeaponBase(Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IModel *a_eventToModel, ISound *a_soundEffects)
{
	m_eventToView = a_eventToView;
	m_eventToModel = a_eventToModel;
	m_soundEffects = a_soundEffects;

	std::stringstream name;
	name << "Weapon:" << a_name;
    m_uniqueName = name.str();

	m_relativePosition = a_relativePosition;
	m_scenemgr = a_scenemgr;

	m_weaponStandEntity = m_scenemgr->createEntity( m_uniqueName, "Mesh/Stativ.mesh" );
	m_weaponStandEntity->setMaterialName("test2");

	m_weaponStandNode = m_scenemgr->getRootSceneNode()->createChildSceneNode( m_uniqueName, Ogre::Vector3(0,0,0));
	m_weaponStandNode->attachObject( m_weaponStandEntity );

	name << "Weapon:" << a_name << "EntityNode:";
    m_uniqueName = name.str();

	m_weaponEntity = m_scenemgr->createEntity( m_uniqueName, "Mesh/Box02.mesh" );
	m_weaponEntity->setMaterialName("test1");

	m_weaponNode = m_weaponStandNode->createChildSceneNode( m_uniqueName, Ogre::Vector3(0,0,0));
	m_weaponNode->setScale(1.0,1.0,1.0);
	m_weaponNode->attachObject( m_weaponEntity );

	m_weaponStandNode->setPosition(a_relativePosition);
	m_weaponNode->setPosition(Ogre::Vector3(-20,+40,0));
	
	m_fireId = 0;

	m_sight = new WeaponSight(m_weaponNode, m_scenemgr, m_uniqueName);
} 

void WeaponBase::Rotate(Ogre::Vector2 a_mousePosition)
{
	m_weaponNode->yaw(Ogre::Degree(- a_mousePosition.x), Ogre::Node::TS_LOCAL);
	m_weaponNode->pitch(Ogre::Degree(- a_mousePosition.y), Ogre::Node::TS_LOCAL);
}

void WeaponBase::Update(float a_timeSinceLastFrame)
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
	if (m_weaponStandNode) {
		return m_weaponStandNode->getPosition() + m_weaponNode->getPosition();
	}
	else {
		return Ogre::Vector3();
	}
}

Ogre::Quaternion WeaponBase::GetOrientation()
{
	if (m_weaponNode != NULL) {
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


