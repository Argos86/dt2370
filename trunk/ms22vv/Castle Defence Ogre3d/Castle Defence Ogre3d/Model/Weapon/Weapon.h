#ifndef Weapon_H_
#define Weapon_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>

#include <OIS/OIS.h>
#include "Projectile.h"
#include "WeaponSight.h"
#include "WeaponBase.h"

class Weapon : public WeaponBase
{
private:
	static const int MAX_PROJECTILES = 200;
	Projectile *m_projectile[MAX_PROJECTILES];

public:	
	Weapon::Weapon( Ogre::SceneNode *a_playerNode, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name );
	void Weapon::Update( Ogre::Real a_timeSinceLastFrame );
	void Weapon::Fire();
	Weapon::~Weapon();
};
#endif
