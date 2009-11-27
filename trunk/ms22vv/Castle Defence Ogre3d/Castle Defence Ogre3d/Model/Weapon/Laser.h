#ifndef Laser_H_
#define Laser_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>

#include "WeaponBase.h"
#include "..\IModel.h"
#include "..\..\Controller\IEvent.h"


class Laser : public WeaponBase
{
private:
	static const int MAX_DISTANCE = 1600;
	static const int ATTACK_DAMAGE = 5;

public:	
	Laser::Laser( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IModel *a_eventToModel, ISound *a_soundEffects);
	void Laser::Update( float a_timeSinceLastFrame );
	void Laser::Fire();
	Laser::~Laser();
};
#endif
