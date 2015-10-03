using UnityEngine;
using System;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
	public class DynamicArrive : DynamicVelocityMatch
	{
		public DynamicArrive()
		{
//			this.Target = new KinematicData();
//			this.MovingTarget = new KinematicData();

			stopRadius = 0.5f;
			slowRadius = 10.0f;
			maxSpeed = 40.0f;
		}
		public override string Name
		{
			get { return "Arrive"; }
		}

		public float stopRadius { get; private set; }
		public float slowRadius { get; private set; }
		public float maxSpeed { get; private set; }
		public float targetSpeed { get; private set; }
		//public Vector3 direction { get; private set; }


		public override MovementOutput GetMovement()
		{
//			var direction = new MovementOutput();
			Vector3 direction = this.Target.position - this.Character.position;
			float distance = direction.magnitude;

			if (distance < stopRadius) {
//				var output = new MovementOutput ();
//				output.linear = Vector3.zero;
				return null;
			}

			if (distance > slowRadius)
				targetSpeed = maxSpeed;
			else
				targetSpeed = maxSpeed * (distance / slowRadius);

			this.Target.velocity = direction.normalized * targetSpeed;
			return base.GetMovement();
		}
	}
}
