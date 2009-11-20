#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include "Player.h"
#include "Weapon\Weapon.h"
#include "Weapon\WeaponFactory.h"
#include "..\Controller\IEvent.h"

Player::Player(Ogre::SceneManager *a_scenemgr, IEvent *a_eventToView, IEvent *a_eventToModel)
{
	m_playerEntity = a_scenemgr->createEntity( "PlayerEntity", "Mesh/Cone01.mesh" );
	m_playerEntity->setMaterialName("test1");
	m_playerEntity->setCastShadows(true);

	m_donutEntity = a_scenemgr->createEntity( "PlayerDonutEntity", "Mesh/Torus01.mesh" );
	m_donutEntity->setMaterialName("test2");
	m_donutEntity->setCastShadows(true);

	m_playerNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( "PlayerNode", Ogre::Vector3(0.0f, 0.0f, 0.0f) );
	m_playerNode->setScale(1.0,1.0,1.0);

	m_playerEntityNode = m_playerNode->createChildSceneNode( "PlayerEntityNode", Ogre::Vector3(0.0f, 0.0f, 0.0f) );
	m_playerEntityNode->attachObject( m_playerEntity );
	m_playerEntityNode->pitch(Ogre::Degree (- 90));

	m_donutNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( "PlayerDonutNode", Ogre::Vector3(0.0f, 0.0f, 0.0f) );
	m_donutNode->setScale(1.5,1.5,1.5);
	m_donutNode->attachObject( m_donutEntity );	

	m_playerVelocity = 0;
	m_playerNode->setPosition(Ogre::Vector3(0,+50,+800));
	m_donutNode->setPosition( m_playerNode->getPosition() + Ogre::Vector3(0, +20.0f, 0.0f) );
	
	//Skapar vapnet, testar Factoryn
	WeaponFactory *WeaponF = new WeaponFactory();
	m_leftWeapon = WeaponF->CreateWeapon(m_playerNode, a_scenemgr, Ogre::Vector3(-20,20,0), WeaponF->LASER, "LeftWeapon", a_eventToView, a_eventToModel);
	m_rightWeapon = WeaponF->CreateWeapon(m_playerNode, a_scenemgr, Ogre::Vector3(20,20,0), WeaponF->STANDARD, "RightWeapon", a_eventToView, a_eventToModel);
}

void Player::UpdateWeapon( float a_timeSinceLastFrame )
{
	m_leftWeapon->Update( a_timeSinceLastFrame);
	m_rightWeapon->Update( a_timeSinceLastFrame);
}

void Player::Move(Ogre::Vector3 a_movementVector, float a_timeSinceLastFrame)
{
	m_donutNode->roll( Ogre::Degree(- a_movementVector.x * a_timeSinceLastFrame) );
	m_donutNode->setPosition( m_donutNode->getPosition() + a_movementVector * a_timeSinceLastFrame );
	m_playerNode->setPosition( m_donutNode->getPosition() + Ogre::Vector3(0, -20.0f, 0.0f));
}

void Player::Rotate(Ogre::Vector2 a_mousePosition)
{
	m_playerNode->yaw(Ogre::Degree(- a_mousePosition.x), Ogre::Node::TS_WORLD);
	m_playerNode->pitch(Ogre::Degree(- a_mousePosition.y), Ogre::Node::TS_WORLD);
}



void Player::ResetOrientation()
{
	std::cout << "Tvingar Player, Weapon och Camera-orientationen: Quaternion::IDENTITY " << std::endl << "Player::Quaternion = " << this->GetOrientation()  << std::endl; 
	m_playerNode->setOrientation(Ogre::Quaternion::IDENTITY);
	m_leftWeapon->ResetOrientation();
}


void Player::FireLeftWeapon()
{
	m_leftWeapon->Fire();
}

void Player::FireRightWeapon()
{
	m_rightWeapon->Fire();
}


Ogre::Vector3 Player::GetPosition()
{
	if (m_playerNode) {
		return m_playerNode->getPosition();
	}
	else {
		return Ogre::Vector3();
	}
}

Ogre::Quaternion Player::GetOrientation()
{
	if (m_playerNode) {
		return m_playerNode->getOrientation();
	}
	else {
		return Ogre::Quaternion();
	}
}


Ogre::Vector3 Player::GetWeaponPosition()
{
	if (m_leftWeapon) {
		return m_leftWeapon->GetPosition();
	}
	else {
		return Ogre::Vector3();
	}
}
Ogre::Quaternion Player::GetWeaponOrientation()
{
	if (m_leftWeapon) {
		return m_leftWeapon->GetOrientation();
	}
	else {
		return Ogre::Quaternion();
	}
}


float Player::GetVelocity()
{
	return m_playerVelocity;
}

Player::~Player()
{

}


