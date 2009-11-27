#ifndef Hud_Manager_H_
#define Hud_Manager_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreQuaternion.h>
#include <OgreTextAreaOverlayElement.h>
#include <OgreOverlayManager.h>
#include <OgreOverlayContainer.h>
#include <OgreFontManager.h>

#include "..\Controller\IEvent.h"

class HudManager : public IEvent
{
private:
	Ogre::OverlayManager *m_overlayManager;
	Ogre::OverlayContainer *m_panel;
	Ogre::TextAreaOverlayElement *m_textArea;
	Ogre::Overlay *m_overlay;
	Ogre::String m_currentMoney;

	Ogre::String m_helpText;

public:	
	HudManager::HudManager();
	void HudManager::UpdateMoney(int a_amount);
	HudManager::~HudManager();

};
#endif
