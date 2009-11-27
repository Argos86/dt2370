#include <OgreSceneNode.h>
#include <OgreString.h>
#include "IModel.h"

IModel::IModel() {}
void IModel::SpawnEnemy() {}
bool IModel::CollisiontestEnemies(Ogre::Vector3 a_initialPoint, Ogre::Quaternion a_orientation) {return true;}
int IModel::GetLastCollisionDistance() {return true;}
void IModel::DamageEnemies(int a_amount, float a_delay ) {}
