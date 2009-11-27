#ifndef Weapon_Factory_H_
#define Weapon_Factory_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>


#include "WeaponBase.h"
#include "..\..\Controller\IEvent.h"

class WeaponFactory
{
private:
	IEvent *m_eventToView;
	IModel *m_eventToModel;
	ISound *m_soundEffects;
	
public:	
	enum WeaponType {STANDARD, LASER, Weapon03};

	WeaponFactory::WeaponFactory( IEvent *a_eventToView, IModel *a_eventToModel , ISound *a_soundEffects);
	WeaponBase* WeaponFactory::CreateWeapon( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, int a_weaponType, Ogre::String a_name);
	WeaponFactory::~WeaponFactory();
};
#endif
