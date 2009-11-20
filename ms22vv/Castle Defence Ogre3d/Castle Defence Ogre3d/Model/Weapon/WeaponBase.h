#ifndef Weapon_Base_H_
#define Weapon_Base_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include <OIS/OIS.h>
#include "WeaponSight.h"

class WeaponBase
{
protected:
	Ogre::Entity *m_weaponEntity;
	Ogre::SceneNode *m_weaponNode;
	Ogre::SceneManager *m_scenemgr;
	int m_fireId;
	WeaponSight *m_sight;
	Ogre::String m_uniqueName;
	Ogre::Vector3 m_relativePosition;
	float m_timeSinceFired;
	float m_timeBetweenFire;
	float m_recoil;

	void WeaponBase::MakeRecoil(float a_force);

public:	
	WeaponBase::WeaponBase( Ogre::SceneNode *a_playerNode, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name);
	virtual void WeaponBase::Update( Ogre::Real a_timeSinceLastFrame );
	virtual void WeaponBase::Fire( );
	

	WeaponBase::~WeaponBase();

	Ogre::Vector3 WeaponBase::GetPosition();
	Ogre::Quaternion WeaponBase::GetOrientation();
	void WeaponBase::ResetOrientation();
};
#endif
