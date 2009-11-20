#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>

#include "Laser.h"
#include "WeaponBase.h"
#include "..\..\Controller\IEvent.h"

Laser::Laser(Ogre::SceneNode *a_playerNode, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IEvent *a_eventToModel) 
	: WeaponBase (a_playerNode, a_scenemgr, a_relativePosition, a_name)
{
	m_eventToView = a_eventToView;
	m_eventToModel = a_eventToModel;

	m_timeSinceFired = 0.0;
	m_timeBetweenFire = 500.0;
	m_recoil = 0.0;
}

void Laser::Update( Ogre::Real a_timeSinceLastFrame)
{
	m_timeSinceFired += a_timeSinceLastFrame;
	m_weaponNode->setPosition(m_relativePosition + m_weaponNode->getOrientation() * Ogre::Vector3(0,0, + m_recoil));
	if (m_recoil > 0.0)
	{
		m_recoil -= m_recoil * (a_timeSinceLastFrame/300);
	}
}

void Laser::Fire()
{
	if (m_timeSinceFired > m_timeBetweenFire) {
		//Kollar om jag träffar, om jag gör det så gör jag en coolare "spline" :P
		if (!m_eventToModel->CollisiontestEnemies(m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(),m_weaponNode->getParentSceneNode()->getOrientation())) {
			m_eventToView->MakeSpline(m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(), m_weaponNode->getParentSceneNode()->getOrientation(), m_uniqueName, MAX_DISTANCE);
		}
		else {
			m_eventToView->MakeSplineHit(m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(), m_weaponNode->getParentSceneNode()->getOrientation(), m_uniqueName, m_eventToModel->GetLastCollisionDistance());
		}
		m_timeSinceFired = 0.0;		
		this->MakeRecoil(20.0);
	}
}



Laser::~Laser()
{

}
