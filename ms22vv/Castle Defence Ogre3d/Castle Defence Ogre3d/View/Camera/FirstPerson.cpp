#include <OgreRoot.h>
#include <OgreSceneManager.h>
#include <OgreCamera.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreRenderWindow.h>
#include <OgreViewport.h>

#include "FirstPerson.h"
#include "Camera.h"

FirstPerson::FirstPerson(Ogre::SceneManager *a_scenemgr, Ogre::RenderWindow *a_window, Ogre::String a_name)
	: CameraManager (a_scenemgr, a_window, a_name)
{
	m_cameraNode->setPosition(Ogre::Vector3(0,+80,+900));
	m_camera->setPosition(Ogre::Vector3(0,0,0));
	m_camera->lookAt(Ogre::Vector3(0,0,0));

	m_cameraNode->attachObject(m_camera);
}

void FirstPerson::Update(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::Vector2 a_mousePosition)
{
	m_cameraNode->setPosition( a_weaponPosition );

	//m_offsetX += a_mousePosition.x;
	m_offsetY = 20;
	/*
	if ( m_offsetX > 80) {	m_offsetX = 80;	}
	else if ( m_offsetX < -80) {	m_offsetX = -80; }
	else {
		m_camera->yaw(Ogre::Degree(- a_mousePosition.x));
		m_camera->pitch(Ogre::Degree(- a_mousePosition.y));
	}*/
	m_camera->setOrientation(a_weaponOrientation);
	m_cameraNode->setPosition(m_cameraNode->getPosition() + m_camera->getOrientation() * Ogre::Vector3(0, +m_offsetY, +60));
}


void FirstPerson::ResetOrientation()
{
	std::cout << "Camera::Quaternion = " << this->GetOrientation()  << std::endl; 
	m_cameraNode->setPosition(Ogre::Vector3(0,+80,+1000));
	m_camera->lookAt(Ogre::Vector3(0,0,0));
	m_offsetX = 0;
	m_offsetY = 0;
}

FirstPerson::~FirstPerson()
{
	
}

