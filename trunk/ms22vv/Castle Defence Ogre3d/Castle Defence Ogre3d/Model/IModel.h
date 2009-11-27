#ifndef Interface_Model_H_
#define Interface_Model_H_

#include <OgreSceneNode.h>
#include <OgreString.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>

class IModel
{
private:

public:	
	IModel::IModel();

	virtual void IModel::SpawnEnemy();
	virtual bool IModel::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation);
	virtual int IModel::GetLastCollisionDistance();
	virtual void IModel::DamageEnemies(int a_amount, float a_delay );
};
#endif
