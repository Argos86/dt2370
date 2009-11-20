#ifndef Weapon_Sight_H_
#define Weapon_Sight_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreBillboardSet.h>

class WeaponSight
{
private:
	Ogre::SceneNode *m_sightNode;
	Ogre::BillboardSet *m_sightBbs;
	Ogre::String m_sightName;
	Ogre::SceneManager *m_scenemgr;

public:	
	WeaponSight::WeaponSight( Ogre::SceneNode *a_playerNode, Ogre::SceneManager *a_scenemgr, Ogre::String a_weaponName );
	WeaponSight::~WeaponSight();
};
#endif
