#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreLight.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>

#include "LightSource.h"

LightSource::LightSource(Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_position, Ogre::String a_name)
{
	m_name = a_name;

	m_lightNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( m_name, a_position );

	m_light = a_scenemgr->createLight( m_name );
	m_light->setType(Ogre::Light::LT_POINT);
	m_light->setPosition(a_position);
	m_light->setDiffuseColour(1.0, 1.0, 2.0);
	m_light->setSpecularColour(1.0, 1.0, 2.0);

	// Billboarden är bara till för att se ljuskällan, än så länge...
	m_lightBbs = a_scenemgr->createBillboardSet( m_name );
	m_lightBbs->setDefaultDimensions(50, 50);
	m_lightBbs->setMaterialName("flare");
	m_lightBbs->createBillboard(0,0,0, Ogre::ColourValue::White);

	m_lightNode->attachObject( m_lightBbs );
}


LightSource::~LightSource()
{
	m_light = NULL;
	m_lightBbs = NULL;
	m_lightNode = NULL;

	//TODO: Måste komma på något bra sätt än o ha SceneManager...
	//->destroyBillboardSet(m_lightBbs);
}
