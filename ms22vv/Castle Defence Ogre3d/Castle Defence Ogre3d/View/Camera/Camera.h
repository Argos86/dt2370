#ifndef Camera_Manager_H_
#define Camera_Manager_H_

#include <OgreRoot.h>
#include <OgreSceneManager.h>
#include <OgreCamera.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreRenderWindow.h>
#include <OgreViewport.h>

class CameraManager
{
protected:
	Ogre::SceneNode *m_cameraNode;
	Ogre::SceneNode *m_cameraYawNode;
	Ogre::SceneNode *m_cameraPitchNode;
	Ogre::SceneNode *m_cameraRollNode;

	Ogre::SceneNode *m_transNode;
	Ogre::Vector3 m_camPosition;
	Ogre::Quaternion m_camOrientation;
	Ogre::Viewport *m_camVp;
	Ogre::RenderWindow *m_window;
	Ogre::Camera *m_camera;
	Ogre::String m_cameraName;

	Ogre::Real m_offsetX;
	Ogre::Real m_offsetY;
	Ogre::Quaternion tempQuat;



public:	
	CameraManager::CameraManager( Ogre::SceneManager *a_scenemgr, Ogre::RenderWindow *a_window, Ogre::String a_name );
	virtual void CameraManager::Update( Ogre::Vector3 a_playerPosition, Ogre::Vector2 a_mousePosition );
	void CameraManager::Move(Ogre::Vector3 a_movementVector, float a_timeSinceLastFrame);
	void CameraManager::Rotate(Ogre::Vector2 a_mousePosition);
	void CameraManager::DisableViewport();
	void CameraManager::EnableViewport();

	Ogre::Vector3 CameraManager::GetPosition();
	Ogre::Quaternion CameraManager::GetOrientation();
	virtual void CameraManager::ResetOrientation();
	CameraManager::~CameraManager();
};
#endif
