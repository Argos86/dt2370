#include <OgreSceneNode.h>
#include <OgreString.h>
#include "IEvent.h"

IEvent::IEvent() {}
void IEvent::MakeSpline(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance) {}
void IEvent::MakeSplineHit(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::String a_weaponName, int a_distance) {}
void IEvent::MakeSplatterEffect( Ogre::Vector3 a_enemyPosition) {}
void IEvent::MakeLaserHit() {}
void IEvent::MakeLaserMiss() {}
void IEvent::MakeStandard() {}
void IEvent::MakeEnemyDeath() {}
void IEvent::UpdateMoney(int a_amount) {}