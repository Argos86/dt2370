#ifndef Laser_H_
#define Laser_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>

#include "WeaponBase.h"
#include "..\..\Controller\IEvent.h"


class Laser : public WeaponBase
{
private:
	IEvent *m_eventToView;
	IEvent *m_eventToModel;

	static const int MAX_DISTANCE = 1000;

public:	
	Laser::Laser( Ogre::SceneNode *a_playerNode, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IEvent *a_eventToModel);
	void Laser::Update( Ogre::Real a_timeSinceLastFrame );
	void Laser::Fire();
	Laser::~Laser();
};
#endif
