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
#include "HudManager.h"
#include "..\Controller\IEvent.h"

HudManager::HudManager()
	:IEvent()
{
	m_overlayManager = Ogre::OverlayManager::getSingletonPtr();

	// Create a panel
	m_panel = static_cast<Ogre::OverlayContainer*>(	m_overlayManager->createOverlayElement("Panel", "Help text"));
	m_panel->setMetricsMode(Ogre::GMM_PIXELS);
	m_panel->setPosition(10, 10);
	m_panel->setDimensions(100, 100);

	m_textArea = static_cast<Ogre::TextAreaOverlayElement*>(m_overlayManager->createOverlayElement("TextArea", "TextAreaName"));
	m_textArea->setMetricsMode(Ogre::GMM_PIXELS);
	m_textArea->setPosition(0, 0);
	m_textArea->setDimensions(100, 100);

	m_helpText = " Press F1 for debug-mode \n Press C to change view \n Or selecta a weapon by clicking it \n \n";
	m_currentMoney = " Current money = 0";

	m_textArea->setCaption(m_helpText + m_currentMoney);

	m_textArea->setCharHeight(16);
	m_textArea->setFontName("Advent");
	m_textArea->setColourBottom(Ogre::ColourValue(0.4, 0.6, 0.4));
	m_textArea->setColourTop(Ogre::ColourValue(0.6, 0.8, 0.6));

	// Create an overlay, and add the panel
	m_overlay = m_overlayManager->create("Overlay");
	m_overlay->add2D(m_panel);
	// Add the text area to the panel
	m_panel->addChild(m_textArea);
	// Show the overlay
	m_overlay->show();
}

void HudManager::UpdateMoney(int a_amount)
{
	std::stringstream temp;
	temp << " Current money = "  << a_amount ;
    m_currentMoney = temp.str();
	m_textArea->setCaption(m_helpText + m_currentMoney);
}

HudManager::~HudManager()
{

}