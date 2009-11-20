#ifndef Castle_H_
#define Castle_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>

class Castle 
{
private:
	Ogre::SceneNode *m_castleNode;
	Ogre::Entity *m_castleEntity;
	int m_hitpoints;
	int m_hitpointLevel;
	static const int HITPOINTS_PER_LEVEL = 10;
	static const int INITIAL_HITPOINS = 100;

public:	
	Castle::Castle(Ogre::SceneManager *a_scenemgr);
	//Update - Updatera vapen, tex om det är dax o skjuta och kollar om jag dött?
	void Castle::UpdateWeapon( float a_timeSinceLastFrame);
	//UdpateToLevel - Updatera efter att man handlat nya uppgraderingar
	void Castle::UdpateToLevel();
	void Castle::UpgradeHitpoints();
	void Castle::TakeDamage(int a_quantity);
	void Castle::UpgradeWeapon();
	void Castle::NewWeapon();
	Castle::~Castle();
};
#endif
