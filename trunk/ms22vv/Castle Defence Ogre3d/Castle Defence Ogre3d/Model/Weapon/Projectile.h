#ifndef Projectile_H_
#define Projectile_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>

#include <OIS/OIS.h>

class Projectile 
{
private:
	Ogre::SceneNode *m_projectileNode;
	Ogre::String m_uniqueName;
	Ogre::BillboardSet *m_projectileBbs;
	// TODO: SceneManager ska bort!!
	Ogre::SceneManager *m_scenemgr;
	Ogre::Real m_velocity;

public:	
	Projectile::Projectile( Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::SceneManager *a_scenemgr, Ogre::Real a_fireId, Ogre::String a_weaponName);
	bool Projectile::Update( Ogre::Real a_timeSinceLastFrame);

	// tiden borde vara private..
	Ogre::Real m_time;
	Projectile::~Projectile();	
};
#endif
