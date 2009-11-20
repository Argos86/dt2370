#include <OgreSceneManager.h>
#include <OgreRoot.h>
#include <OgreCamera.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreRenderWindow.h>
#include <OgreViewport.h>
#include "Camera.h"

CameraManager::CameraManager(Ogre::SceneManager *a_scenemgr, Ogre::RenderWindow *a_window, Ogre::String a_name)
{
	m_cameraName = a_name;
	m_cameraNode = a_scenemgr->getRootSceneNode()->createChildSceneNode();
	m_cameraNode->setPosition(Ogre::Vector3(0,0,0));
	m_camera = a_scenemgr->createCamera(m_cameraName);
	m_camera->setNearClipDistance(10);
	m_camera->setPosition(Ogre::Vector3(0,0,0));

	m_window = a_window;
	
	m_offsetX = 0;
	m_offsetY = 0;

	//Ogre::CompositorManager::getSingleton().addCompositor(m_camVp, "blur");
	//Ogre::CompositorManager::getSingleton().setCompositorEnabled(m_camVp, "blur", false);
	//m_camera->setAspectRatio(m_camVp->getActualWidth() / m_camVp->getActualHeight());
}

void CameraManager::Update(Ogre::Vector3 a_playerPosition, Ogre::Vector2 a_mousePosition)
{

}

void CameraManager::Move(Ogre::Vector3 a_movementVector, float a_timeSinceLastFrame)
{
	m_cameraNode->setPosition( m_cameraNode->getPosition() + m_camera->getOrientation() * a_movementVector * a_timeSinceLastFrame );
}

void CameraManager::Rotate(Ogre::Vector2 a_mousePosition)
{
	m_camera->yaw(Ogre::Degree(- a_mousePosition.x));
	m_camera->pitch(Ogre::Degree(- a_mousePosition.y));
}
void CameraManager::DisableViewport()
{
	m_window->removeViewport(0);
}

void CameraManager::EnableViewport()
{
	m_camVp = m_window->addViewport(m_camera, 0, 0, 0, 1.0, 1.0 );
}

Ogre::Vector3 CameraManager::GetPosition()
{
	if(m_camera) {
		return m_camera->getPosition();
	}
	else {
		return Ogre::Vector3();
	}
}

Ogre::Quaternion CameraManager::GetOrientation()
{
	if(m_camera) {
		return m_camera->getOrientation();
	}
	else {
		return Ogre::Quaternion();
	}	
}

void CameraManager::ResetOrientation()
{
}

CameraManager::~CameraManager()
{
	
}

