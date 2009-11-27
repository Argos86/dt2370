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
	virtual void IEvent::MakeSplatterEffect( Ogre::Vector3 a_enemyPosition);

	virtual void IEvent::MakeLaserHit();
	virtual void IEvent::MakeLaserMiss();
	virtual void IEvent::MakeStandard();
	virtual void IEvent::MakeEnemyDeath();

	virtual void IEvent::UpdateMoney(int a_amount);
};
#endif
