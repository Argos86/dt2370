#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreBillboardSet.h>
#include <OgreSimpleSpline.h>
#include <OgreRibbonTrail.h>

#include "CatmullRomProjectile.h"

CatmullRomProjectile::CatmullRomProjectile(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::SceneManager *a_scenemgr, Ogre::Real a_fireId, Ogre::String a_weaponName, float a_offset, int a_distance)
{
	std::stringstream name;
    name << "Fire" << a_fireId << a_weaponName;
    m_uniqueName = name.str();

	m_scenemgr = a_scenemgr;

	// Billboard
	
	m_projectileBbs = m_scenemgr->createBillboardSet( m_uniqueName );
	m_projectileBbs->setDefaultDimensions(4, 4);
	m_projectileBbs->setMaterialName("flare");
	m_projectileBbs->createBillboard(0,0,0, Ogre::ColourValue::Black);	

	m_spline = new Ogre::SimpleSpline();
	m_spline->addPoint(a_weaponPosition);
	m_spline->addPoint(a_weaponPosition + a_weaponOrientation * Ogre::Vector3(-a_offset*1.6,-a_offset*1.6,-a_distance * 0.2));
	m_spline->addPoint(a_weaponPosition + a_weaponOrientation * Ogre::Vector3(-a_offset*1.5,+a_offset*1.5,-a_distance * 0.4));
	m_spline->addPoint(a_weaponPosition + a_weaponOrientation * Ogre::Vector3(+a_offset/2,+a_offset/2,-a_distance * 0.6));
	m_spline->addPoint(a_weaponPosition + a_weaponOrientation * Ogre::Vector3(+0,-0,-a_distance * 0.8));
	m_spline->addPoint(a_weaponPosition + a_weaponOrientation * Ogre::Vector3(0,0,-a_distance));
	
	m_lifetime = a_distance / 5.0f;

	m_time = 0.001;

	m_projectileNode = m_scenemgr->getRootSceneNode()->createChildSceneNode( m_uniqueName, a_weaponPosition );
	m_projectileNode->setOrientation(a_weaponOrientation);
	m_projectileNode->setPosition(a_weaponPosition);

	this->InitTrail();

	m_projectileNode->attachObject( m_projectileBbs );
}

bool CatmullRomProjectile::Update(Ogre::Real a_timeSinceLastFrame)
{
	m_time += a_timeSinceLastFrame;
	if (m_time < m_lifetime) { 
		m_projectileNode->setPosition( m_spline->interpolate(m_time/m_lifetime) );
		return true;
	}
	// är bara för att jag vill hinna "fada" innan jag deletar.. 
	else if (m_time < m_lifetime*4.0f) {
		return true;
	}
	else {
		return false;
	}
}


void CatmullRomProjectile::InitTrail()
{	
	m_trail = m_scenemgr->createRibbonTrail(m_uniqueName);
	m_trail->setMaterialName("lightRibbonTrail");
	m_trail->setInitialColour(0, 0.6, 0.0, 0.0);
	m_trail->setColourChange(0, 0.3, 0.5, 0.5, 15);
	m_trail->setInitialWidth(0,3);
	m_trail->setWidthChange(0,10);
	m_trail->setTrailLength(700);
	m_trail->setMaxChainElements(15);
	m_trail->addNode(m_projectileNode);
    m_scenemgr->getRootSceneNode()->attachObject(m_trail);
}


CatmullRomProjectile::~CatmullRomProjectile()
{
	m_projectileNode = NULL;
	m_trail = NULL;
	m_spline = NULL;
	m_projectileBbs = NULL;

	m_scenemgr->destroyRibbonTrail(m_uniqueName);
	m_scenemgr->destroySceneNode(m_uniqueName);

	m_scenemgr->destroyBillboardSet(m_uniqueName);
	m_scenemgr = NULL;
}