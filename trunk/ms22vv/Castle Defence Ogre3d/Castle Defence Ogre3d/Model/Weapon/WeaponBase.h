#ifndef Weapon_Base_H_
#define Weapon_Base_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include "..\IModel.h"
#include "..\..\Controller\IEvent.h"
#include "..\..\View\Sound\ISound.h"

#include "WeaponSight.h"

class WeaponBase
{
protected:
	Ogre::Entity *m_weaponEntity;
	Ogre::Entity *m_weaponPipeEntity;
	Ogre::Entity *m_weaponStandEntity;
	Ogre::SceneNode *m_weaponNode;
	Ogre::SceneNode *m_weaponPipeNode;
	Ogre::SceneNode *m_weaponStandNode;
	Ogre::SceneManager *m_scenemgr;

	IEvent *m_eventToView;
	IModel *m_eventToModel;
	ISound *m_soundEffects;

	int m_fireId;
	WeaponSight *m_sight;
	Ogre::String m_uniqueName;
	Ogre::Vector3 m_relativePosition;
	float m_timeSinceFired;
	float m_timeBetweenFire;
	float m_recoil;

	void WeaponBase::MakeRecoil(float a_force);

public:	
	WeaponBase::WeaponBase( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IModel *a_eventToModel, ISound *a_soundEffects);
	virtual void WeaponBase::Update( float a_timeSinceLastFrame );
	virtual void WeaponBase::Fire( );
	
	void WeaponBase::Rotate(Ogre::Vector2 a_mousePosition);
	Ogre::Vector3 WeaponBase::GetPosition();
	Ogre::Quaternion WeaponBase::GetOrientation();
	void WeaponBase::ResetOrientation();
	WeaponBase::~WeaponBase();
};
#endif
