using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// file: EnhanceCharacterJoint.cs
[RequireComponent (typeof (CharacterJoint))]
public class EnhanceCharacterJoint : MonoBehaviour {
}

// file: EnhanceCharacterJointEditor.cs
[CustomEditor(typeof(EnhanceCharacterJoint))]
class EnhanceCharacterJointEditor : Editor
{
    void OnSceneGUI () {

        //Debug.Log(this.target.GetType());
        EnhanceCharacterJoint script = (EnhanceCharacterJoint) this.target;
        if (script != null)
        {
            CharacterJoint joint = (CharacterJoint) script.gameObject.GetComponent(typeof(CharacterJoint));

            Vector3 twistAxis = joint.transform.TransformDirection(joint.axis).normalized;
            Vector3 swing1Axis = joint.transform.TransformDirection(joint.swingAxis).normalized;
            Vector3 swing2Axis = Vector3.Cross(twistAxis, swing1Axis);         

            float size = HandleUtility.GetHandleSize(joint.transform.position);

            Handles.color = new Color(1,0.5f,0) ; // orange == twist, direction = swing1, rotates about twist axis
            Handles.DrawWireArc(joint.transform.position, twistAxis, swing1Axis, joint.highTwistLimit.limit, size);
            Handles.DrawWireArc(joint.transform.position, twistAxis, swing1Axis, joint.lowTwistLimit.limit, size);
            Handles.DrawLine(joint.transform.position, joint.transform.position + Quaternion.AngleAxis(joint.highTwistLimit.limit, twistAxis) * swing1Axis * size);
            Handles.DrawLine(joint.transform.position, joint.transform.position + Quaternion.AngleAxis(joint.lowTwistLimit.limit, twistAxis) * swing1Axis * size);

            Handles.color = new Color(0,0.3f,0) ; // green=swing1, direction=twistAxis rotates about swing1Axis
            Handles.DrawWireArc(joint.transform.position, swing1Axis, twistAxis, joint.swing1Limit.limit, size);
            Handles.DrawWireArc(joint.transform.position, swing1Axis, twistAxis, -joint.swing1Limit.limit, size);          
            Handles.DrawLine(joint.transform.position, joint.transform.position + Quaternion.AngleAxis(joint.swing1Limit.limit, swing1Axis) * twistAxis * size);
            Handles.DrawLine(joint.transform.position, joint.transform.position + Quaternion.AngleAxis(-joint.swing1Limit.limit, swing1Axis) * twistAxis * size);

            Handles.color = new Color(0,0,0.5f); // blue=swing2, direction=twistAxis, rotates about swing2 axis
            Handles.DrawWireArc(joint.transform.position, swing2Axis, twistAxis, joint.swing2Limit.limit, size);
            Handles.DrawWireArc(joint.transform.position, swing2Axis, twistAxis, -joint.swing2Limit.limit, size);          
            Handles.DrawLine(joint.transform.position, joint.transform.position + Quaternion.AngleAxis(joint.swing2Limit.limit, swing2Axis) * twistAxis * size);
            Handles.DrawLine(joint.transform.position, joint.transform.position + Quaternion.AngleAxis(-joint.swing2Limit.limit, swing2Axis) * twistAxis * size);              
        }
    }
}
