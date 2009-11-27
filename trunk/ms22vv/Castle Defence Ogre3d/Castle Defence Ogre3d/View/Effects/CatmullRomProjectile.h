#ifndef Catmull_Rom_Projectile_H_
#define Catmull_Rom_Projectile_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>
#include <OgreSimpleSpline.h>
#include <OgreRibbonTrail.h>
#include <OgreFrameListener.h>

class CatmullRomProjectile
{
private:
	Ogre::SceneNode *m_projectileNode;
	Ogre::String m_uniqueName;
	Ogre::BillboardSet *m_projectileBbs;
	Ogre::SceneManager *m_scenemgr;
	Ogre::SimpleSpline *m_spline;
	Ogre::RibbonTrail *m_trail;
	void CatmullRomProjectile::InitTrail();

	float m_time;
	float m_lifetime;


public:	
	CatmullRomProjectile::CatmullRomProjectile( Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::SceneManager *a_scenemgr, Ogre::Real a_fireId, Ogre::String a_weaponName, float a_offset, int a_distance);
	bool CatmullRomProjectile::Update( Ogre::Real a_timeSinceLastFrame);
	
	
	CatmullRomProjectile::~CatmullRomProjectile();	
};
#endif
