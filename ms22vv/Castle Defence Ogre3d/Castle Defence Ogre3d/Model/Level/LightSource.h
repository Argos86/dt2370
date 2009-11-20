#ifndef Light_Source_H_
#define Light_Source_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreLight.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>

class LightSource
{
private:
	Ogre::SceneNode *m_lightNode;
	Ogre::Light *m_light;
	Ogre::BillboardSet *m_lightBbs;
	Ogre::String m_name;

public:	
	LightSource::LightSource( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_position, Ogre::String a_name);
	LightSource::~LightSource();
};
#endif
