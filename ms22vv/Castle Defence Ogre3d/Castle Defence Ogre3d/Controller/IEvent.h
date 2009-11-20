#ifndef I_Event_H_
#define I_Event_H_

#include <OgreSceneNode.h>
#include <OgreString.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>



class IEvent
{
private:

public:	
	IEvent::IEvent();

	virtual void IEvent::MakeSpline(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance);
	virtual void IEvent::MakeSplineHit(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance);
	virtual bool IEvent::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation);
	virtual int IEvent::GetLastCollisionDistance();
};
#endif
