#ifndef Weapon_H_
#define Weapon_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>

#include "Projectile.h"
#include "WeaponSight.h"
#include "WeaponBase.h"
#include "..\IModel.h"
#include "..\..\Controller\IEvent.h"

class Weapon : public WeaponBase
{
private:
	static const int MAX_PROJECTILES = 100;
	Projectile *m_projectile[MAX_PROJECTILES];
	static const int MAX_DISTANCE = 1400;
	static const int ATTACK_DAMAGE = 2;

public:	
	Weapon::Weapon( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IModel *a_eventToModel, ISound *a_soundEffects );
	void Weapon::Update( float a_timeSinceLastFrame );
	void Weapon::Fire();
	Weapon::~Weapon();
};
#endif
