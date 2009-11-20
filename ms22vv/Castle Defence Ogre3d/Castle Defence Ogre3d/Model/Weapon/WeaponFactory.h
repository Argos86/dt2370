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
	static const int WEAPON_TYPE_01 = 0;
	static const int WEAPON_TYPE_02 = 1;

	

public:	
	enum type {STANDARD, LASER, Weapon03};

	WeaponFactory::WeaponFactory( );
	WeaponBase* WeaponFactory::CreateWeapon( Ogre::SceneNode *a_node, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, int a_weaponType, Ogre::String a_name, IEvent *a_eventToView, IEvent *a_eventToModel);
	WeaponFactory::~WeaponFactory();
};
#endif
