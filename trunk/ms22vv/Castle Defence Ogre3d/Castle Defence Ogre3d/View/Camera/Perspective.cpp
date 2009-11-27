#include <OgreRoot.h>
#include <OgreSceneManager.h>
#include <OgreCamera.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreRenderWindow.h>
#include <OgreViewport.h>

#include "Perspective.h"
#include "Camera.h"

Perspective::Perspective(Ogre::SceneManager *a_scenemgr, Ogre::RenderWindow *a_window, Ogre::String a_name)
	: CameraManager (a_scenemgr, a_window, a_name)
{
	m_cameraNode->attachObject(m_camera);

	m_camera->setPosition(Ogre::Vector3(0,0,0));
	m_cameraNode->setPosition(Ogre::Vector3(-1000,+1000,+1000));	
	m_camera->lookAt(Ogre::Vector3(-200,0,+400));
}

void Perspective::Update(Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::Vector2 a_mousePosition)
{
	m_cameraNode->setPosition( Ogre::Vector3(-1000,+1000,+1000) );
}

void Perspective::ResetOrientation()
{
	std::cout << "Camera::Quaternion = " << this->GetOrientation()  << std::endl; 
	m_camera->lookAt(Ogre::Vector3(-200,0,+400));
	m_cameraNode->setPosition(Ogre::Vector3(-1000,+1000,+1000));
	m_offsetX = 0;
	m_offsetY = 0;
}

Perspective::~Perspective()
{
	
}

