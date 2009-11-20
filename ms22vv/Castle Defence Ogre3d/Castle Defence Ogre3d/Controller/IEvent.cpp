#include <OgreSceneNode.h>
#include <OgreString.h>
#include "IEvent.h"

IEvent::IEvent()
{

}

void IEvent::MakeSpline(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance)
{

}
void IEvent::MakeSplineHit(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance)
{

}

bool IEvent::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation)
{
	return true;
}

int IEvent::GetLastCollisionDistance()
{
	return true;
}
