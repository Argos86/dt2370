#ifndef Player_H_
#define Player_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include "Weapon\Weapon.h"
#include "..\Controller\IEvent.h"


class Player
{
private:
	Ogre::SceneNode *m_playerNode;
	Ogre::SceneNode *m_playerEntityNode;
	Ogre::Entity *m_playerEntity;

	Ogre::SceneNode *m_donutNode;
	Ogre::Entity *m_donutEntity;
	float m_playerVelocity;

	//Weapon
	WeaponBase *m_leftWeapon;
	WeaponBase *m_rightWeapon;

public:	
	Player::Player(Ogre::SceneManager *a_scenemgr, IEvent *a_eventToView, IEvent *a_eventToModel);
	void Player::Move(Ogre::Vector3 movementVector, float a_timeSinceLastFrame);
	void Player::UpdateWeapon( float a_timeSinceLastFrame );
	void Player::Rotate(Ogre::Vector2 a_mousePosition);

	Ogre::Vector3 Player::GetPosition();
	Ogre::Quaternion Player::GetOrientation();
	void Player::ResetOrientation();
	float Player::GetVelocity();

	void Player::FireLeftWeapon();
	void Player::FireRightWeapon();

	Ogre::Vector3 Player::GetWeaponPosition();
	Ogre::Quaternion Player::GetWeaponOrientation();

	Player::~Player();
};
#endif
