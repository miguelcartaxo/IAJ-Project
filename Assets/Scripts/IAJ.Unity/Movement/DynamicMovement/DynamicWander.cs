using System;
using Assets.Scripts.IAJ.Unity.Util;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
	public class DynamicWander : DynamicSeek
	{
		public KinematicData Character { get; set; }

		public DynamicWander()
		{
			this.Target = new KinematicData();
			WanderOffset = 1.0f;
			WanderRadius = 10.0f;
			
		}
		public override string Name
		{
			get { return "Wander"; }
		}
		public float TurnAngle { get; private set; }
		
		public float WanderOffset { get; private set; }
		public float WanderRadius { get; private set; }
		
		protected float WanderOrientation { get; set; }
		
		protected float wanderRate = 1f;
		
		protected Vector3 circleCenter;
		
		
		
		public Vector3 OrientationToVector (float orientation){
			return new Vector3 (-(float)Math.Sin(orientation), 0f, (float)Math.Cos(orientation));
		}
		
		public override MovementOutput GetMovement()
		{

			WanderOrientation += RandomHelper.RandomBinomial() * wanderRate;
			Target.orientation = WanderOrientation + Character.orientation;
			circleCenter = Character.position + WanderOffset * OrientationToVector (Character.orientation);
			Target.position = circleCenter + WanderRadius * OrientationToVector (Target.orientation);
			return base.GetMovement();
			//			return new MovementOutput();
		}
		
		
	}
}
