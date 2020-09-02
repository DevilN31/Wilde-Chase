﻿using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Scriptables;

namespace MalbersAnimations.Controller
{

    /// <summary>Idle Should be the Last State on the Queue, when nothing is moving Happening </summary>
    public class Idle : State
    {
        [Space, Header("Idle Parameters")]
        [Tooltip("The Idle will be activated while the Animal is moving. Use this only when there's no Locomotion State")]
        public BoolReference IsLocomotion = new BoolReference(false);

        public override bool TryActivate()
        {
            //Activate when the animal is not moving and is grounded
            if (!IsLocomotion) //NORMAL IDLE IMPORTANT
            {
                return (animal.MovementAxisSmoothed == Vector3.zero && animal.MovementAxis == Vector3.zero && !animal.MovementDetected  &&  General.Grounded == animal.Grounded);
            }
            else
            {
                return (General.Grounded == animal.Grounded); //This enables that you can be on idle if you are not grounded too
            }
        }
       

#if UNITY_EDITOR
        void Reset()
        {
            ID = MalbersTools.GetInstance<StateID>("Idle");

            General = new AnimalModifier()
            {
                RootMotion = true,
                Grounded = true,
                Sprint = false,
                OrientToGround = true,
                CustomRotation = false,
            //    Colliders = true,
                FreeMovement = false,
                AdditivePosition = true,
                Gravity = false,
                modify = (modifier)(-1),
            };
        }
#endif
    }
}