#ifndef Projectile_H_
#define Projectile_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>

class Projectile 
{
private:
	Ogre::SceneNode *m_projectileNode;
	Ogre::String m_uniqueName;
	Ogre::BillboardSet *m_projectileBbs;
	Ogre::SceneManager *m_scenemgr;
	Ogre::Real m_velocity;
	float m_time;

public:	
	Projectile::Projectile( Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::SceneManager *a_scenemgr, Ogre::Real a_fireId, Ogre::String a_weaponName);
	bool Projectile::Update( float a_timeSinceLastFrame);
	
	Projectile::~Projectile();	
};
#endif
